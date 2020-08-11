using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public static QuestController Instance { get; set; }

    [SerializeField] GameObject questStage2Obj;
    [SerializeField] GameObject questStage1Obj;

    public bool readyStage2;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void DoneStage1()
    {
        readyStage2 = true;
        questStage2Obj.SetActive(true);
        questStage1Obj.SetActive(false);
    }
}
