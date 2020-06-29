using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipeUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] RectTransform arrowParent;
    [SerializeField] BaseItemSlot[] itemSlots;

    [Header("Public Variables")]
    public Itemcontainer itemcontainer;

    private CraftingRecipe craftingRecipe;
    public CraftingRecipe CraftingRecipe
    {
        get { return craftingRecipe; }
        set { SetCraftingRecipe(value); }
    }

    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;

    private void OnValidate()
    {
        itemSlots = GetComponentsInChildren<BaseItemSlot>(includeInactive: true);
    }
    private void Start()
    {
        foreach(BaseItemSlot baseItemSlot in itemSlots)
        {
            baseItemSlot.OnPointerEnterEvent += OnPointerEnterEvent;
            baseItemSlot.OnPointerExitEvent += OnPointerExitEvent;
        }
    }
    public void OnCraftBtnClick()
    {
        //if (craftingRecipe != null && itemcontainer != null)
        //{
        //    if (craftingRecipe.CanCraft(itemcontainer))
        //    {
        //        if (!itemcontainer.CanAddItem())
        //        {
        //            craftingRecipe.Craft(itemcontainer);
        //        }
        //        else
        //        {
        //            Debug.Log("inventory is full");
        //        }
        //    }
        //    else
        //    {
        //        Debug.Log("not have the materials");
        //    }
        //}
    }
    private void SetCraftingRecipe(CraftingRecipe newCraftingRecipe)
    {
        craftingRecipe = newCraftingRecipe;
        if (craftingRecipe != null)
        {
            int slotIndex = 0;
            slotIndex = SetSlots(craftingRecipe.materialsItem,slotIndex);
            arrowParent.SetSiblingIndex(slotIndex);
            slotIndex = SetSlots(craftingRecipe.resultsItem, slotIndex);

            for (int i = slotIndex; i < itemSlots.Length; i++)
            {
                itemSlots[i].transform.parent.gameObject.SetActive(false);
            }

            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private int SetSlots(IList<ItemAmount> itemAmountsList, int slotIndex)
    {
        for(int i = 0; i < itemAmountsList.Count; i++, slotIndex++)
        {
            ItemAmount itemAmount = itemAmountsList[i];
            BaseItemSlot baseItemSlot = itemSlots[slotIndex];

            baseItemSlot.item = itemAmount.item;
            baseItemSlot.Amount = itemAmount.amount;
            baseItemSlot.transform.parent.gameObject.SetActive(true);
        }
        return slotIndex;
    }
}
