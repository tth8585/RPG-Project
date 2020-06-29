using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedUnitSystem : MonoBehaviour
{
    public static SelectedUnitSystem Instance { get; set; }

    public GameObject selectedUnitPanel;

    Image iconUnit;
    Text nameUnit;
    Image hpSprite;
    int id;
    private void Awake()
    {
        iconUnit = selectedUnitPanel.transform.Find("Target Icon").Find("icon").GetComponent<Image>();
        nameUnit = selectedUnitPanel.transform.Find("Target name").GetComponent<Text>();
        hpSprite = selectedUnitPanel.transform.Find("Target hp").Find("bar").GetComponent<Image>();

        selectedUnitPanel.SetActive(false);

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
        UIEvent.OnEnemyHealthChange += SelectedUnitHPChange;
    }

    public void AddNewUnit(Sprite icon, string npcName, float hpFillAmount, int id)
    {
        this.iconUnit.sprite = icon;
        this.nameUnit.text = npcName;
        hpSprite.fillAmount = hpFillAmount;
        this.id = id;
        selectedUnitPanel.SetActive(true);
    }

    public void HideUnit()
    {
        selectedUnitPanel.SetActive(false);
    }

    void SelectedUnitHPChange(float amount, int ID)
    {
        if(ID == id)
        {
            hpSprite.fillAmount = amount;
        }
    }

    public void ChangeColor(bool isEnemy)
    {
        if (isEnemy)
        {
            nameUnit.color = Color.red;
        }
        else
        {
            nameUnit.color = Color.white;
        }
    }
}
