using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public List<Goal> Goals { get; set; } = new List<Goal>();
    public string QuestType { get; set; }
    public Vector3 posQuestTarget { get; set; }
    public string QuestName { get; set; }
    public string Description { get; set; }
    public int ExpReward { get; set; }
    public Item ItemReward { get; set; }
    public bool Completed { get; set; }

    public void CheckGoal()
    {
        //Completed = Goals.All(g => g.Completed);
        for(int i = 0; i < Goals.Count; i++)
        {
            if (Goals[i].Completed == false)
            {
                return;
            }
        }

        Completed = true;
    }

    public void GiveReward()
    {
        if (ItemReward != null)
        {
            if (Inventory.Instance.CanAddItem(ItemReward))
            {
                Inventory.Instance.AddItem(ItemReward);
                CombatEvent.ItemLoot(ItemReward);
            }
        }

        CombatEvent.QuestDone(ExpReward);
    }
}
