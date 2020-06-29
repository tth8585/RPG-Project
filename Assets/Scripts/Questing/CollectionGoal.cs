

public class CollectionGoal : Goal
{
    public string ItemID { get; set; }
    public CollectionGoal   (Quest quest, string itemId, string description, bool completed, int currentAmount, int requiredAmount)
    {
        this.Quest = quest;
        this.ItemID = itemId;
        this.Description = description;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requiredAmount;
    }

    public override void Init()
    {
        base.Init();
        //CombatEvent.OnEnemyDeath += ItemPickedUp;
    }

    void ItemPickedUp(Item item)
    {
        if (item.ID == this.ItemID)
        {
            this.CurrentAmount++;
            Evaluate();
        }
    }
}
