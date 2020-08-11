using UnityEngine;

public class LevelUpSystem : MonoBehaviour
{
    public int currentLevel = 1;
    public int baseXp = 20;
    public int currentXP=0;

    private int xpForNextLevel;
    private int xpDifferenceToNextLevel;
    private int totalXpDifference;

    private float fillAmount;
    public float reverseFillAmount = 0;

    [SerializeField] private GameObject levelUpVFX;
    [SerializeField] private Transform levelUpVFXPos;

   // [SerializeField] LevelWindow levelWindow;
    private void Awake()
    {
        //SetValue();
    }
    private void Start()
    {
        CombatEvent.OnEnemyDeath += EnemytoExperience;
        CombatEvent.OnQuestComplete += QuesttoExperience;
    }
    
    public void AddXP(int amount)
    {
        CalculateLevel(amount);
    }

    void CalculateLevel(int amout)
    {
        currentXP += amout;
        
        int temp_cur_level = (int)Mathf.Sqrt(currentXP / baseXp) +1;
        
        if (currentLevel != temp_cur_level && temp_cur_level < 20)
        {
            currentLevel = temp_cur_level;
            PlayerStats.Instance.LevelUp();
            
            if(levelUpVFX!=null&& levelUpVFXPos != null)
            {
                Instantiate(levelUpVFX, levelUpVFXPos.position, Quaternion.identity);
            }
            
            SpellPlusSystem.Instance.OnLevelUp(currentLevel);

            SoundManager.PlaySound(SoundManager.Sound.PlayerLevelUp);
        }

        xpForNextLevel = baseXp * currentLevel * currentLevel;
        xpDifferenceToNextLevel = xpForNextLevel - currentXP;
        totalXpDifference = xpForNextLevel - (baseXp * (currentLevel - 1) * (currentLevel - 1));

        fillAmount = (float)xpDifferenceToNextLevel / (float)totalXpDifference;
        reverseFillAmount = 1 - fillAmount;

        UIEvent.PlayerLevelChange(currentLevel,reverseFillAmount);
        //LevelWindow.Instance.UpdateLevelUI(currentLevel,reverseFillAmount);
    }

    public void EnemytoExperience(IEnemy enemy)
    {
        AddXP(enemy.Experience);
    }

    public void QuesttoExperience(int questExp)
    {
        AddXP(questExp);
    }
}
