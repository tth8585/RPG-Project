
using UnityEngine;

public enum RoleType
{
    BASE_STR, 
    BASE_INT,//CM
    BASE_AGI,

    BASE_STR_AGI,
    BASE_STR_INT,
    BASE_INT_AGI,

    BASE_CENTER,
}
[CreateAssetMenu(menuName = "Character/Role Player")]
public class RoleStat : ScriptableObject
{
    public string nameRole;

    [Header("Initial Stats")]
    public float initialSTR;
    public float initialAGI;
    public float initialINT;

    public float initialHP;
    public float initialHpRegen;
    public float initialMP;
    public float initialMpRegen;
    public float initialArmor;
    public float initialDamage;

    [Header("Gain Per Level")]
    public float gainPerLevelSTR;
    public float gainPerLevelAGI;
    public float gainPerLevelINT;

    [Header("Not change")]
    public float magicResistance;
    public float movementSpeed;

    public float attackSpeed;

    public float damageBlock;
    public float critChance;
    public float critMulty;
}
