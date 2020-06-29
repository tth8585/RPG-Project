using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private BaseStat _characterStat;
    public BaseStat characterStat 
    {
        get { return _characterStat; }
        set
        {
            _characterStat = value;
            UpdateStatValue();
        }
    }

    private string _nameStat;
    public string nameStat 
    {
        get { return _nameStat; }
        set
        {
            _nameStat = value;
            textName.text = _nameStat;
        }
    }

    [SerializeField] Text textName;
    [SerializeField] Text textValue;

    private StatTooltip statTooltip;
    protected bool isPointerOver;

    private void Awake()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        textName = texts[0];
        textValue = texts[1];
        textName.color = Color.clear;

    }

    private void Start()
    {
        statTooltip = StatTooltip.Instance;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true;
        statTooltip.ShowTooltip(characterStat, nameStat);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;
        statTooltip.HideTooltip();
    }

    public void UpdateStatValue()
    {
        textValue.text = _characterStat.value.ToString();
    }

    protected virtual void OnDisable()
    {
        if (isPointerOver)
        {
            OnPointerExit(null);
        }
    }
}
