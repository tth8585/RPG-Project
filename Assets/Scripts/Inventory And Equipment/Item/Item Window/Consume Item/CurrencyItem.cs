
using UnityEngine;

public enum CurrencyType
{
    ArmourersScrap,

    PortalScroll,
    ScrollOfWisdom,

    OrbOfScouring, // xoa het dong
    OrbOfTransmutation, // them 1 dong thanh magic
    OrbOfAlteration, // random magic item
    OrbOfRegret, // refund point
    VaalOrb, //mod to nowhere
    OrbOfAlchemy, // item thanh rare
    OrbOfAnnulment, // xoa 1 dong bat ky
    RegalOrb, // them 1 dong, item thanh rare
    ChaosOrb, //random rare item
}
[CreateAssetMenu(menuName = "Items/Consume Item/Currency Item")]
public class CurrencyItem : ConsumableItem
{
    [Space]
    public CurrencyType currencyType;
    public string description;

    public override string GetItemDescription()
    {
        stringBuilder.Length = 0;
        stringBuilder.AppendLine(description);
        return stringBuilder.ToString();
    }
}
