
using System.Collections.Generic;
using UnityEngine;

public class DropTable
{
    public List<LootDrop> loot;

    public Item GetDrop()
    {
        int roll = Random.Range(0, 100 + 1);
        int weightSum = 0;
        foreach(LootDrop lootDrop in loot)
        {
            weightSum += lootDrop.weight;
            if (roll < weightSum)
            {
                return lootDrop.item.GetCopy();
            }
        }
        return null;
    }
}


