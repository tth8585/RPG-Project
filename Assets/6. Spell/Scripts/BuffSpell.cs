
using UnityEngine;

[CreateAssetMenu(menuName = "Spell/BuffSpell")]
public class BuffSpell : Spell
{
    public override void ActiveSpell(PlayerController c)
    {
        c.playerStats.currentHP += spellDamage;
        c.playerStats.GetStat();
        UIEvent.HealthChanged(c.playerStats.currentHP, c.playerStats.HP.value);
    }
}
