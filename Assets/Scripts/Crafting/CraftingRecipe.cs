using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public struct ItemAmount 
{
    public Item item;
    [Range(1,1)]
    public int amount;
}

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
    [Space]
    public List<ItemAmount> materialsItem;
    public List<ItemAmount> resultsItem;

    public bool CanCraft(IItemContainer itemContainer)
    {
        return HasMaterials(itemContainer) && HasSpace(itemContainer);
    }

    private bool HasSpace(IItemContainer itemContainer)
    {
        foreach(ItemAmount itemAmount in resultsItem)
        {
            if (!itemContainer.CanAddItem(itemAmount.item, itemAmount.amount))
            {
                Debug.Log("No space");
                return false;
            }
        }
        return true;
    }

    private bool HasMaterials(IItemContainer itemContainer)
    {
        foreach (ItemAmount itemAmount in materialsItem)
        {
            if (itemContainer.ItemCount(itemAmount.item.ID) < itemAmount.amount)
            {
                Debug.Log("No material");
                return false;
            }
        }
        return true;
    }

    public void Craft(IItemContainer itemContainer)
    {
        if (CanCraft(itemContainer))
        {
            foreach (ItemAmount itemAmount in materialsItem) 
            {
                for(int i = 0; i< itemAmount.amount; i++)
                {
                    Item oldItem = itemContainer.RemoveItem(itemAmount.item.ID);
                    oldItem.Destroy();
                }
            }

            //random mod result
            int randomNumber = UnityEngine.Random.Range(0, resultsItem.Count);

            itemContainer.AddItem(resultsItem[randomNumber].item.GetCopy());
        }
    }

    public bool CanCraft(BaseItemSlot materialOne, BaseItemSlot materialTwo)
    {

        if (materialOne.item.ID == materialsItem[0].item.ID && materialTwo.item.ID == materialsItem[1].item.ID)
        {
            return true;
        }
        return false;
    }

    public void Craft(IItemContainer itemContainer, BaseItemSlot materialOne, BaseItemSlot materialTwo)
    {
        RemoveMaterials(itemContainer, materialOne, materialTwo);
        AddResults(itemContainer);
    }

    private void AddResults(IItemContainer itemContainer)
    {
        //random mod result
        int randomNumber = UnityEngine.Random.Range(0, resultsItem.Count);

        itemContainer.AddItem(resultsItem[randomNumber].item.GetCopy());
    }

    private static void RemoveMaterials(IItemContainer itemContainer, BaseItemSlot materialOne, BaseItemSlot materialTwo)
    {
        //remove materials
        itemContainer.RemoveItemSlot(materialOne);
        itemContainer.RemoveItemSlot(materialTwo);
    }
}
