
using UnityEngine;

public class QuestGiver : NPC
{
    public bool AssignedQuest { get; set; }
    public bool Helped { get; set; }
    [SerializeField]
    private GameObject quests;
    private Quest Quest { get; set; }
    [SerializeField] private string questType;
    public override void Interact()
    {
        if (!AssignedQuest && !Helped)
        {
            base.Interact();
            //assign
            AssignQuest();
        }
        else if (AssignedQuest && !Helped)
        {
            //check
            CheckQuest();
        }
        else
        {
            DialogSystem.Instance.AddNewDialogue(new string[] { "thx for that stuff", "abc" }, nameNPC, spriteNPC);
        }
    }

    void AssignQuest()
    {
        AssignedQuest = true;
        Quest = (Quest)quests.AddComponent(System.Type.GetType(questType));
        UIEvent.AcceptQuest();
    }

    void CheckQuest()
    {
        if (Quest.Completed)
        {
            Quest.GiveReward();
            Helped = true;
            AssignedQuest = false;
            DialogSystem.Instance.AddNewDialogue(new string[] { "Thanks for that! Here's your reward.","abc"}, nameNPC, spriteNPC);
            QuestListSystem.Instance.HidePanel();
        }
        else
        {
            DialogSystem.Instance.AddNewDialogue(new string[] { "just do it.", "abc" }, nameNPC, spriteNPC);
        }
    }
}
