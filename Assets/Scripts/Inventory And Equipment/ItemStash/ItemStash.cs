using UnityEngine;

public class ItemStash : Itemcontainer
{
    public static ItemStash Instance { get; set; }
    //[SerializeField] Inventory inventory;
    [SerializeField] KeyCode openKeycode;
    SpriteRenderer spriteRenderer;

    Transform itemParent;
    private PlayerController character;

    private bool isOpen;
    private bool isInRange;
    private Color color = new Color(1, 1, 1, 0.4f);

    public GameObject itemStashPanel;
  
    protected override void Awake()
    {
        itemParent = itemStashPanel.transform.Find("Grid Zone").Find("Item Slots Grid");

        if (itemParent != null)
        {
            itemSlots = itemParent.GetComponentsInChildren<ItemSlot>(includeInactive: true);
        }

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>(includeInactive: true);
        }

        spriteRenderer.enabled = false;
        spriteRenderer.color = color;

        base.Awake();
        itemStashPanel.SetActive(false);

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Update()
    {
        if (isInRange && Input.GetKeyDown(openKeycode))
        {
            isOpen = !isOpen;
            itemStashPanel.SetActive(isOpen);
            itemStashPanel.GetComponent<RectTransform>().SetAsLastSibling();

            if (isOpen)
            {
                character.OpenItemcontainer(this);
            }
            else
            {
                character.CloseItemcontainer(this);
            }
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

            if (!isInRange && isOpen)
            {
                isOpen = false;
                itemStashPanel.SetActive(false);
                character.CloseItemcontainer(this);
            }

            if (isInRange)
            {
                character = gameObject.GetComponent<PlayerController>();
            }
            else
            {
                character = null;
            }
        }
    }

    public void HidePanel()
    {
        isOpen = !isOpen;
        itemStashPanel.SetActive(false);
    }
}
