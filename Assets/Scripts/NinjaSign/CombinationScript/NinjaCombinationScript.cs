using UnityEngine.SceneManagement;

public struct NinjaCombinationScriptData {
	public Scene Scene;
	public NinjaSignCombination combinationData;
}

public interface INinjaCombinationScript {
	public void Activate(NinjaCombinationScriptData data);
}
