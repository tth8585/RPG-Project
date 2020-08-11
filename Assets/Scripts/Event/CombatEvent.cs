using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEvent : MonoBehaviour
{
    public delegate void EnemyEventHandler(IEnemy enemy);
    public static event EnemyEventHandler OnEnemyDeath;

    public delegate void QuestEventHandler(int questExp);
    public static event QuestEventHandler OnQuestComplete;

    public delegate void NPCEventHandler(string npcName);
    public static event NPCEventHandler OnNPCInteract;

    public delegate void ItemQuestEventHandler(Item item);
    public static event ItemQuestEventHandler OnPickupItem;
    public static void EnemyDied(IEnemy enemy)
    {
        if (OnEnemyDeath != null)
        {
            OnEnemyDeath(enemy);
        }
    }

    public static void QuestDone(int questExp)
    {
        if (OnQuestComplete != null)
        {
            OnQuestComplete(questExp);
        }
    }

    public static void MeetNPC(string npcName)
    {
        if (OnNPCInteract != null)
        {
            OnNPCInteract(npcName);
        }
    }

    public static void ItemLoot(Item item)
    {
        if (OnPickupItem != null)
        {
            OnPickupItem(item);
        }
    }
}
