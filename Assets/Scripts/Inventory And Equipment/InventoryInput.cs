
using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    GameObject inventoryGameObject;
    [SerializeField] GameObject characterDetailGameObject;
    [SerializeField] GameObject equipmentGameObject;
    [SerializeField] GameObject[] listTooltip;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject helpPanel;

    Animator animMenu;
    bool menuShow;
    [SerializeField] PlayerController playerController;

    [SerializeField] KeyCode[] toggleMenu;
    //[SerializeField] KeyCode toggleTestDark;
    [SerializeField] KeyCode toggleInventoryKey;
    [SerializeField] KeyCode toggleCharacterDetailKey;
    [SerializeField] KeyCode toggleEquipmentKey;
    [SerializeField] KeyCode toggleHelpKey;

    [SerializeField] GameObject testDark;

    [SerializeField] KeyCode closeAll;
    private void Start()
    {
        inventoryGameObject = Inventory.Instance.inventoryPanel;
        menuShow = false;
        animMenu = menuPanel.GetComponent<Animator>();
    }
    private void Update()
    {
        for (int i = 0; i < toggleMenu.Length; i++)
        {
            if (Input.GetKeyDown(toggleMenu[i]))
            {
                //SoundManager.PlaySound(SoundManager.Sound.OpenStash);
                menuShow = !menuShow;
                animMenu.SetBool("isShow", menuShow);
                CheckNeedCursor();
                break;
            }
        }

        if (Input.GetKeyDown(toggleInventoryKey))
        {
            ToggleInventory();
        }

        if (Input.GetKeyDown(toggleCharacterDetailKey))
        {
            ToggleCharacterDetail();
        }

        if (Input.GetKeyDown(toggleEquipmentKey))
        {
            ToggleEquipment();
        }

        if (Input.GetKeyDown(toggleHelpKey))
        {
            ToggleHelp();
        }

        if (Input.GetKeyDown(closeAll))
        {
            //SoundManager.PlaySound(SoundManager.Sound.OpenStash);
            if (inventoryGameObject.activeSelf || characterDetailGameObject.activeSelf || equipmentGameObject.activeSelf)
            {
                SoundManager.PlaySound(SoundManager.Sound.OpenStash);
            }
            inventoryGameObject.SetActive(false);
            characterDetailGameObject.SetActive(false);
            equipmentGameObject.SetActive(false);
            DialogSystem.Instance.HideDialogue();
            ItemStash.Instance.HidePanel();

            if (menuShow == true)
            {
                menuShow = false;
            }
            animMenu.SetBool("isShow", menuShow);

            for(int i=0;i< listTooltip.Length; i++)
            {
                listTooltip[i].SetActive(false);
            }
            CheckNeedCursor();
        }
    }

    private void CheckNeedCursor()
    {
        return;
        if (inventoryGameObject.activeSelf || characterDetailGameObject.activeSelf || equipmentGameObject.activeSelf)
        {
            ShowMouseCursor();
        }else if(!inventoryGameObject.activeSelf || !characterDetailGameObject.activeSelf || !equipmentGameObject.activeSelf)
        {
            HideMouseCursor();
        }
    }
    public void ShowMouseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ToggleInventory()
    {
        SoundManager.PlaySound(SoundManager.Sound.OpenStash);
        inventoryGameObject.SetActive(!inventoryGameObject.activeSelf);
        if(inventoryGameObject.activeSelf == true) inventoryGameObject.GetComponent<RectTransform>().SetAsLastSibling();
        CheckNeedCursor();
    }
    public void ToggleCharacterDetail()
    {
        SoundManager.PlaySound(SoundManager.Sound.OpenStash);
        characterDetailGameObject.SetActive(!characterDetailGameObject.activeSelf);
        if (characterDetailGameObject.activeSelf == true) characterDetailGameObject.GetComponent<RectTransform>().SetAsLastSibling();
        CheckNeedCursor();
    }
    public void ToggleEquipment()
    {
        SoundManager.PlaySound(SoundManager.Sound.OpenStash);
        equipmentGameObject.SetActive(!equipmentGameObject.activeSelf);
        if (equipmentGameObject.activeSelf == true) equipmentGameObject.GetComponent<RectTransform>().SetAsLastSibling();
        CheckNeedCursor();
    }
    public void ToggleHelp()
    {
        helpPanel.SetActive(!helpPanel.activeSelf);
    }
}
