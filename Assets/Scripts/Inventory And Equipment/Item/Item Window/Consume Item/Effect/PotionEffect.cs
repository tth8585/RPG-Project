
using UnityEngine;

public abstract class PotionEffect : ScriptableObject
{
    public abstract void ExecuteEffect(PotionItem potionItem, PlayerController character);

    public abstract string GetDescription();
}
