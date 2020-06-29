using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : BaseItemSlot, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    public override bool CanAddStack(Item item, int amount = 1)
    {
        return base.CanAddStack(item, amount) && Amount + amount < item.maximumStack; ;
    }
    public override bool CanReceiveItem(Item item)
    {
        return true;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (OnDropEvent != null)
        {
            OnDropEvent(this);
        }
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (OnBeginDragEvent != null)
        {
            OnBeginDragEvent(this);
            amountText.gameObject.SetActive(false);
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragEvent != null)
        {
            OnDragEvent(this);
            image.color = disableColor;
            //image.color = blackColor;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (OnEndDragEvent != null)
        {
            OnEndDragEvent(this);
            amountText.gameObject.SetActive(true);

        }

        if(item == null)
        {
            //image.color = normalColor;
        }
        else
        {
            image.color = normalColor;
        }
    }
}
