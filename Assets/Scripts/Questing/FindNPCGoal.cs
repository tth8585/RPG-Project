using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNPCGoal : Goal
{
    public string npcName { get; set; }
    public FindNPCGoal(Quest quest, string npcName, string description, bool completed, int currentAmount, int requiredAmount)
    {
        this.Quest = quest;
        this.npcName = npcName;
        this.Description = description;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requiredAmount;
    }

    public override void Init()
    {
        base.Init();
        CombatEvent.OnNPCInteract += InteractNPC;
    }

    void InteractNPC(string npcName)
    {
        if (npcName == this.npcName)
        {
            this.CurrentAmount++;
            Evaluate();
        }
    }
}
