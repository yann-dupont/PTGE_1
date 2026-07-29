using System;

[Serializable]
public class NinjaCombinationScript_Chevreuil : INinjaCombinationScript {
	public void Activate(NinjaCombinationScriptData data)
    {
        if (data.combinationData.InputActionToHold != null && 
            data.combinationData.InputActionToHold.action.IsPressed())
        {
            ChevreuilDiriger(data);
        }
        else
        {
            Chevreuil(data);
        }
    }

    /// <summary>
    /// Effet ninja différent (ou meilleur) si fait dans un direction
    /// </summary>
    /// <param name="data"></param>
    private static void ChevreuilDiriger(NinjaCombinationScriptData data)
    {
        if (FlashScreen.HasInstance(data.Scene))
        {
            FlashScreen.Instance(data.Scene).Display(data.combinationData.DisplayName + data.combinationData.InputActionToHold.action.name);
        }
    }

    private static void Chevreuil(NinjaCombinationScriptData data)
    {
        if (FlashScreen.HasInstance(data.Scene))
        {
            FlashScreen.Instance(data.Scene).Display(data.combinationData.DisplayName);
        }
    }
}
