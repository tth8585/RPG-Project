using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Character/Character Effect/Armored Effect")]
public class ArmoredEffect :CharacterStatus
{
    public override string GetDescription()
    {
        return "Doubles base DEF";
    }
}
