using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Ninja Sign Combination", menuName = "Ninja/Ninja Sign Combination")]
public class NinjaSignCombination : ScriptableObject {
	[SerializeField]
	private List<NinjaSignDescriptor> signsToActivate = new List<NinjaSignDescriptor>();

	[SerializeField]
	private string displayName = "";
	
	[SerializeReference, Tooltip("Must match exactly the name of the C# script to execute.")]
	private string scriptType;
	
	[Header("Spell Combination")]
	[SerializeField, Tooltip("If is a spell, add the spell GO prefab to be casted")] private bool isASpell;
	[SerializeField] private GameObject spellPrefab; 
	
	public List<NinjaSignDescriptor> SignsToActivate => signsToActivate;

	[Tooltip("Stay empty if no action to hold to activate")]
	public InputActionReference InputActionToHold;

    public int NumberOfSignsToActivate => signsToActivate.Count;
	public string DisplayName => displayName;
	public bool IsASpell => isASpell;
	public GameObject SpellPrefab => spellPrefab;
	public INinjaCombinationScript ScriptToActivate {
		get {
			Type scriptTypeValue = Type.GetType(scriptType);
			if (scriptTypeValue == null) {
				return null;
			}
			return Activator.CreateInstance(scriptTypeValue) as INinjaCombinationScript;
		}
	}
}