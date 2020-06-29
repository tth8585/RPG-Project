
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public string enemyName;
    public Sprite enemyIcon;

    public int currentHP;
    public int maxHP;

    public int currentMP;
    public int maxMP;

    public int Armor;
    public int MovementSpeed;
    public float AttackSpeed;

    public int minDamage;
    public int maxDamage;

    public int expGive;
    public float rangeDetected;

    public float wanderTime;
    public float speedWander;

    private EnemyAttributes enemyAttributes;
    public EnemyType enemyType;
    public EnemyDataBase enemyDataBase;

    private void Awake()
    {
        enemyAttributes = enemyDataBase.GetEnemyReference(enemyType);
        GetStat();

        currentHP = maxHP;
        currentMP = maxMP;
        this.gameObject.tag = "Enemy";
    }
    private void Start()
    {
       


        //LoadRoles();
    }

    private void GetStat()
    {
        enemyName = enemyAttributes.nameEnemy;
        enemyIcon = enemyAttributes.icon;

        maxHP = (int)enemyAttributes.HP;
        maxMP = (int)enemyAttributes.MP;
        expGive = enemyAttributes.baseExp;
        Armor = (int)enemyAttributes.Armor;
        MovementSpeed = (int)enemyAttributes.MovementSpeed;
        AttackSpeed = (float)enemyAttributes.AttackSpeed;
        minDamage = enemyAttributes.minDamage;
        maxDamage = enemyAttributes.maxDamage;
        rangeDetected = enemyAttributes.rangeDetected;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        LoadRoles();
    }
    
    private void LoadRoles()
    {
        enemyDataBase = FindAssetByType<EnemyDataBase>("Assets/Resources/DataBase");
    }

    //load item database type auto https://answers.unity.com/questions/486545/getting-all-assets-of-the-specified-type.html?childToView=1216386#answer-1216386
    public static T FindAssetByType<T>(params string[] folders) where T : Object
    {
        string type = typeof(T).ToString().Replace("UnityEngine.", "");

        string[] guids;// = AssetDatabase.FindAssets("t:" + type);
        if (folders == null || folders.Length == 0)
        {
            guids = AssetDatabase.FindAssets("t:" + type);
        }
        else
        {
            guids = AssetDatabase.FindAssets("t:" + type, folders);
        }

        string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
        T assets = AssetDatabase.LoadAssetAtPath<T>(assetPath);

        return assets;
    }
#endif
}
