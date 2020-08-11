using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestContentController : MonoBehaviour
{
    public Toggle toggle;
    public Text questName;
    [SerializeField] Text questDiscription;
    [SerializeField] Text questGoal;
    public Vector3 goalPos;

    const string COMPLETED_STRING = "(Completed)";
    const string SYS_ONE = "(";
    const string SYS_TWO = " /";
    const string SYS_THREE = ")";
    const string SYS_BLANK = "";

    private void Start()
    {
        toggle.onValueChanged.AddListener(ToggleChanged);
    }
    public void QuestInit(Quest quest)
    {
        questName.text = quest.QuestName;
        questDiscription.text = quest.Description;
        questGoal.text = SYS_ONE + quest.Goals[0].CurrentAmount + SYS_TWO + quest.Goals[0].RequiredAmount + SYS_THREE;
        goalPos = quest.posQuestTarget;
    }

    public void QuestUpdateUI(Quest questUp, bool completed)
    {
        if (completed)
        {
            questGoal.text = COMPLETED_STRING;
        }
        else
        {
            questGoal.text = SYS_ONE + questUp.Goals[0].CurrentAmount + SYS_TWO + questUp.Goals[0].RequiredAmount + SYS_THREE;
        }
    }

    private void ToggleChanged(bool isOn)
    {
        QuestListSystem.Instance.CheckToogleUI(questName.text, isOn, goalPos);
    }
}
