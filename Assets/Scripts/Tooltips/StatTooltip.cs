using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class StatTooltip : MonoBehaviour
{
    public static StatTooltip Instance { get; set; }
    [SerializeField] Text statNameText;
    [SerializeField] Text statModLabelText;
    [SerializeField] Text statModText;

    private StringBuilder stringBuilder = new StringBuilder();

    public GameObject StatTooltipPanel;

    private void Awake()
    {
        statNameText = StatTooltipPanel.transform.Find("NameText").GetComponent<Text>();
        statModLabelText = StatTooltipPanel.transform.Find("Modlabel Text").GetComponent<Text>();
        statModText = StatTooltipPanel.transform.Find("ModsText").GetComponent<Text>();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            //Debug.Log(Instance);
        }
    }
    public void ShowTooltip(BaseStat characterStat, string statName)
    {
        statNameText.text = GetStatTopText(characterStat, statName);
        switch (statName)
        {
            case "STR":
                statNameText.color = Color.red;
                break;
            case "AGI":
                statNameText.color = Color.green;
                break;
            case "INT":
                statNameText.color = Color.blue;
                break;
            default:
                statNameText.color = Color.white;
                break;
        }

        statModText.text = GetStatModText(characterStat, statName);

        StatTooltipPanel.SetActive(true);
    }

    public void HideTooltip()
    {
        StatTooltipPanel.SetActive(false);
    }

    private string GetStatTopText(BaseStat characterStat,string statName)
    {
        stringBuilder.Length = 0;
        stringBuilder.Append(statName);
        stringBuilder.Append(" ");
        stringBuilder.Append(characterStat.value);

        if (characterStat.value != characterStat.baseValue)
        {
            stringBuilder.Append(" (");
            stringBuilder.Append(characterStat.baseValue);
            stringBuilder.Append(" ");

            if (characterStat.value > characterStat.baseValue)
            {
                stringBuilder.Append("+");
            }

            stringBuilder.Append(Math.Round(characterStat.value - characterStat.baseValue,2));
            stringBuilder.Append(")");
        }

        return stringBuilder.ToString();
    }

    private string GetStatModText(BaseStat characterStat, string statName)
    {
        stringBuilder.Length = 0;
        float value;

        foreach (StatModifier mod in characterStat.statModifiers)
        {
            value = (float)Math.Round(mod.value,2);
            if (stringBuilder.Length > 0)
            {
                stringBuilder.AppendLine();
            }

            if (mod.value > 0)
            {  
                if (mod.statModType == StatModType.Flat)
                {
                    stringBuilder.Append("+ ");
                    stringBuilder.Append(value);
                }
                else if(mod.statModType == StatModType.PercentAdd)
                {
                    stringBuilder.Append("+ ");
                    stringBuilder.Append(value * 100);
                    stringBuilder.Append("%");
                }
                else if(mod.statModType == StatModType.PercentMult)
                {
                    stringBuilder.Append("More ");
                    stringBuilder.Append(value * 100);
                    stringBuilder.Append("%");
                }
            }
            else if(mod.value <0)
            {
                value = mod.value * -1;

                if (mod.statModType == StatModType.Flat)
                {
                    stringBuilder.Append("- ");
                    stringBuilder.Append(value);
                }
                else if (mod.statModType == StatModType.PercentAdd)
                {
                    stringBuilder.Append("- ");
                    stringBuilder.Append(value * 100);
                    stringBuilder.Append("%");
                }
                else if (mod.statModType == StatModType.PercentMult)
                {
                    stringBuilder.Append("Less ");
                    stringBuilder.Append(value * 100);
                    stringBuilder.Append("%");
                }
            }
         
            EquipableItem item = mod.source as EquipableItem;

            if (item != null)
            {
                stringBuilder.Append(" ");
                stringBuilder.Append(item.itemName);
            }
            else
            {
                Debug.Log("Mod is not from Equip item");
            }
        }
       
        switch (statName)
        {
            case "STR":
                stringBuilder.Append("1 STR = 20 health.");
                stringBuilder.AppendLine();
                stringBuilder.Append("1 STR = 0.1 health regeneration.");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine("STR heroes + 1 attack damage per point of STR.");
                break;
            case "AGI":
                stringBuilder.Append("1 AGI = 0.16 armor.");
                stringBuilder.AppendLine();
                stringBuilder.Append("1 AGI = 1 attack speed.");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine("AGI heroes + 1 attack damage per point of AGI.");
                break;
            case "INT":
                stringBuilder.Append("1 INT = 12 mana");
                stringBuilder.AppendLine();
                stringBuilder.Append("1 INT = 0.05 mana regeneration.");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine("INT heroes + 1 attack damage per point of INT.");
                break;
            default:
                break;
        }

        return stringBuilder.ToString();
    }
}
