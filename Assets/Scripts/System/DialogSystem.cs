using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public static DialogSystem Instance { get; set; }
    private List<string> dialogueLines = new List<string>();
    private string nameNpc;

    public GameObject dialoguePanel;

    Button contiBtn;
    Button exitBtn;

    Text dialogueText, nameText;
    int dialogueIndex;
    Image imageNPC;
    private void Awake()
    {
        contiBtn = dialoguePanel.transform.Find("Continue").GetComponent<Button>();
        contiBtn.onClick.AddListener(delegate { ContinueDialogue(); });

        exitBtn = dialoguePanel.transform.Find("Exit").GetComponent<Button>();
        exitBtn.onClick.AddListener(delegate { HideDialogue(); });

        dialogueText = dialoguePanel.transform.Find("Text").GetComponent<Text>();
        nameText = dialoguePanel.transform.Find("Name").Find("Name Text").GetComponent<Text>();
        imageNPC = dialoguePanel.transform.Find("Icon").Find("Inside").GetComponent<Image>();

        dialoguePanel.SetActive(false);

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddNewDialogue(string[] lines, string npcName, Sprite npcSprite)
    {
        dialogueIndex = 0;
        this.nameNpc = npcName;
        imageNPC.sprite = npcSprite;
        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);
        CreateDialogue();
    }

    public void CreateDialogue()
    {
        dialogueText.text = dialogueLines[dialogueIndex];
        nameText.text = nameNpc;
        dialoguePanel.SetActive(true);
    }

    public void ContinueDialogue()
    {
        if (dialogueIndex < dialogueLines.Count -1)
        {
            dialogueIndex++;
            dialogueText.text = dialogueLines[dialogueIndex];
        }
        else
        {
            dialoguePanel.SetActive(false);
        }
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}
