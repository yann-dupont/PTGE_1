using System;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        Debug.Log(SceneManager.GetActiveScene().name=="Island");
        if (GameplayManager.instance.AreAllEnnemiesDead() && SceneManager.GetActiveScene().name=="Island")
        {
            FlashScreen.Instance(data.Scene).Display("This is a test.\nI've accepted this test to stand victorious against my past.\nA person grows once they are able to defeat their weaker self. \n\n Congratulations",30f);
        }
        else if (FlashScreen.HasInstance(data.Scene))
        {
            FlashScreen.Instance(data.Scene).Display(data.combinationData.DisplayName + data.combinationData.InputActionToHold.action.name,0.5f);
        }
    }

    private static void Chevreuil(NinjaCombinationScriptData data)
    {
        if (FlashScreen.HasInstance(data.Scene))
        {
            FlashScreen.Instance(data.Scene).Display(data.combinationData.DisplayName,0.5f);
        }
    }
}
