using UnityEngine;

public enum EnemyRank
{
    Normal,
    Elite,
    Boss,
}
public enum EnemyType
{
    Slime,
    Orc,
}
[CreateAssetMenu(menuName = "Character/Enemy")]
public class EnemyAttributes : ScriptableObject
{
    public string nameEnemy;
    public Sprite icon;
    public EnemyType enemyType;

    [Header("Stats")]
    public int baseExp;
    [Space]
    public float HP;
    public float MP;
    public float Armor;
    public float MovementSpeed;
    public float AttackSpeed;
    [Space]
    public int minDamage;
    public int maxDamage;
    [Space]
    public float rangeDetected;

    //public int initialATT;
    //public int initialDEF;
    //public int initialSPD;
    //public int initialDEX;
    //public int initialVIT;
    //public int initialWIS;
}
