using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Character/Character Effect/Armor Piercing Effect")]
public class ArmorPiercingEffect : CharacterStatus
{
    public override string GetDescription()
    {
        return "attacks treat their target’s Defense as if it were 0";
    }
}
