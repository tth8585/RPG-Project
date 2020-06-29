
using UnityEngine;

public enum WeaponType
{
    Sword,
    Bow,
    Katana,
    Dagger,
    Wand,
    Staff,
}

[CreateAssetMenu(menuName = "Items/Equipment Item/Weapons Item")]
public class WeaponsEquipment : EquipableItem
{
    [Space]
    public WeaponType weaponType;
    [Space]
    public int minDamage;
    public int maxDamage;

    [Space]
    //crit chance
    [Header("Weapon critical strike potential")]
    public float minBaseCritChane;
    public float maxBaseCritChane;

    //[Header("Weapon Attributes")]
    ////public float ProjectileSpeed;
    ////public float ProjectileLifetime;
    ////public float WeaponRange;
    ////public float ProjectileAmplitude;
    ////public int ProjectileAmount;
    ////public float RateofFire;
    ////public float WeaponDamage;
    ////public bool EnemyPiercingProjectiles;
    ////public bool ArmorPiercingProjectiles;
    ////public bool ProjectilesThatIgnoreObstacles;
    ///
}
