using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/Item Effect/Heal")]
public class HealItemEffect : PotionEffect
{
    public int healthAmount;
    public override void ExecuteEffect(PotionItem potionItem, PlayerController character)
    {
        character.playerStats.currentHP = Mathf.Clamp(character.playerStats.currentHP + healthAmount, 0f, character.playerStats.HP.baseValue);
    }

    public override string GetDescription()
    {
        return "Heals for " + healthAmount + " health.";
    }
}
