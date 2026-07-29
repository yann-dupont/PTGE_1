using System;
using UnityEngine;

[Serializable]
public class NinjaCombinationScript_Spell : INinjaCombinationScript
{

    public void Activate(NinjaCombinationScriptData data)
    {
        if (data.combinationData.IsASpell)
        {
            bool withPerfectDirection = data.combinationData.InputActionToHold != null && 
                                        data.combinationData.InputActionToHold.action.IsPressed();

            PlayerSpellManager.Instance.CastSpell(data.combinationData.SpellPrefab, withPerfectDirection);
        }
        else
        {
            Debug.Log("This combination is not a spell.");
        }
    }
}
