using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemTooltip : MonoBehaviour
{
    public static ItemTooltip Instance { get; set; }
    [SerializeField] Text itemNameText;
    [SerializeField] Text itemSlotText;
    [SerializeField] Text itemStatsText;

    public GameObject itemToolTipPanel;
    private void Awake()
    {
        itemNameText = itemToolTipPanel.transform.Find("NameText").GetComponent<Text>();
        itemSlotText = itemToolTipPanel.transform.Find("SlotText").GetComponent<Text>();
        itemStatsText = itemToolTipPanel.transform.Find("StatsText").GetComponent<Text>();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    public void ShowTooltip(Item item)
    {
        itemNameText.text = item.itemName;
        itemSlotText.text = item.GetItemType();
        itemStatsText.text = item.GetItemDescription();

        itemToolTipPanel.SetActive(true);
    }

    public void HideTooltip()
    {
        itemToolTipPanel.SetActive(false);
    }
}
