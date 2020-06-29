using UnityEngine;

public class ItemChest : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] int amount =1;
    [SerializeField] Inventory inventory;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] KeyCode itemPickupKeycode;

    private Color color = new Color(1, 1, 1, 0.4f);
    private Color disableColor = Color.clear;
    private bool isInRange;
    private bool isEmpty;
    private void OnValidate()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>(includeInactive: true);
        }

        spriteRenderer.enabled = false;
        spriteRenderer.color = color;
    }
    private void Update()
    {
        if (isInRange && Input.GetKeyDown(itemPickupKeycode) && !isEmpty)
        {
            Item itemCopy = item.GetCopy();
            if (inventory.AddItem(itemCopy))
            {
                amount--;
                if (amount == 0)
                {
                    isEmpty = true;
                    spriteRenderer.color = disableColor;
                }
            }
            else
            {
                itemCopy.Destroy();
            }  
        }
        else if(isInRange && Input.GetKeyDown(itemPickupKeycode) && isEmpty)
        {
            Debug.Log("empty chest");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        CheckCollision(other.gameObject, true);
    }

    private void OnTriggerExit(Collider other)
    {
        CheckCollision(other.gameObject, false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollision(collision.gameObject, true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        CheckCollision(collision.gameObject, false);
    }

    private void CheckCollision(GameObject gameObject, bool state)
    {
        if (gameObject.CompareTag("Player"))
        {
            isInRange = state;
            spriteRenderer.enabled = state;
        }
    }
}
