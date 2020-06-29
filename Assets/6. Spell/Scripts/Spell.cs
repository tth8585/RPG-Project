
using UnityEngine;

public enum SpellAbility
{
    TargetUnit,
    TargetPoint,
    TargetArea,
    NoTarget,
    Toggle,
    Passive,
}
public enum SpellAffects
{
    Allies,
    Enemy,
}
public enum SpellDamageType
{
    MagicalDamage,
    PhysicalDamage,
    PureDamage,
}
[CreateAssetMenu(menuName = "Spell")]
public class Spell: ScriptableObject
{
    public SpellAbility spellAbility;
    public SpellAffects spellAffects;
    public SpellDamageType spellDamageType;
    public GameObject spellFX;

    public string spellId;
    public string spellName;
    public Sprite spellIcon;
    public string spellDescription;

    public float spellDamage;
    public float spellCastTime;

    public float spellRange;
    public float spellAoe;
    public int spellNumberTarget;

    public float spellCoolDown;
    public float currentSpellCoolDown;

    public float manaCost;

    public virtual void ActiveSpell(PlayerController c)
    {

    }
    public virtual void InactiveSpell(PlayerController c)
    {
       
    }
}
