using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ArmorType
{
    LeatherArmor,
    Robe,
    HeavyArmor,
}
[CreateAssetMenu(menuName = "Items/Equipment Item/Armour Item")]
public class ArmourEquipment : EquipableItem
{
    [Space]
    public ArmorType armorType;
}
