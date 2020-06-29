using System;
using UnityEngine;
[Serializable]
public class Buff
{
    [SerializeField] Icon icon;
    [SerializeField] string buffName;
    [SerializeField] int duration;
    [SerializeField] int currentDuration;
    [SerializeField] bool permanent = false;

   
    //effect
    //its buff or debuff
}
