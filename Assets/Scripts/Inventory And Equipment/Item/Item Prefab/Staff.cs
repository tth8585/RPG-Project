using UnityEngine;

public class Staff : WeaponController, IProjectileWeapon
{
    //string projectilePath = "Prefabs/Items/Equipment/Weapon/Projectiles/Fireball";
    string projectilePath = "Prefabs/Spell/RangeSpell";
    public Transform ProjectileSpawn { get ; set ; }

    //Fireball fireball;
    RangeSpell rangeSpellPrefab;

    protected override void Start()
    {
        base.Start();
        //fireball = Resources.Load<Fireball>(projectilePath);
        rangeSpellPrefab = Resources.Load<RangeSpell>(projectilePath);
        //Debug.Log(rangeSpellPrefab);
    }

    public override void CastProjectile(bool isCrit, float valueCritMulty)
    {
        //Fireball fireballInstance = Instantiate(fireball, ProjectileSpawn.position, ProjectileSpawn.rotation);
        //fireballInstance.direction = ProjectileSpawn.forward;
      
        if(target != null)
        {
            RangeSpell clone = Instantiate(rangeSpellPrefab, ProjectileSpawn.position, ProjectileSpawn.rotation);
            clone.target = target;
            clone.isCrit = isCrit;
            //clone.SetDamage(damageFromPlayer);
            //if (isCrit)
            //{
            //    clone.SetDamage(damageFromPlayer * valueCritMulty);
            //}
        }
        else
        {
            Debug.Log("target null");
        }       
    }
    public override void SpecialAttack(bool isCrit, float valueCritMulty)
    {
        CastProjectile(isCrit, valueCritMulty);
    }
    public override void PerformAttack(bool isCrit, float valueCritMulty)
    {
        animator.SetTrigger("Base_Attack");

        float critMulty = valueCritMulty;

        damageAverage = (int)(Random.Range(item.minDamage, item.maxDamage + 1) + damageFromPlayer);
        if (isCrit)
        {
            damageAverage += (int)(damageAverage * critMulty);
        }  
        //Debug.Log(damageAverage);
    }
}
