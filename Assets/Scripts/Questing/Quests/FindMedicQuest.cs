using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class FindMedicQuest : Quest
{
    [SerializeField] Item item;
    private StringBuilder stringBuilder = new StringBuilder();
    private void OnEnable()
    {
        QuestType = "FindMedicQuest";
        QuestName = "Help me please";
        Description = GetDesscription();
        ItemReward = item;
        ExpReward = 200;

        Goals.Add(new FindNPCGoal(this, "Jarsha", "xxx",false,0,1));
        Goals.ForEach(g => g.Init());
    }

    string GetDesscription()
    {
        stringBuilder.Length = 0;
        stringBuilder.Append("Find Jarsha the medic");
        return stringBuilder.ToString();
    }
}
