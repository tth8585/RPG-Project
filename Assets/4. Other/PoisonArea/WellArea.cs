using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellArea : MonoBehaviour
{
    float tickTime;
    PlayerStats c;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            c = other.GetComponent<PlayerStats>();
            if (c.HP.value > c.currentHP||
                c.MP.value > c.currentMP)
            {
                tickTime -= Time.deltaTime;
                if (tickTime < 0)
                {
                    tickTime = 1;
    
                    c.currentMP += c.MP.value * 0.3f;
                    c.currentHP += c.HP.value * 0.3f;

                    UIEvent.HealthChanged(c.currentHP, c.HP.value);
                    UIEvent.ManaChanged(c.currentMP, c.MP.value);
                }
            }
        }
    }
}
