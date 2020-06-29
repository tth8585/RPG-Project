using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusType
{
    ArmorPiercing,
}
public class PlayerStatus : MonoBehaviour
{
    public List<CharacterStatus> effects;

    private void Start()
    {
        //effects = new List<int>();
    }

    public void AddStatusToPlayer(CharacterStatus characterStatus)
    {
        effects.Add(characterStatus);
    }

   // public void RemoveStatus(StatusType statusType)
    //{
    //    for(int i = 0; i < effects.Count; i++)
    //    {
    //        Debug.Log(effects[i].nameStatus);
    //        if (effects[i].nameStatus == (statusType + ""))
    //        {
    //            effects.Remove(effects[i]);
    //        }
    //    }
    //}

    //private void Update()
    //{
    //    if (effects.Count > 0)
    //    {
    //        foreach (CharacterStatus status in effects)
    //        {
    //            status.ExecuteEffect(GetComponent<PlayerController>());
    //        }
    //    }
    //}
}
