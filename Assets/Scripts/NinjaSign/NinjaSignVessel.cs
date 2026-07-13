using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NinjaSignVessel : MonoBehaviour {
	private const string NINJA_SIGNS_RESOURCE_PATH = "Ninja/Signs";
	private const string NINJA_COMBINATIONS_RESOURCE_PATH = "Ninja/Combinations";
	
	private const int MAX_NINJA_SIGNS = 6;

	public event Action<NinjaSignDescriptor> OnNinjaSignAdded;
	public event Action<NinjaSignCombination> OnNinjaCombinationExecuted;
	
	private static Dictionary<Scene, NinjaSignVessel> instances = new Dictionary<Scene, NinjaSignVessel>();
	
	private NinjaSignDescriptor[] ninjaSigns = Array.Empty<NinjaSignDescriptor>();
	private NinjaSignCombination[] ninjaSignCombinations = Array.Empty<NinjaSignCombination>();
	
	private NinjaSignDescriptor[] ninjaSignArray = new NinjaSignDescriptor[MAX_NINJA_SIGNS];
	private int currentNinjaSignIndex = 0;

	public static bool HasIsntance(Scene forScene) {
		return instances.ContainsKey(forScene);
	}

	public static NinjaSignVessel Instance(Scene forScene) {
		return instances[forScene];
	}
	
	public IEnumerable<NinjaSignDescriptor> NinjaSigns => ninjaSigns;
	public IEnumerable<NinjaSignCombination> NinjaSignCombinations => ninjaSignCombinations;
	
	private void Awake() {
		if (HasIsntance(gameObject.scene)) {
			Debug.Log("Ok buddy.");
			DestroyImmediate(gameObject);
		} else {
			LoadAllResources();
			instances.Add(gameObject.scene, this);
		}
	}

	private void OnDestroy() {
		if (HasIsntance(gameObject.scene) && Instance(gameObject.scene) == this) {
			instances.Remove(gameObject.scene);
		}
	}

	private void LoadAllResources() {
		ninjaSigns = Resources.LoadAll<NinjaSignDescriptor>(NINJA_SIGNS_RESOURCE_PATH);
		ninjaSignCombinations = Resources.LoadAll<NinjaSignCombination>(NINJA_COMBINATIONS_RESOURCE_PATH);
	}

	public bool AddNinjaSign(NinjaSignDescriptor ninjaSign) {
		if (ninjaSign == null) {
			return false;
		}
		
		ninjaSignArray[currentNinjaSignIndex] = ninjaSign;
		OnNinjaSignAdded?.Invoke(ninjaSign);
		Debug.Log($"Sign added '{ninjaSign.DisplayName}'.");	
		
		NinjaSignCombination foundCombination = CheckForNinjaSignCombinationFromCurrentIndex();
		currentNinjaSignIndex = (currentNinjaSignIndex + 1) % MAX_NINJA_SIGNS;

		if (foundCombination != null) {
			OnNinjaCombinationExecuted?.Invoke(foundCombination);
			INinjaCombinationScript foundScriptToActivate = foundCombination.ScriptToActivate;
			if (foundScriptToActivate != null) {
				foundScriptToActivate.Activate(CreateScriptData(foundCombination));
			} else {
				Debug.Log($"Combination activated '{foundCombination.DisplayName}', but no valid script was found to activate.");	
			}
			
			ninjaSignArray = new NinjaSignDescriptor[MAX_NINJA_SIGNS];
		}
		
		return foundCombination != null;
	}

	private NinjaCombinationScriptData CreateScriptData(NinjaSignCombination foundCombination) {
		NinjaCombinationScriptData scriptData;
		scriptData.Scene = gameObject.scene;
		scriptData.combinationData = foundCombination;
		return scriptData;
	}

	private NinjaSignCombination CheckForNinjaSignCombinationFromCurrentIndex() {
		List<NinjaSignCombination> validatingCombinations = ninjaSignCombinations.ToList();
		List<NinjaSignCombination> validCombinations = new List<NinjaSignCombination>();
		for (int i = 0; i < MAX_NINJA_SIGNS; i++) {
			int ninjaSignIndex = currentNinjaSignIndex - i;
			if (ninjaSignIndex < 0) {
				ninjaSignIndex += MAX_NINJA_SIGNS;
			}
			
			NinjaSignDescriptor ninjaSign = ninjaSignArray[ninjaSignIndex];
			if (ninjaSign == null) {
				break;
			}

			List<NinjaSignCombination> combinationsToRemove = new List<NinjaSignCombination>();
			foreach (NinjaSignCombination validatingCombination in validatingCombinations) {
				if (i >= validatingCombination.NumberOfSignsToActivate) {
					combinationsToRemove.Add(validatingCombination);
					break;
				}

				int indexOfCombinationNinjaSignToValidate = validatingCombination.NumberOfSignsToActivate - i - 1;
				if (validatingCombination.SignsToActivate[indexOfCombinationNinjaSignToValidate] == ninjaSign) {
					if (i + 1 >= validatingCombination.NumberOfSignsToActivate) {
						combinationsToRemove.Add(validatingCombination);
						validCombinations.Add(validatingCombination);
					}
				}
				else {
					combinationsToRemove.Add(validatingCombination);
				}
			}

			foreach (NinjaSignCombination combinationToRemove in combinationsToRemove) {
				validatingCombinations.Remove(combinationToRemove);
			}
		}

		if (validCombinations.Count == 0) {
			return null;
		}

		return validCombinations[0];
	}
}
