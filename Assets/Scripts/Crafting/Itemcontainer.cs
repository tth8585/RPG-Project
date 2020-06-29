using System;
using UnityEngine;

public abstract class Itemcontainer: MonoBehaviour, IItemContainer
{
    public ItemSlot[] itemSlots;

    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;
    public event Action<BaseItemSlot> OnRightClickEvent;

    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    protected virtual void OnValidate()
    {
        itemSlots = GetComponentsInChildren<ItemSlot>(includeInactive: true);
    }
    protected virtual void Awake()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnPointerEnterEvent += slot => OnPointerEnterEvent(slot);
            itemSlots[i].OnPointerExitEvent += slot => OnPointerExitEvent(slot);
            itemSlots[i].OnRightClickEvent += slot => OnRightClickEvent(slot);
            itemSlots[i].OnBeginDragEvent += slot => OnBeginDragEvent(slot);
            itemSlots[i].OnEndDragEvent += slot => OnEndDragEvent(slot);
            itemSlots[i].OnDragEvent += slot => OnDragEvent(slot);
            itemSlots[i].OnDropEvent += slot => OnDropEvent(slot);
        }
    }
    public virtual bool AddItem(Item item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].CanAddStack(item))
            {
                itemSlots[i].item = item;
                itemSlots[i].Amount++;
                return true;
            }
        }

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item == null)
            {
                itemSlots[i].item = item;
                itemSlots[i].Amount++;
                return true;
            }
        }

        return false;
    }

    public virtual bool RemoveItem(Item item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item == item)
            {
                itemSlots[i].Amount--;
                return true;
            }
        }

        return false;
    }
    public virtual Item RemoveItem(string itemID)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            Item item = itemSlots[i].item;
            if (item != null && item.ID == itemID)
            {
                itemSlots[i].Amount--;
                return item;
            }
        }

        return null;
    }

    public virtual Item RemoveItemSlot(BaseItemSlot itemSlot)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlot != null && i == itemSlot.IndexSlot)
            {
                itemSlots[i].Amount--;
                if(itemSlots[i].Amount == 0)
                {
                    return null;
                }
                
                return itemSlot.item;
            }
        }

        return null;
    }

    public virtual int ItemCount(string itemID)
    {
        int number = 0;

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].item != null && itemSlots[i].item.ID == itemID)
            {
                number += itemSlots[i].Amount;
            }  
        }

        return number;
    }

    public virtual void Clear()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].item = null;
            itemSlots[i].Amount = 0;
        }
    }

    public virtual void SetIndexSlot()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].IndexSlot = i;
        }
    }

    public bool CanAddItem(Item item, int amount = 1)
    {
        int freeSpace = 0; 
        foreach(ItemSlot itemSlot in itemSlots)
        {
            if(itemSlot.item == null || itemSlot.item.ID == item.ID)
            {
                freeSpace += item.maximumStack - itemSlot.Amount;
            }
        }
        return freeSpace >= amount;
    }
}