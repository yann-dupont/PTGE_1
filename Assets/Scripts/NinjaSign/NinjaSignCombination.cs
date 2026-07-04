using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ninja Sign Combination", menuName = "Ninja/Ninja Sign Combination")]
public class NinjaSignCombination : ScriptableObject {
	[SerializeField]
	private List<NinjaSignDescriptor> signsToActivate = new List<NinjaSignDescriptor>();

	[SerializeField]
	private string displayName = "";
	
	[SerializeReference, Tooltip("Must match exactly the name of the C# script to execute.")]
	private string scriptType;
	
	public IEnumerable<NinjaSignDescriptor> SignsToActivate => signsToActivate;
	public string DisplayName => displayName;
	public INinjaCombinationScript ScriptType {
		get {
			Type scriptTypeValue = Type.GetType(scriptType);
			if (scriptTypeValue == null) {
				return null;
			}
			return Activator.CreateInstance(scriptTypeValue) as INinjaCombinationScript;
		}
	}
}