using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Inventory inventory;
    private EquipmentPanel equipmentPanel;
    private StatPanel statPanel;
    private ItemTooltip itemTooltip;
    [SerializeField] Image dragableItem;
    [SerializeField] DropItem dropItemArea;
    private PlayerWeaponController playerWeaponController;

    private ItemSlot draggedSlot;

    public bool isDead;
    bool isImmortal = false;

    [HideInInspector]
    public PlayerStats playerStats;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        
        playerWeaponController = GetComponent<PlayerWeaponController>();
    }

    private void SetEvents()
    {
        //Set up Events
        //right click
        inventory.OnRightClickEvent += EquipOrConsume;
        equipmentPanel.OnRightClickEvent += Unequip;
        //Pointer enter
        inventory.OnPointerEnterEvent += ShowTooltip;
        equipmentPanel.OnPointerEnterEvent += ShowTooltip;
        //pointer exit
        inventory.OnPointerExitEvent += HideTooltip;
        equipmentPanel.OnPointerExitEvent += HideTooltip;
        //begin drag
        inventory.OnBeginDragEvent += BeginDrag;
        equipmentPanel.OnBeginDragEvent += BeginDrag;
        //end drag
        inventory.OnEndDragEvent += EndDrag;
        equipmentPanel.OnEndDragEvent += EndDrag;
        //drag
        inventory.OnDragEvent += Drag;
        equipmentPanel.OnDragEvent += Drag;
        //drop
        inventory.OnDropEvent += Drop;
        equipmentPanel.OnDropEvent += Drop;
        dropItemArea.OnDropEvent += DropItemOutsideUI;
    }

    private void Start()
    {
        inventory = Inventory.Instance;
        equipmentPanel = EquipmentPanel.Instance;
        statPanel = StatPanel.Instance;
        itemTooltip = ItemTooltip.Instance;

        statPanel.SetStats(playerStats.STR, playerStats.INT, playerStats.AGI);
        
        SetEvents();
        //LoadDataFromSaveFile();
        isDead = false;

        statPanel.UpdateStatValue();
        
        craftingRecipe = craftRecipeDataBase.recipe;
    }

    private void EquipOrConsume(BaseItemSlot itemSlot)
    {
        //return;
        if(itemSlot.item == null)
        {
            if (IsRightClickOnCurrency)
            {
                RedoClickOnCurrency();
            }
            else
            {
                //nothing happend 
            }
            //Debug.Log("no item here");
        }

        EquipableItem equipableItem = itemSlot.item as EquipableItem;
        ConsumableItem consumableItem = itemSlot.item as ConsumableItem;

        if (equipableItem != null && consumableItem == null)
        {
            if (IsRightClickOnCurrency)
            {
                //Debug.Log("craft here");
                Craft(itemSlot);
                return;
            }

            Equip(equipableItem);
        }
        else if(consumableItem != null && equipableItem == null)
        {
            if(consumableItem.consumableType == ConsumableType.Currency)
            {
                RightClickOnCurrency(itemSlot);
            }
            else if (consumableItem.consumableType == ConsumableType.Potion)
            {
                if (IsRightClickOnCurrency)
                {
                    Debug.Log("still using currency");
                    return;
                }
                ConsumeItem(consumableItem);
            }
        }
    }

    private bool IsRightClickOnCurrency = false;
    private Color normalColor = Color.white;
    private Color craftColor = new Color(1,1,1,0.4f);
    private BaseItemSlot usingSlot;
    private void RightClickOnCurrency(BaseItemSlot baseItemSlot)
    {
        IsRightClickOnCurrency = true;

        dragableItem.sprite = baseItemSlot.item.iconItem;
        dragableItem.rectTransform.SetAsLastSibling();

        usingSlot = baseItemSlot;
        baseItemSlot.image.color = craftColor;

        dragableItem.enabled = IsRightClickOnCurrency;
    }
    private void RedoClickOnCurrency()
    {
        IsRightClickOnCurrency = false;
        dragableItem.enabled = false;
       // Debug.Log(dragableItem);
       // Debug.Log(usingSlot.item);
        if (usingSlot.Amount != 0)
        {
            usingSlot.image.color = normalColor;
        }
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }

        if (IsRightClickOnCurrency)
        {
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            dragableItem.transform.position = screenPosition;
        }
    }

    private void Unequip(BaseItemSlot itemSlot)
    {
        EquipableItem equipableItem = itemSlot.item as EquipableItem;

        if (equipableItem != null)
        {
            Unequip(equipableItem);
        }
    }
    private void ShowTooltip(BaseItemSlot itemSlot)
    {
        if (itemSlot.item != null)
        {
            itemTooltip.ShowTooltip(itemSlot.item);
        }
    }
    private void HideTooltip(BaseItemSlot itemSlot)
    {
        itemTooltip.HideTooltip();
    }
    private void BeginDrag(ItemSlot itemSlot)
    {
        if (itemSlot.item != null)
        {
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            draggedSlot = itemSlot;
            dragableItem.sprite = itemSlot.item.iconItem;
            dragableItem.transform.position = screenPosition;
            dragableItem.enabled = true;
            dragableItem.rectTransform.SetAsLastSibling();
            UIEvent.PlayerDragging(true);

            dropItemArea.gameObject.SetActive(true);
        }
    }
    private void EndDrag(ItemSlot itemSlot)
    {
        draggedSlot = null;
        dropItemArea.gameObject.SetActive(false);
        dragableItem.enabled = false;
        UIEvent.PlayerDragging(false);
        //Debug.Log("vao day");
    }
    private void Drag(ItemSlot itemSlot)
    {
        if (dragableItem.enabled)
        {
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            dragableItem.transform.position = screenPosition;
        }
    }
    private void Drop(ItemSlot dropItemSlot)
    {
        if(draggedSlot == null)
        {
            return;
        }

        if (dropItemSlot.CanAddStack(draggedSlot.item)) //can add stack of dragItemslot.item to dropitemslot  
        {
            AddStacks(dropItemSlot);
        }
        else if (dropItemSlot.CanReceiveItem(draggedSlot.item) && draggedSlot.CanReceiveItem(dropItemSlot.item))
        {
            SwapItems(dropItemSlot);
        }
    }

    private void DropItemOutsideUI()
    {
        if(draggedSlot == null)
        {
            return;
        }
        QuestionDialog.Instance.Show();
        BaseItemSlot baseItemSlot = draggedSlot;
        QuestionDialog.Instance.OnYesEvent += () =>DestroyItemInSlot(baseItemSlot); 
    }

    private void DestroyItemInSlot(BaseItemSlot baseItemSlot)
    {
        baseItemSlot.item.Destroy();
        baseItemSlot.item = null;
    }

    private void SwapItems(ItemSlot dropItemSlot)
    {
        EquipableItem dragItem = draggedSlot.item as EquipableItem;
        EquipableItem dropItem = dropItemSlot.item as EquipableItem;

        if (draggedSlot is EquipmentSlot)
        {
            if (dragItem != null)
            {
                dragItem.Unequip(this);
                if(dragItem.equipmentType == EquipmentType.Weapon)
                {
                    playerWeaponController.UnEquipWeapon();
                }
            }

            if (dropItem != null)
            {
                dropItem.Equip(this);
            }
        }

        if (dropItemSlot is EquipmentSlot)
        {
            if (dragItem != null)
            {
                dragItem.Equip(this);
                if (dragItem.equipmentType == EquipmentType.Weapon)
                {
                    playerWeaponController.EquipWeapon(dragItem);
                }
            }

            if (dropItem != null)
            {
                dropItem.Unequip(this);
            }
        }

        statPanel.UpdateStatValue();

        Item draggedItem = draggedSlot.item;
        int draggedItemAmout = draggedSlot.Amount;

        draggedSlot.item = dropItemSlot.item;
        draggedSlot.Amount = dropItemSlot.Amount;

        dropItemSlot.item = draggedItem;
        dropItemSlot.Amount = draggedItemAmout;

        //Debug.Log()
    }

    private void AddStacks(ItemSlot dropItemSlot)
    {
        //add stack until dropItemSlot is full
        int numberAddableStacks = dropItemSlot.item.maximumStack - dropItemSlot.Amount;
        int stackToAdd = Mathf.Min(numberAddableStacks, draggedSlot.Amount);

        dropItemSlot.Amount += stackToAdd;
        //remove the same number of stack from dragItemSlot 
        draggedSlot.Amount -= stackToAdd;
    }

    public void Equip(EquipableItem item)
    {
        //Debug.Log(item);
        if (inventory.RemoveItem(item))
        {
            EquipableItem previousItem;

            if(equipmentPanel.AddItem(item,out previousItem))
            {
                if(previousItem != null)
                {
                    inventory.AddItem(previousItem);
                    previousItem.Unequip(this);
                    statPanel.UpdateStatValue();
                }

                item.Equip(this);
                if (item.equipmentType == EquipmentType.Weapon)
                {
                    playerWeaponController.EquipWeapon(item);
                }
                
                statPanel.UpdateStatValue();
            }
            else
            {
                inventory.AddItem(item);  
            }
        }
    }

    public void Unequip(EquipableItem equipableItem)
    {
        //Item item = equipableItem as Item;
        if (inventory.CanAddItem(equipableItem) && equipmentPanel.RemoveItem(equipableItem))
        {
            inventory.AddItem(equipableItem);
            equipableItem.Unequip(this);

            if (equipableItem.equipmentType == EquipmentType.Weapon)
            {
                playerWeaponController.UnEquipWeapon();
            }
            statPanel.UpdateStatValue();
        }
    }

    private void ConsumeItem(ConsumableItem consumableItem)
    {
        if (inventory.RemoveItem(consumableItem))
        {
            PotionItem potionItem = consumableItem as PotionItem;
            potionItem.Consume(this);
            statPanel.UpdateStatValue();
        }
    }
    public CraftingRecipe[] craftingRecipe;
    [SerializeField] private CraftRecipeDataBase craftRecipeDataBase;
    private void Craft(BaseItemSlot slotItem)
    {
        for (int i = 0; i < craftingRecipe.Length; i++)
        {
            if(craftingRecipe[i].CanCraft(usingSlot, slotItem))
            {
                craftingRecipe[i].Craft(inventory, usingSlot, slotItem);
                break;
            }
        }

        RedoClickOnCurrency();
    }

    public void UpdateStatValues()
    {
        statPanel.UpdateStatValue();
    }
    private Itemcontainer openItemcontainer;
    private void TransferToItemContainer(BaseItemSlot baseItemSlot)
    {
        Item item = baseItemSlot.item;
        if (item != null && openItemcontainer.CanAddItem(item))
        {
            inventory.RemoveItem(item);
            openItemcontainer.AddItem(item);
        }
    }
    private void TransferToInventory(BaseItemSlot baseItemSlot)
    {
        Item item = baseItemSlot.item;
        if (item != null && inventory.CanAddItem(item))
        {
            openItemcontainer.RemoveItem(item);
            inventory.AddItem(item);
        }
    }
    public void OpenItemcontainer(Itemcontainer itemcontainer)
    {
        openItemcontainer = itemcontainer;
        inventory.OnRightClickEvent -= EquipOrConsume;
        inventory.OnRightClickEvent += TransferToItemContainer;

        itemcontainer.OnRightClickEvent += TransferToInventory;

        itemcontainer.OnPointerEnterEvent += ShowTooltip;
        itemcontainer.OnPointerExitEvent += HideTooltip;
        itemcontainer.OnBeginDragEvent += BeginDrag;
        itemcontainer.OnEndDragEvent += EndDrag;
        itemcontainer.OnDragEvent += Drag;
        itemcontainer.OnDropEvent += Drop;
    }

    public void CloseItemcontainer(Itemcontainer itemcontainer)
    {
        openItemcontainer = null;
        inventory.OnRightClickEvent += EquipOrConsume;
        inventory.OnRightClickEvent -= TransferToItemContainer;

        itemcontainer.OnRightClickEvent -= TransferToInventory;

        itemcontainer.OnPointerEnterEvent -= ShowTooltip;
        itemcontainer.OnPointerExitEvent -= HideTooltip;
        itemcontainer.OnBeginDragEvent -= BeginDrag;
        itemcontainer.OnEndDragEvent -= EndDrag;
        itemcontainer.OnDragEvent -= Drag;
        itemcontainer.OnDropEvent -= Drop;
    }

    public void LoadDataFromSaveFile()
    {
        ItemSaveManager.Instance.LoadEquipment(this);
        ItemSaveManager.Instance.LoadInventory();
        ItemSaveManager.Instance.LoadStash();
    }
    
    public void TakeDamage(int amount, bool isMagicDame)
    {
        if (isImmortal)
        {
            return;
        }

        PlayerAnimationController.Instance.AnimateHurt();

        amount = CalculateDamageRecive(amount, isMagicDame);
        
        playerStats.currentHP -= amount;

        if (playerStats.currentHP <= 0)
        {
            playerStats.currentHP = 0;
            Die();
        }

        UIEvent.HealthChanged(playerStats.currentHP,playerStats.HP.value);
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("player death");
        PlayerAnimationController.Instance.AnimateDeath();
        GetComponent<MMO_Player_movement>().isDead = isDead;
        isImmortal = true;
    }

    private int CalculateDamageRecive(int amount, bool isMagicDame)
    {
        int finalAmount = 0;
        bool isCrit = false;
        if (UnityEngine.Random.Range(0, 100) <= playerStats.CritChance.value)
        {
            isCrit = true;
        }
        else
        {
            isCrit = false;
        }
   
        if (isMagicDame)
        {
            amount = (int)(amount * (1 - playerStats.MagicRes.value));
        }
        else
        {
            float damageMulty = (float)(1 - ((0.052 * playerStats.Armor.value) / (0.9 + 0.048 * Math.Abs(playerStats.Armor.value))));
            amount = (int)(amount - damageMulty);
        }

        if (floatingText)
        {
            ShowFloatingText(amount, isCrit);
        }

        finalAmount = amount;

        return finalAmount;
    }

    [SerializeField] private GameObject floatingText;
    [SerializeField] Transform playerHead;
    private void ShowFloatingText(int amount, bool isCrit)
    {
        var go = Instantiate(floatingText, playerHead.position, Quaternion.identity, transform);
        go.GetComponent<FloatingTextController>().SetText(amount.ToString(), isCrit);
    } 
}
