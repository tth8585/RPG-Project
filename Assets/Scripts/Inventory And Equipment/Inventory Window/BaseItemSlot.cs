using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class BaseItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] public Image image;
    [SerializeField] public Text amountText;

    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;
    public event Action<BaseItemSlot> OnRightClickEvent;

    protected Color normalColor = Color.white;
    protected Color disableColor = Color.clear;
    protected Color blackColor = Color.black;
    //protected Color craftColor = new Color(1,1,1,0.4f);

    protected bool isPointerOver;

    private Item _item;
    public Item item
    {
        get { return _item; }
        set
        {
            _item = value;

            if(_item == null && Amount != 0)
            {
                Amount = 0;
            }

            if (_item == null)
            {
                image.color = disableColor;
            }
            else
            {
                image.sprite = _item.iconItem;
                image.color = normalColor;
            }

            if (isPointerOver)
            {
                OnPointerExit(null);
                OnPointerEnter(null);
            }
        }
    }

    private int _amount;
    public int Amount
    {
        get { return _amount; }
        set
        {
            _amount = value;

            if (_amount < 0)
            {
                _amount = 0;
            }

            if (_amount == 0 && item != null)
            {
                item = null;
            }

            amountText.enabled = ((_item != null) && _amount > 1);

            if (amountText.enabled)
            {
                amountText.text = _amount.ToString();
            }
        }
    }

    public int IndexSlot;

    protected virtual void OnValidate()
    {
        if (image == null)
        {
            image.GetComponent<Image>();
        }

        if (amountText == null)
        {
            amountText.GetComponentInChildren<Text>();
        }
    }

    protected virtual void OnDisable()
    {
        if (isPointerOver)
        {
            OnPointerExit(null);
        }
    }
    public virtual bool CanAddStack(Item item, int amount = 1)
    {
        return this.item != null && this.item.ID == item.ID;
    }
    public virtual bool CanReceiveItem(Item item)
    {
        return false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (OnRightClickEvent != null)
            {
                OnRightClickEvent(this);
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true;
        if (OnPointerEnterEvent != null)
        {
            OnPointerEnterEvent(this);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;

        if (OnPointerExitEvent != null)
        {
            OnPointerExitEvent(this);
        }
    }
}
