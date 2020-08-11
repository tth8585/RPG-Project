
using System;
using UnityEngine;

public class QuestGiver : NPC
{
    public bool AssignedQuest { get; set; }
    public bool Helped { get; set; }
    [SerializeField]
    private GameObject quests;
    protected Quest Quest { get; set; }
    [SerializeField] private string questType;

    [SerializeField] private GameObject questState1;
    [SerializeField] private GameObject questState2;

    [SerializeField] Vector3 posQuestPoint;

    private void Start()
    {
        if (questState1 != null)
        {
            questState1.SetActive(true);
        }
    }
    public override void Interact()
    {
        if (!AssignedQuest && !Helped)
        {
            base.Interact();
            //assign
            AssignQuest();
            if (questState2 != null)
            {
                if(nameNPC == "Dana")
                {
                    SoundManager.PlaySound(SoundManager.Sound.HelpMe);
                    Inventory.Instance.SetStartingItems();
                }
                questState2.SetActive(true);
                questState1.SetActive(false);
            }
        }
        else if (AssignedQuest && !Helped)
        {
            //check
            CheckQuest();
        }
        else
        {
            DialogSystem.Instance.AddNewDialogue(new string[] { "Thanks for helping." }, nameNPC, spriteNPC);
        }
    }

    void AssignQuest()
    {
        AssignedQuest = true;
        //add quest
        Quest = (Quest)quests.AddComponent(System.Type.GetType(questType));
        Quest.posQuestTarget = posQuestPoint;
        UIEvent.AcceptQuest(Quest);
    }

    void CheckQuest()
    {
        if (Quest.Completed)
        {
            Quest.GiveReward();
            Helped = true;
            AssignedQuest = false;
            DialogSystem.Instance.AddNewDialogue(new string[] { "Thanks for that! Here's your reward."}, nameNPC, spriteNPC);
            
            QuestListSystem.Instance.RemoveQuest(Quest);

            if (questState2.activeSelf == true)
            {
                questState2.SetActive(false);
            }
        }
        else
        {
            DialogSystem.Instance.AddNewDialogue(new string[] { "Just do it.",}, nameNPC, spriteNPC);
        }
    }
}
