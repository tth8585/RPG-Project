
using UnityEngine;

public class StatPanel : MonoBehaviour
{
    public static StatPanel Instance { get; set; }
    [SerializeField] StatDisplay[] statDisplays;
    [SerializeField] string[] statNames;

    private BaseStat[] characterStat;
    public GameObject statPanel;
    private void Awake()
    {
        statDisplays = statPanel.transform.GetComponentsInChildren<StatDisplay>();
        UpdateStatName();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void SetStats(params BaseStat[] listStats)
    {
        this.characterStat = listStats;

        if (characterStat.Length > statDisplays.Length)
        {
            Debug.Log("not enough stat display");
            return;
        }

        for(int i = 0; i < statDisplays.Length; i++)
        {
            statDisplays[i].gameObject.SetActive(i < statDisplays.Length);

            if(i< listStats.Length)
            {
                statDisplays[i].characterStat = listStats[i];
            }
        }
    }

    public void UpdateStatValue()
    {
        for(int i = 0; i < characterStat.Length; i++)
        {
            statDisplays[i].UpdateStatValue();
        }
    }

    public void UpdateStatName()
    {
        for (int i = 0; i < statNames.Length; i++)
        {
            statDisplays[i].nameStat = statNames[i] + " - ";
        }
    }
}
