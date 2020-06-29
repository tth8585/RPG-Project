using System.Collections.Generic;
using UnityEngine;

public class Inventory : Itemcontainer
{
    public static Inventory Instance { get; set; }
    [SerializeField] List<Item> startingItems;
    [SerializeField] Transform itemParent;
    public GameObject inventoryPanel;

    protected override void Awake()
    {
        itemParent = inventoryPanel.transform.Find("Grid Zone").Find("Item Slots Grid");

        if (itemParent != null)
        {
            itemSlots = itemParent.GetComponentsInChildren<ItemSlot>(includeInactive: true);
        }

        base.Awake();

        SetIndexSlot();

        //SetStartingItems();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
   
    private void SetStartingItems()
    {
        Clear();
        for(int i = 0; i < startingItems.Count; i++)
        {
            if (i == 0)
            {
                AddItem(startingItems[i].GetCopy());
            }
            else
            {
                for(int j = 0; j < 10; j++)
                {
                    AddItem(startingItems[i].GetCopy());
                }
            }
            
        }
    }
}
