using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ConsumableType
{
    Potion,
    Currency,
}
public class ConsumableItem : Item
{
    [Space]
    public ConsumableType consumableType;

    public override Item GetCopy()
    {
        return Instantiate(this);
    }

    public override void Destroy()
    {
        Destroy(this);
    }

    public override string GetItemType()
    {
        return consumableType.ToString();
    }
}
