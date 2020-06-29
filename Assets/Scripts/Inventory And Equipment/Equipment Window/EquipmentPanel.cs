using System;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    public static EquipmentPanel Instance { get; set; }
    [SerializeField] Transform equipmentSlotParent;
    public EquipmentSlot[] equipmentSlots;

    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;
    public event Action<BaseItemSlot> OnRightClickEvent;

    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    public GameObject equipmentPanel;

    private void Awake()
    {
        equipmentSlotParent = equipmentPanel.transform;

        if (equipmentSlotParent != null)
        {
            equipmentSlots = equipmentSlotParent.GetComponentsInChildren<EquipmentSlot>();
        }

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            equipmentSlots[i].OnPointerEnterEvent += slot => OnPointerEnterEvent(slot);
            equipmentSlots[i].OnPointerExitEvent += slot => OnPointerExitEvent(slot);
            equipmentSlots[i].OnRightClickEvent += slot => OnRightClickEvent(slot);
            equipmentSlots[i].OnBeginDragEvent += slot => OnBeginDragEvent(slot);
            equipmentSlots[i].OnEndDragEvent += slot => OnEndDragEvent(slot);
            equipmentSlots[i].OnDragEvent += slot => OnDragEvent(slot);
            equipmentSlots[i].OnDropEvent += slot => OnDropEvent(slot);
        }
    }
    public bool AddItem(EquipableItem item, out EquipableItem previousItem)
    {
        for(int i = 0; i < equipmentSlots.Length; i++)
        {
            if(equipmentSlots[i].equipmentType == item.equipmentType)
            {
                previousItem = (EquipableItem) equipmentSlots[i].item;
                equipmentSlots[i].item = item;
                equipmentSlots[i].Amount = 1;
                return true;
            }
        }

        previousItem = null;
        return false;
    }

    public bool RemoveItem(EquipableItem item)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].item == item)
            {
                equipmentSlots[i].item = null;
                return true;
            }
        }

        return false;
    }
}
