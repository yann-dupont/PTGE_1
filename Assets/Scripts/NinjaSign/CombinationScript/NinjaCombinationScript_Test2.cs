using System;
using UnityEngine;

[Serializable]
public class NinjaCombinationScript_Test2 : INinjaCombinationScript{
	public void Activate(NinjaCombinationScriptData data) {
		if (FlashScreen.HasIsntance(data.Scene)) {
			FlashScreen.Instance(data.Scene).Display("Test 2 activated.");
		}
	}
}
