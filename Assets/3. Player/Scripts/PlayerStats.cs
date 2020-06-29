using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LevelUpSystem))]
[RequireComponent(typeof(PlayerController))]
public class PlayerStats : MonoBehaviour
{
    public float currentHP;
    public float currentMP;

    public float damage;
    
    public BaseStat STR;
    public BaseStat AGI;
    public BaseStat INT;

    public BaseStat HP;
    public BaseStat HpRegen;
    public BaseStat MP;
    public BaseStat MpRegen;

    //public float MpBeforeRe;

    public BaseStat Armor;
    public BaseStat AttackSpeed;

    public BaseStat MagicRes;
    public BaseStat MoveSpeed;
    public BaseStat DamageBlock;
    public BaseStat CritChance;
    public BaseStat CritMulty;

    [Space]
    private RoleStat PlayerRoleAtt;
    public RoleType playerRoleType;
    public RoleDataBase roleDataBase;
   
    private void Awake()
    {
        PlayerRoleAtt = roleDataBase.GetRoleReference(playerRoleType.ToString());
    }
    private void Start()
    {
        GetStat();

        StartCoroutine(DelayUpdateUI());
    }

    private IEnumerator DelayUpdateUI()
    {
        yield return new WaitForSeconds(0.5f);
        UpdateCurrent();
    }

    public void GetStat()
    {
        int levelPlayer = GetComponent<LevelUpSystem>().currentLevel;

        STR.baseValue = PlayerRoleAtt.initialSTR + (levelPlayer-1) * PlayerRoleAtt.gainPerLevelSTR;
        HP.baseValue = PlayerRoleAtt.initialHP + CalHP();
        HpRegen.baseValue = PlayerRoleAtt.initialHpRegen + CalculateHPRegen();

        INT.baseValue = PlayerRoleAtt.initialINT + (levelPlayer-1) * PlayerRoleAtt.gainPerLevelINT;
        MP.baseValue = PlayerRoleAtt.initialMP + CalMP();
        MpRegen.baseValue = PlayerRoleAtt.initialMpRegen + CalculateMPRegen();

        AGI.baseValue = PlayerRoleAtt.initialAGI + (levelPlayer-1) * PlayerRoleAtt.gainPerLevelAGI;
        Armor.baseValue = PlayerRoleAtt.initialArmor + CalArmor();
        AttackSpeed.baseValue = CalAttackPerSec();

        MagicRes.baseValue = PlayerRoleAtt.magicResistance;
        MoveSpeed.baseValue = PlayerRoleAtt.movementSpeed;
        DamageBlock.baseValue = PlayerRoleAtt.damageBlock;
        CritChance.baseValue = PlayerRoleAtt.critChance;
        CritMulty.baseValue = PlayerRoleAtt.critMulty;

        damage = CalDamagePhysic();

        if (currentHP > HP.value)
        {
            currentHP = HP.value;
        }

        if (currentMP > MP.value)
        {
            currentMP = MP.value;
        }
    }
    float CalHP()
    {
        return (float)STR.value * 20;
    }
    float CalMP()
    {
        return (float)INT.value * 12;
    }
    float CalAttackPerSec()
    {
        float BAT = 1.7f;
        return (float)(((PlayerRoleAtt.attackSpeed+ AGI.value)*0.01f)/BAT);
    }
    float CalArmor()
    {
        return (float)(AGI.value*0.16f);
    }

    public float CalDamagePhysic()
    {
        //without buff or status
        switch (playerRoleType)
        {
            case RoleType.BASE_STR:
                return (float)(STR.value);
            case RoleType.BASE_INT:
                return (float)(INT.value);
            case RoleType.BASE_AGI:
                return (float)(AGI.value);
            case RoleType.BASE_STR_AGI:
                return (float)(STR.value*0.5+ AGI.value*0.5);
            case RoleType.BASE_STR_INT:
                return (float)(INT.value * 0.5 + STR.value * 0.5);
            case RoleType.BASE_INT_AGI:
                return (float)(INT.value * 0.5 + AGI.value * 0.5);
            default:
                Debug.Log("wrong role class");
                return 0;
        }
    }
    public void UpdateCurrent()
    {
        currentHP = HP.value;
        currentMP = MP.value;
        
        UIEvent.HealthChanged(currentHP,HP.value);
        UIEvent.ManaChanged(currentMP, MP.value);
    }
    public void LevelUp()
    {
        GetStat();
    }
    private float CalculateHPRegen()
    {
        return (float)(0.1 * STR.value);
    }
    private float CalculateMPRegen( )
    {
        return (float)(0.05 * INT.value);
    }

    void RegenHP()
    {
        if (currentHP < HP.value)
        {
            currentHP += HpRegen.value;
            UIEvent.HealthChanged(currentHP, HP.value);
        }
        else if(currentHP > HP.value)
        {
            currentHP = HP.value;
            UIEvent.HealthChanged(currentHP, HP.value);
        }
    }
    void RegenMP()
    {
        if (currentMP < MP.value)
        {
            currentMP += MpRegen.value;
            UIEvent.ManaChanged(currentMP,MP.value);
        }
        else if (currentMP > MP.value)
        {
            currentMP = MP.value;
            UIEvent.ManaChanged(currentMP,MP.value);
        }
    }
    float elapsed = 0f;
    private void FixedUpdate()
    {
        elapsed += Time.fixedDeltaTime;
        if (elapsed >= 1f)
        {
            elapsed = elapsed % 1f;
            RegenHP();
            RegenMP();
        }
    }
}
