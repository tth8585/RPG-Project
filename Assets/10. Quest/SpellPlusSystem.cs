using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellPlusSystem : MonoBehaviour
{
    public static SpellPlusSystem Instance { get; set; }
    [SerializeField] GameObject[] buttons;
    [SerializeField] SpellPanel spellPanel;
    [SerializeField] PlayerSpellController playerSpellController;

    int spellPoint = 0;
    int spellPointSpend = 1;
    int currentLevel = 0;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    public void OnClick(int buttonIndex)
    {
        if (playerSpellController.listCurrentSpells[buttonIndex].spellLevel >= 4)
        {
            return;
        }

        playerSpellController.listCurrentSpells[buttonIndex].spellLevel++;
        spellPanel.spellSlots[buttonIndex].spellLevel++;
        spellPanel.UpdateLevelSpellUI();
        spellPoint--;
        spellPointSpend++;
        
        //for (int i = 0; i < buttons.Length; i++)
        //{
        //    buttons[i].gameObject.SetActive(false);
        //}
    }

    public void OnLevelUp(int currentLevel)
    {
        spellPoint = currentLevel - spellPointSpend;
        //spellPoint++;
        this.currentLevel = currentLevel;
        Debug.Log("skillPoint"+ spellPoint);
        //for (int i = 0; i < buttons.Length; i++)
        //{
        //    buttons[i].gameObject.SetActive(true);
        //}
    }

    private void Update()
    {
        if (spellPoint > 0)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if(buttons[i].activeSelf == false)
                {
                    buttons[i].SetActive(true);
                } 
            }
        }
        else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].activeSelf == true)
                {
                    buttons[i].SetActive(false);
                }
            }
        }
    }
}
