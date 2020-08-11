using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class FindHerbalQuest : Quest
{
    [SerializeField] Item item;
    private StringBuilder stringBuilder = new StringBuilder();
    private void OnEnable()
    {
        QuestType = "FindHerbalQuest";
        QuestName = "The grass is everywhere";
        Description = GetDesscription();
        ItemReward = item;
        ExpReward = 200;

        Goals.Add(new CollectionGoal(this, "754d5ddf24aeb5a4e9ac9ce45cb9ae34", "xxx", false, 0, 3));
        Goals.ForEach(g => g.Init());
    }

    string GetDesscription()
    {
        stringBuilder.Length = 0;
        stringBuilder.Append("The slime dropped grass");
        return stringBuilder.ToString();
    }
}
