


public class KillGoal: Goal
{
    public int EnemyID { get; set; }
    public KillGoal(Quest quest, int enemyId, string description, bool completed, int currentAmount, int requiredAmount)
    {
        this.Quest = quest;
        this.EnemyID = enemyId;
        this.Description = description;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requiredAmount;
    }

    public override void Init()
    {
        base.Init();
        CombatEvent.OnEnemyDeath += EnemyDied;
    }

    void EnemyDied(IEnemy enemy)
    {     
        if (enemy.ID == this.EnemyID)
        {
            this.CurrentAmount++;
            Evaluate();
        }
    }
}
