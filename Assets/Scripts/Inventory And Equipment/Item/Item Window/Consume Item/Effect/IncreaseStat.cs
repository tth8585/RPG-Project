
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item Effect/Increase Stat")]
public class IncreaseStat : PotionEffect
{
    public int amount;
    public PotionType potionType;
    public override string GetDescription()
    {
        switch (potionType)
        {
            case PotionType.NormalHP:
                return "Heals for " + amount + " health.";
            case PotionType.NormalMP:
                return "Mana for " + amount + " health.";
            case PotionType.ManaPotion:
                return "+" + amount + " max MP.";
            case PotionType.LifePotion:
                return "+" + amount + " max HP."; 
            case PotionType.STRPotion:
                return "+" + amount + " max STR." ;
            case PotionType.INTPotion:
                return "+" + amount + " max INT.";
            case PotionType.AGIPotion:
                return "+" + amount + " max AGI.";
            default:
                return "wrong poiton type";
        }
        
    }

    public override void ExecuteEffect(PotionItem potionItem, PlayerController character)
    {
        Debug.Log("chua xu ly potion + stat");
    }
}
