using System;
using UnityEngine;

[Serializable]
public class NinjaCombinationScript_Spell : INinjaCombinationScript
{
	
	public void Activate(NinjaCombinationScriptData data) 
	{
		if (data.combinationData.IsASpell)
		{
			Debug.Log("test");
			PlayerSpellManager.Instance.CastSpell(data.combinationData.SpellPrefab);
		}
		else
		{
			Debug.Log("This combination is not a spell.");
		}
	}
}
