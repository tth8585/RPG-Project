
using UnityEngine;
[CreateAssetMenu(menuName = "Spell/AuraSpell")]
public class AuraSpell : Spell
{
    public float manaReserved;
    public float manaRegen;
    public bool isActive;
    public override void ActiveSpell(PlayerController c)
    {
        c.playerStats.MP.AddModifier(new StatModifier(- manaReserved, StatModType.Flat, this));
        c.playerStats.MpRegen.AddModifier(new StatModifier(manaRegen, StatModType.Flat, this));
        c.playerStats.GetStat();
    }
    public override void InactiveSpell(PlayerController c)
    {
        c.playerStats.MP.RemoveAllModifierFromSource(this);
        c.playerStats.MpRegen.RemoveAllModifierFromSource(this);
        c.playerStats.GetStat();
    }
}
