using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerQuestStage2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag + " " + QuestController.Instance.readyStage2);
        if (other.tag == "Player" && QuestController.Instance.readyStage2 == true)
        {
            Quest Quest = new FindMedicQuest();
            Quest.ExpReward = 200;
            Quest.QuestName = "Help me please";
            Quest.QuestType = "FindMedicQuest";

            if (QuestListSystem.Instance.CheckQuestCurrent(Quest))
            {
                Quest.GiveReward();
                QuestListSystem.Instance.RemoveQuest(Quest);
            }
        }
    }
}
