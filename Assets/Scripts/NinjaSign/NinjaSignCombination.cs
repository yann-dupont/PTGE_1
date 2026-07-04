using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ninja Sign Combination", menuName = "Ninja/Ninja Sign Combination")]
public class NinjaSignCombination : ScriptableObject {
	[SerializeField]
	private List<NinjaSignDescriptor> signsToActivate = new List<NinjaSignDescriptor>();

	[SerializeField]
	private string displayName = "";
}