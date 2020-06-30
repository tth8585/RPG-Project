using System.Collections.Generic;
using UnityEngine;

public class ItemSaveManager : MonoBehaviour
{
    public static ItemSaveManager Instance { get; set; }
    [SerializeField] ItemDataBase itemDataBase;

    private const string InventoryFileName = "Inventory";
    private const string EquipmentFileName = "Equipment";
    private const string StashFileName = "StashItem";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    public void LoadInventory()
    {
        ItemContainerSaveData saveSlots = ItemSaveIO.LoadItems(InventoryFileName);
        if(saveSlots == null)
        {
            return;
        }

        Inventory.Instance.Clear();

        for(int i = 0; i < saveSlots.SaveSlots.Length; i++)
        {
            ItemSlot itemSlot = Inventory.Instance.itemSlots[i];
            ItemSlotSaveData itemSlotSaveData = saveSlots.SaveSlots[i];

            if(itemSlotSaveData == null)
            {
                itemSlot.item = null;
                itemSlot.Amount = 0;
            }
            else
            {
                itemSlot.item = itemDataBase.GetItemCopy(itemSlotSaveData.ItemID);
                itemSlot.Amount = itemSlotSaveData.Amount;
            }
        }
    }
    public void LoadEquipment(PlayerController character)
    {
        
        ItemContainerSaveData saveSlots = ItemSaveIO.LoadItems(EquipmentFileName);
        if (saveSlots == null)
        {
            return;
        }

        foreach(ItemSlotSaveData saveSlot in saveSlots.SaveSlots)
        {
            if (saveSlot == null)
            {
                continue;
            }

            Item item = itemDataBase.GetItemCopy(saveSlot.ItemID);
            Inventory.Instance.AddItem(item);
            character.Equip((EquipableItem) item);
        }
    }
    public void LoadStash()
    {
        ItemContainerSaveData saveSlots = ItemSaveIO.LoadItems(StashFileName);
        if (saveSlots == null)
        {
            return;
        }

        ItemStash.Instance.Clear();

        for (int i = 0; i < saveSlots.SaveSlots.Length; i++)
        {
            ItemSlot itemSlot = ItemStash.Instance.itemSlots[i];
            ItemSlotSaveData itemSlotSaveData = saveSlots.SaveSlots[i];

            if (itemSlotSaveData == null)
            {
                itemSlot.item = null;
                itemSlot.Amount = 0;
            }
            else
            {
                itemSlot.item = itemDataBase.GetItemCopy(itemSlotSaveData.ItemID);
                itemSlot.Amount = itemSlotSaveData.Amount;
            }
        }
    }
    public void SaveInventory()
    {
        SaveItems(Inventory.Instance.itemSlots, InventoryFileName);
    }

    public void SaveEquipment()
    {
        SaveItems(EquipmentPanel.Instance.equipmentSlots, EquipmentFileName);
    }
    public void SaveStash()
    {
        SaveItems(ItemStash.Instance.itemSlots, StashFileName);
    }

    private void SaveItems(IList<ItemSlot> itemSlots, string fileName)
    {
        var saveData = new ItemContainerSaveData(itemSlots.Count);

        for(int i = 0; i < saveData.SaveSlots.Length; i++)
        {
            ItemSlot itemSlot = itemSlots[i];
            if(itemSlot.item == null)
            {
                saveData.SaveSlots[i] = null;
            }
            else
            {
                saveData.SaveSlots[i] = new ItemSlotSaveData(itemSlot.item.ID, itemSlot.Amount);
            }
        }

        ItemSaveIO.SaveItems(saveData, fileName);
    }
}
