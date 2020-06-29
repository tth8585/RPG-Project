
using UnityEngine;

public class LootDrop : MonoBehaviour
{
    public Item item;
    public int weight;

    public LootDrop(Item item, int weight)
    {
        this.item = item;
        this.weight = weight;
    }
}
