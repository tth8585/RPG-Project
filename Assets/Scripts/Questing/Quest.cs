using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public List<Goal> Goals { get; set; } = new List<Goal>();
    public string QuestName { get; set; }
    public string Description { get; set; }
    public int ExpReward { get; set; }
    public Item ItemReward { get; set; }
    public bool Completed { get; set; }

    public void CheckGoal()
    {
        Completed = Goals.All(g => g.Completed);
    }

    public void GiveReward()
    {
        if (ItemReward != null)
        {
            if (Inventory.Instance.CanAddItem(ItemReward))
            {
                Inventory.Instance.AddItem(ItemReward);
            }
        }
    }
}
