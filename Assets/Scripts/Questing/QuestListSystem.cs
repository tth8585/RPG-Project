
using UnityEngine;
using UnityEngine.UI;

public class QuestListSystem : MonoBehaviour
{
    public static QuestListSystem Instance { get; set; }

    [SerializeField] GameObject questPanel;
    [SerializeField] GameObject questPref;
    [SerializeField] GameObject contentQ;
   
    private void Awake()
    {
        //questPanel.SetActive(false);

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        UIEvent.OnAddNewQuest += AddQuest;
    }

    void AddQuest(Quest quest)
    {
        if (contentQ != null)
        {
            GameObject questChild = Instantiate(questPref) as GameObject;
            questChild.GetComponent<QuestContentController>().QuestInit(quest);
            questChild.transform.SetParent(contentQ.transform, false);
        }
    }

    public void HidePanel()
    {
        //questPanel.SetActive(false);
    }

    public void RemoveQuest(Quest quest)
    {
        for (int i = contentQ.transform.childCount - 1; i >= 0; i--)
        {
            Transform obj = contentQ.transform.GetChild(i);
            if(obj.GetComponent<QuestContentController>().questName.text == quest.QuestName)
            {
                Destroy(GetComponent(System.Type.GetType(quest.QuestType)));
                Destroy(obj.gameObject);
                CheckToogleUI(quest.QuestName, false, new Vector3(0, 0, 0));
            }
        }
    }

    public void UpdateUI(Quest questUp, bool completed)
    {
        for(int i=0;i< contentQ.transform.childCount; i++)
        {
            if(questUp.QuestName == contentQ.transform.GetChild(i).GetComponent<QuestContentController>().questName.text)
            {
                contentQ.transform.GetChild(i).GetComponent<QuestContentController>().QuestUpdateUI(questUp, completed);
                break;
            }
        }
    }

    public bool CheckQuestCurrent(Quest questCheck)
    {
        for (int i = contentQ.transform.childCount - 1; i >= 0; i--)
        {
            Transform obj = contentQ.transform.GetChild(i);

            if (obj.GetComponent<QuestContentController>().questName.text == questCheck.QuestName)
            {
                return true;
            }
        }
        return false;
    }

    public void CheckToogleUI(string questName,bool isOn, Vector3 goalPos)
    {
        //PointToQuest.Instance.OnOff(isOn);
        //if (!isOn)
        //{
        //    return;
        //}
        //else
        //{
        //    PointToQuest.Instance.UpdateTarget(goalPos);
        //}
        if (isOn)
        {
            PointToQuest.Instance.UpdateTarget(goalPos);
            for (int i = contentQ.transform.childCount - 1; i >= 0; i--)
            {
                Transform obj = contentQ.transform.GetChild(i);

                if (obj.GetComponent<QuestContentController>().questName.text == questName)
                {
                    //obj.GetComponent<QuestContentController>().toggle.isOn = true;
                }
                else
                {
                    obj.GetComponent<QuestContentController>().toggle.isOn = false;
                }
            }
        }
        else
        {
            for (int i = contentQ.transform.childCount - 1; i >= 0; i--)
            {
                Transform obj = contentQ.transform.GetChild(i);

                if (obj.GetComponent<QuestContentController>().toggle.isOn == true)
                {
                    return;
                } 
            }
        }

        PointToQuest.Instance.OnOff(isOn);
    }
}
