using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSlayerQuest : Quest
{
    [SerializeField] Item item;
    private void Start()
    {
        //QuestName = "Slime Slayer";
        //Description = "Basic for newbie";
        //ItemReward = item;
        //ExpReward = 200;

        //Goals.Add(new KillGoal(this, 0, "Kill 3 Slime", false, 0, 5));
        //Goals.ForEach(g => g.Init());
    }

    private void OnEnable()
    {
        QuestName = "Slime Slayer";
        Description = "Basic for newbie";
        ItemReward = item;
        ExpReward = 200;

        int slimeID = 0;
        Goals.Add(new KillGoal(this, slimeID, "Kill 3 Slime", false, 0, 5));
        Goals.ForEach(g => g.Init());
    }

    public void QuestActive()
    {
        //QuestName = "Slime Slayer";
        //Description = "Basic for newbie";
        //ItemReward = item;
        //ExpReward = 200;

        //Goals.Add(new KillGoal(this, 0, "Kill 3 Slime", false, 0, 5));
        //Goals.ForEach(g => g.Init());
    }
}
