using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInput : MonoBehaviour
{
    public static UIInput Instance { get; set; }

    const string HPBAR_ANIM_APPEAR = "barHpAppear";
    const string HPBAR_ANIM_DISAPPEAR = "barHpDisappear";
    const string MINIMAP_ANIM_APPEAR = "miniMapAppear";
    const string MINIMAP_ANIM_DISAPPEAR = "miniMapDisappear";
    const string SPELLBAR_ANIM_APPEAR = "spellUIAppear";
    const string SPELLBAR_ANIM_DISAPPEAR = "spellUIDisappear";
    const string QUEST_ANIM_APPEAR = "quetsAppear";
    const string QUEST_ANIM_DISAPPEAR = "questDisappear";
    const string HELP_ANIM_APPEAR = "helpAppear";
    const string HELP_ANIM_DISAPPEAR = "helpDisappear";

    [SerializeField] private GameObject _uiHpBar;
    [SerializeField] private GameObject _uiMiniMap;
    [SerializeField] private GameObject _uiSpellBar;
    [SerializeField] private GameObject _uiQuestPanel;
    [SerializeField] private GameObject _uiHelpBtn;

    [SerializeField] GameObject OptionInGame;
    [SerializeField] private GameObject settingPanel;

    [SerializeField] PlayerController playerController;
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
    private void Start()
    {
        ShowUI();
        InitMainGame();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowOptionIngame();
        }
    }
    public void ShowUI()
    {
        _uiHpBar.GetComponent<Animator>().Play(HPBAR_ANIM_APPEAR);
        _uiMiniMap.GetComponent<Animator>().Play(MINIMAP_ANIM_APPEAR);
        _uiSpellBar.GetComponent<Animator>().Play(SPELLBAR_ANIM_APPEAR);
        _uiQuestPanel.GetComponent<Animator>().Play(QUEST_ANIM_APPEAR);
        _uiHelpBtn.GetComponent<Animator>().Play(HELP_ANIM_APPEAR);
    }

    public void ShowOptionIngame()
    {
        OptionInGame.SetActive(!OptionInGame.activeSelf);
        if (OptionInGame.activeSelf == false && settingPanel.activeSelf == true)
        {
            settingPanel.SetActive(false);
        }
    }

    public void ShowSetting()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);
        settingPanel.transform.SetAsLastSibling();
        //LoadManager.instance.SaveData();
        if (settingPanel.activeSelf == false)
        {
            LoadManager.instance.SetVolume();
        }
    }

    public void SaveDataToSaveFile()
    {
        LoadManager.instance.SaveData();
        LoadManager.instance.SaveItemData();
        PlayFakeSave();
    }

    public void BackToStartScene()
    {
        Loader.Load(Loader.Scene.StartMenu);
    }

    private void InitMainGame()
    {
        LoadData(playerController);
        LoadSound();

        StatPanel.Instance.UpdateStatValue();
        //LevelWindow.Instance.UpdateLevelUI();
    }

    void LoadData(PlayerController c)
    {
        LoadManager.instance.LoadData();
        LoadManager.instance.LoadItemData(c);
    }

    void LoadSound()
    {
        BGMController.Instance.PlaySound(BGMController.Music.Ocean);
    }

    [SerializeField] GameObject lastword;
    [SerializeField] GameObject playerDie;
    public void DieBtn(int indexBtn)
    {
        if(indexBtn == 0)
        {
            lastword.SetActive(true);
            playerDie.SetActive(false);
        }
        else
        {
            //indexBtn ==1
            Loader.Load(Loader.Scene.StartMenu);
        }
    }

    [SerializeField] GameObject fakeSave;
    public void PlayFakeSave()
    {
        fakeSave.GetComponent<Animator>().Play("fakeSave");
    }
}
