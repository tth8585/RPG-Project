
using UnityEngine;

public class Sword : WeaponController
{
    public override void PerformAttack(bool isCrit, float valueCritMulty)
    {
        animator.SetTrigger("Base_Attack");

        float critMulty = valueCritMulty;
        damageAverage = (int)(Random.Range(item.minDamage, item.maxDamage + 1) + damageFromPlayer);
        if (isCrit)
        {
            damageAverage += (int)(damageAverage * critMulty);
        }
        critDamage = isCrit;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("hit enemy");
            other.GetComponent<IEnemy>().TakeDamage(damageAverage,critDamage);
        }
        else if (other.tag == "Player")
        {
            // Debug.Log("hit player");
        }
        else
        {
            //Debug.Log("hit " + other.tag);
        }
    }

}
