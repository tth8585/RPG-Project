
using UnityEngine;
using UnityEngine.UI;

public class LevelWindow : MonoBehaviour
{
    public static LevelWindow Instance { get; set; }
    [SerializeField] private Text levelText;
    [SerializeField] private Image imageExp;
    //[SerializeField] private LevelUpSystem levelUpSystem;
    int levelPlayer =0;
    int expPlayer;
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
        UIEvent.OnPlayerLevelChange += UpdateLevel;
    }

    private void UpdateLevel(int currentExp, float fillAmount)
    {
        if (levelText != null)
        {
            Debug.Log("updateUI???");
            levelText.text = currentExp.ToString();
        }

        if (imageExp != null)
        {
            imageExp.fillAmount = fillAmount;
        }
    }

    public void SetData(int currentExp, float fillAmount)
    {
        levelText.text = currentExp.ToString();
        imageExp.fillAmount = fillAmount;
    }

    public void UpdateLevelUI(int level, float fillAmount)
    {
        //levelText.text = levelPlayer.ToString();
    }
}
