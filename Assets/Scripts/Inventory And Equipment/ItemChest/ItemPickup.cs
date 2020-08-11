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

    [SerializeField] KeyCode lootKeycode;
    private bool isInRange;
    RectTransform fKeyHint;

    private void Update()
    {
        if(fKeyHint == null)
        {
            fKeyHint = DialogSystem.Instance.fKeyHint;
        }
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

        if (isInRange && Input.GetKeyDown(lootKeycode))
        {
            Interact();
        }
    }
    public override void Interact()
    {
        SoundManager.PlaySound(SoundManager.Sound.ItemPick);
        AddItem();
        fKeyHint.gameObject.SetActive(false);
    }

    private void AddItem()
    {
        Item itemCopy = itemDrop.GetCopy();
        inventory = Inventory.Instance;
        if (inventory.AddItem(itemCopy))
        {
            CombatEvent.ItemLoot(itemCopy);
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

    private void OnTriggerEnter(Collider other)
    {
        CheckCollision(other.gameObject, true);
    }

    private void OnTriggerExit(Collider other)
    {
        CheckCollision(other.gameObject, false);
    }
    private void CheckCollision(GameObject gameObject, bool state)
    {
        if (gameObject.CompareTag("Player"))
        {
            isInRange = state;
            if (isInRange)
            {
                //show hint
                fKeyHint.gameObject.SetActive(true);// = true;
            }
            else
            {
                fKeyHint.gameObject.SetActive(false);
            }
        }
    }
}
