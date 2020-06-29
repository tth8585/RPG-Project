using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item itemDrop;
    private Inventory inventory;
    public Material material;
    private float currentTime = 5;
    private float timeToBlink = 3;
    private float timer = 0;

    private void Update()
    {
        currentTime -= Time.deltaTime;
        
        if (currentTime <= timeToBlink)
        {
            timer += Time.deltaTime;
            if(timer >= 0.5)
            {
                ItemBlink1();
            }
            if (timer >= 1)
            {
                ItemBlink2();
                timer = 0;
            }
        }
        if (currentTime <= 0)
        {
            Destroy(gameObject);
        }
    }
    public override void Interact()
    {
        AddItem();
        Debug.Log("loot");
    }

    private void AddItem()
    {
        Item itemCopy = itemDrop.GetCopy();
        inventory = Inventory.Instance;
        if (inventory.AddItem(itemCopy))
        {
            Destroy(gameObject);
        }
    }

    private void ItemBlink1()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
    }

    private void ItemBlink2()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
