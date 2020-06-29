using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/Item Effect/Stat Buff")]
public class StatBuffEffect : PotionEffect
{
    public int DEXbuff;
    public float duration;
    public override void ExecuteEffect(PotionItem potionItem, PlayerController character)
    {
        StatModifier statModifier = new StatModifier(DEXbuff, StatModType.Flat, this);
        //character.playerStats.DEX.AddModifier(statModifier);
        character.StartCoroutine(RemoveBuff(character, statModifier, duration));
        character.UpdateStatValues();
    }

    public override string GetDescription()
    {
        return "Grants " + DEXbuff + " Dex for " + duration + " seconds.";
    }

    private static IEnumerator RemoveBuff(PlayerController character, StatModifier statModifier, float duration)
    {
        yield return new WaitForSeconds(duration);
        //character.playerStats.DEX.RemoveModifier(statModifier);
        Debug.Log("chua fix buff effect stat");
        character.UpdateStatValues();
    }
}
