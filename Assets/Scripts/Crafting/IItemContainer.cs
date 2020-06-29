﻿
public interface IItemContainer 
{
    int ItemCount(string itemID);
    Item RemoveItem(string itemID);
    Item RemoveItemSlot(BaseItemSlot baseItemSlot);
    bool RemoveItem(Item item);
    bool AddItem(Item item);
    bool CanAddItem(Item item, int amount = 1);
    void Clear();
    void SetIndexSlot();
}
