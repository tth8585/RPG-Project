
using UnityEngine;
using UnityEngine.UI;

public class QuestListSystem : MonoBehaviour
{
    public static QuestListSystem Instance { get; set; }

    [SerializeField] GameObject questPanel;
    [SerializeField] Text questName;
    [SerializeField] Text questDiscription;
    [SerializeField] Text questGoal;
    Quest quest;
    const string COMPLETED_STRING = "(Completed)";
    const string SYS_ONE = "(";
    const string SYS_TWO = " /";
    const string SYS_THREE = ")";
    const string SYS_BLANK = "";
    private void Awake()
    {
        //questPanel.SetActive(false);

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
        UIEvent.OnAddNewQuest += AddQuest;
        CheckQuestCurrent();
    }

    void AddQuest()
    {
        quest = GetComponent<Quest>();

        questName.text = quest.QuestName;
        questDiscription.text = quest.Description;
        questGoal.text = "(" + quest.Goals[0].CurrentAmount + " /" + quest.Goals[0].RequiredAmount + ")";
        //questPanel.SetActive(true);
    }

    public void HidePanel()
    {
        //questPanel.SetActive(false);
    }

    public void UpdateUI(bool completed)
    {
        //Debug.Log(enemy.ID);
        if (completed)
        {
            questGoal.text = COMPLETED_STRING;
        }
        else
        {
            questGoal.text = SYS_ONE + quest.Goals[0].CurrentAmount + SYS_TWO + quest.Goals[0].RequiredAmount + SYS_THREE;
        }
    }

    public void CheckQuestCurrent()
    {
        Quest currentQuest = GetComponent<Quest>();

        if(currentQuest == null)
        {
            questName.text = SYS_BLANK;
            questDiscription.text = SYS_BLANK;
            questGoal.text = SYS_BLANK;
        }
    }
}
