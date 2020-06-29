using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PotionType
{
    NormalHP,
    NormalMP,

    ManaPotion,
    LifePotion,

    STRPotion,
    INTPotion,
    AGIPotion,
}
[CreateAssetMenu(menuName = "Items/Consume Item/Potion Item")]
public class PotionItem : ConsumableItem
{
    [Space]
    public PotionType potionType;

    public List<PotionEffect> effects;
    public virtual void Consume(PlayerController c)
    {
        foreach(PotionEffect potionEffect in effects)
        {
            potionEffect.ExecuteEffect(this, c);
        }
    }

    public override string GetItemDescription()
    {
        stringBuilder.Length = 0;
        foreach(PotionEffect potionEffect in effects)
        {
            stringBuilder.AppendLine(potionEffect.GetDescription());
        }
        return stringBuilder.ToString();
    }
}
