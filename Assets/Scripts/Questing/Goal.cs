
using System.Numerics;

public class Goal
{
    public Quest Quest { get; set; }
    public string Description { get; set; }
    public bool Completed { get; set; }
    public int CurrentAmount { get; set; }
    public int RequiredAmount { get; set; }

    //public Vector3 posGoal { get; set; }

    public virtual void Init()
    {
        //default init stuff
    }
    public void Evaluate()
    {
        if (CurrentAmount>= RequiredAmount)
        {
            Complete();
        }
        QuestListSystem.Instance.UpdateUI(Quest,Completed);
    }

    public void Complete()
    {
        Quest.CheckGoal();
        Completed = true;
    }
}
