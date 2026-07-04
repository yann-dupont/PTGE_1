using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NinjaSignVessel : MonoBehaviour {
	private const string NINJA_SIGNS_RESOURCE_PATH = "Ninja/Signs";
	private const string NINJA_COMBINATIONS_RESOURCE_PATH = "Ninja/Combinations";
	
	private static Dictionary<Scene, NinjaSignVessel> instances = new Dictionary<Scene, NinjaSignVessel>();
	
	private NinjaSignDescriptor[] ninjaSigns = Array.Empty<NinjaSignDescriptor>();
	private NinjaSignCombination[] ninjaSignCombinations = Array.Empty<NinjaSignCombination>();

	public static bool HasIsntance(Scene forScene) {
		return instances.ContainsKey(forScene);
	}

	public NinjaSignVessel Instance(Scene forScene) {
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
}
