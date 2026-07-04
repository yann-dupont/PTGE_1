using System;

[Serializable]
public class NinjaCombinationScript_Test1 : INinjaCombinationScript {
	public void Activate(NinjaCombinationScriptData data) {
		if (FlashScreen.HasIsntance(data.Scene)) {
			FlashScreen.Instance(data.Scene).Display("Test 1 activated.");
		}
	}
}
