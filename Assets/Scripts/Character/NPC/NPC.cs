
using UnityEngine;

public class NPC : Interactable
{
    public string[] dialogue;
    public string nameNPC;
    public Sprite spriteNPC;

    [SerializeField] KeyCode lootKeycode;
    private bool isInRange;
    private RectTransform fKeyHint;
    public override void Update()
    {
        base.Update();
        if (fKeyHint == null)
        {
            fKeyHint = DialogSystem.Instance.fKeyHint;
        }

        if (isInRange && Input.GetKeyDown(lootKeycode))
        {
            Interact();
        }
    }

    public override void Interact()
    {
        CombatEvent.MeetNPC(nameNPC);

        DialogSystem.Instance.AddNewDialogue(dialogue, nameNPC, spriteNPC); 
        Debug.Log("interacting with NPC");
        if (nameNPC == "Jarsha")
        {
            QuestController.Instance.DoneStage1();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckCollision(other.gameObject, true);
    }

    private void OnTriggerExit(Collider other)
    {
        CheckCollision(other.gameObject, false);
    }
    private void CheckCollision(GameObject gameObject, bool state)
    {
        if (gameObject.CompareTag("Player"))
        {
            isInRange = state;
            if (isInRange)
            {
                //show hint
                fKeyHint.gameObject.SetActive(true);// = true;
            }
            else
            {
                fKeyHint.gameObject.SetActive(false);
            }
        }
    }
}
