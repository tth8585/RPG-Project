

using System.Collections;
using UnityEngine;


public class StartMenuInput : MonoBehaviour
{
    public static StartMenuInput Instance { get; set; }
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

    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 originCamPos;
    Vector3 _velocity = Vector3.zero;
    float movespeed = 100f;
    bool startMove = false;
    [SerializeField] public GameObject imageGo;
    [SerializeField] public GameObject imageGoMain;

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
        originCamPos = cam.transform.position;
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    startMenu.SetActive(!startMenu.activeSelf);

        //    if(startMenu.activeSelf == true)
        //    {
        //        HideUI();
        //    }
        //    else
        //    {
        //        ShowUI();
        //    }
        //}

        if (Input.anyKeyDown)
        {
            if (pressAnyKey.activeSelf)
            {
                pressAnyKey.SetActive(false);
                popupStartMenu.GetComponent<Animator>().Play(POPUPSTARTMENU_ANIM_APPEAR);
            }
        }

        float distance = Vector3.Distance(target.position, cam.transform.position);

        if (startMove == true && distance > 1f)
        {
            cam.transform.position = Vector3.SmoothDamp(cam.transform.position, target.position, ref _velocity, Time.deltaTime * movespeed);
        }
        else if(startMove == true && distance <= 1f)
        {
            UIEvent.NewGame();
            cam.transform.position = originCamPos;
            startMove = false;
        }
    }

    public void ShowUI()
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

    public void Options()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);
        //LoadManager.instance.SaveData();
        if(settingPanel.activeSelf == false)
        {
            LoadManager.instance.SetVolume();
        }
    }

    private void HideOptions()
    {
        settingPanel.SetActive(false);
        //LoadManager.instance.SaveData();
        LoadManager.instance.SetVolume();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void NewGame()
    {
        HideOptions();
        if (startMove == false)
        {
            startMove = true;
        }
        imageGo.GetComponent<Animator>().Play("FadeIn");
    }

    public void ContinueGame()
    {
        HideOptions();

        imageGo.GetComponent<Animator>().Play("FadeIn");
        StartCoroutine(NewGameFadeIn()); 
    }

    private IEnumerator NewGameFadeIn()
    {
        yield return new WaitForSeconds(4f);
        //imageGo.GetComponent<Animator>().Play("FadeOut");
        ShowUI();
        imageGoMain.GetComponent<Animator>().Play("FadeOut");

        yield return new WaitForSeconds(0.1f);
        startMenu.SetActive(false);
        BGMController.Instance.PlaySound(BGMController.Music.Ocean);
    }
}
