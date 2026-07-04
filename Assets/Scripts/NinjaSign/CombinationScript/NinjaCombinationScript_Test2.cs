using System;
using UnityEngine;

[Serializable]
public class NinjaCombinationScript_Test2 : INinjaCombinationScript{
	public void Activate(NinjaCombinationScriptData data) {
		Debug.Log("Test2 activated.");
	}
}
