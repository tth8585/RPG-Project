using UnityEngine;

public interface IProjectileWeapon 
{
    Transform ProjectileSpawn { get; set; }
    void CastProjectile(bool isCrit, float valueCritMulty);
}
