using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AbilityType
{
    Helm,
    Quiver,
    Star,
    Cloak,
    Tome,
    Spell,
}
[CreateAssetMenu(menuName = "Items/Equipment Item/Ability Item")]
public class AbilityEquipment : EquipableItem
{
    [Space]
    public AbilityType abilityType;
}
