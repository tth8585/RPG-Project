
using System.Collections;
using UnityEngine;

public class StartMenuInput : MonoBehaviour
{
    const string HPBAR_ANIM_APPEAR = "barHpAppear";
    const string HPBAR_ANIM_DISAPPEAR = "barHpDisappear";
    const string MINIMAP_ANIM_APPEAR = "miniMapAppear";
    const string MINIMAP_ANIM_DISAPPEAR = "miniMapDisappear";
    const string SPELLBAR_ANIM_APPEAR = "spellUIAppear";
    const string SPELLBAR_ANIM_DISAPPEAR = "spellUIDisappear";
    const string QUEST_ANIM_APPEAR = "quetsAppear";
    const string QUEST_ANIM_DISAPPEAR = "questDisappear";

    const string POPUPSTARTMENU_ANIM_APPEAR = "RightToLeft";
    const string POPUPSTARTMENU_ANIM_DISAPPEAR = "LeftToRight";

    [SerializeField] GameObject pressAnyKey;
    [SerializeField] GameObject popupStartMenu;
    [SerializeField] private GameObject settingPanel;

    [SerializeField] private GameObject startMenu;

    [SerializeField] private GameObject _uiHpBar;
    [SerializeField] private GameObject _uiMiniMap;
    [SerializeField] private GameObject _uiSpellBar;
    [SerializeField] private GameObject _uiQuestPanel;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            startMenu.SetActive(!startMenu.activeSelf);

            if(startMenu.activeSelf == true)
            {
                HideUI();
            }
            else
            {
                ShowUI();
            }
        }

        if (Input.anyKeyDown)
        {
            if (pressAnyKey.activeSelf)
            {
                pressAnyKey.SetActive(false);
                popupStartMenu.GetComponent<Animator>().Play(POPUPSTARTMENU_ANIM_APPEAR);
            }
        }
    }

    private void ShowUI()
    {
        _uiHpBar.GetComponent<Animator>().Play(HPBAR_ANIM_APPEAR);
        _uiMiniMap.GetComponent<Animator>().Play(MINIMAP_ANIM_APPEAR);
        _uiSpellBar.GetComponent<Animator>().Play(SPELLBAR_ANIM_APPEAR);
        _uiQuestPanel.GetComponent<Animator>().Play(QUEST_ANIM_APPEAR);
    }

    private void HideUI()
    {
        _uiHpBar.GetComponent<Animator>().Play(HPBAR_ANIM_DISAPPEAR);
        _uiMiniMap.GetComponent<Animator>().Play(MINIMAP_ANIM_DISAPPEAR);
        _uiSpellBar.GetComponent<Animator>().Play(SPELLBAR_ANIM_DISAPPEAR);
        _uiQuestPanel.GetComponent<Animator>().Play(QUEST_ANIM_DISAPPEAR);
    }

    public void StartGame()
    {
        StartCoroutine(GameOn());
    }

    public void Options()
    {
        //popupStartMenu.GetComponent<Animator>().Play(POPUPSTARTMENU_ANIM_DISAPPEAR);
        //settingPanel.GetComponent<Animator>().Play(POPUPSTARTMENU_ANIM_DISAPPEAR);
        settingPanel.SetActive(!settingPanel.activeSelf);
    }

    private IEnumerator GameOn()
    {
        yield return new WaitForSeconds(0.5f);
        startMenu.SetActive(!startMenu.activeSelf);
        ShowUI();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
