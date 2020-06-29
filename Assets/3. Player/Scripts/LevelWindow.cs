
using UnityEngine;
using UnityEngine.UI;

public class LevelWindow : MonoBehaviour
{
    [SerializeField] private Text levelText;
    [SerializeField] private Image imageExp;
    //[SerializeField] private LevelUpSystem levelUpSystem;

    private void Start()
    {
        UIEvent.OnPlayerLevelChange += UpdateLevel;
    }

    private void UpdateLevel(int currentExp, float fillAmount)
    {
       
        levelText.text = currentExp.ToString();
        imageExp.fillAmount = fillAmount;
    }

    public void SetData(int currentExp, float fillAmount)
    {
        levelText.text = currentExp.ToString();
        imageExp.fillAmount = fillAmount;
    }
}
