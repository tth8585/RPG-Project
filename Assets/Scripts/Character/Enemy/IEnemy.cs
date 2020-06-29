
using UnityEngine;

public interface IEnemy
{
    string enemyName { get; }
    Sprite icon{ get;}
    int ID { get; set; }
    int Experience { get;}
    Spawner Spawner { get; set; }
    void TakeDamage(float amount, bool isCrit);
    void PerformAttack();
    //void GetStat(EnemyAttributes enemyAttributes);
    bool isDead { get; set; }
    bool inCombat { get; set; }
    void Die();
    //void GiveExp();
}
