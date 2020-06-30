
using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    GameObject inventoryGameObject;
    [SerializeField] GameObject characterDetailGameObject;
    [SerializeField] GameObject equipmentGameObject;
    [SerializeField] GameObject[] listTooltip;
    [SerializeField] GameObject menuPanel;

    Animator animMenu;
    bool menuShow;
    [SerializeField] PlayerController playerController;

    [SerializeField] KeyCode[] toggleMenu;
    [SerializeField] KeyCode toggleTestDark;
    //[SerializeField] KeyCode[] toggleEquipmentKeys;

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
                menuShow = !menuShow;
                animMenu.SetBool("isShow", menuShow);
                CheckNeedCursor();
                break;
            }
        }

        if (Input.GetKeyDown(closeAll))
        {
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
        inventoryGameObject.SetActive(!inventoryGameObject.activeSelf);
        if(inventoryGameObject.activeSelf == true) inventoryGameObject.GetComponent<RectTransform>().SetAsLastSibling();
        CheckNeedCursor();
    }
    public void ToggleCharacterDetail()
    {
        characterDetailGameObject.SetActive(!characterDetailGameObject.activeSelf);
        if (characterDetailGameObject.activeSelf == true) characterDetailGameObject.GetComponent<RectTransform>().SetAsLastSibling();
        CheckNeedCursor();
    }
    public void ToggleEquipment()
    {
        equipmentGameObject.SetActive(!equipmentGameObject.activeSelf);
        if (equipmentGameObject.activeSelf == true) equipmentGameObject.GetComponent<RectTransform>().SetAsLastSibling();
        CheckNeedCursor();
    }

    public void LoadDataFromSaveFile()
    {
        LoadManager.instance.LoadData();
        LoadManager.instance.LoadItemData(playerController);
    }

    public void SaveDataToSaveFile()
    {
        LoadManager.instance.SaveData();
        LoadManager.instance.SaveItemData();
    }
}
