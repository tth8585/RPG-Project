
using UnityEngine;

public class NPC : Interactable
{
    public string[] dialogue;
    public string nameNPC;
    public Sprite spriteNPC;
    public override void Interact()
    {
        DialogSystem.Instance.AddNewDialogue(dialogue, nameNPC, spriteNPC); 
        Debug.Log("interacting with NPC");
    }
}
