#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public string itemName;
    protected Animator animator;
    protected int damageAverage;
    public float damageFromPlayer;
    public WeaponsEquipment item;
    protected GameObject target;
    protected bool critDamage;
    
    public int playerStatus;

    public WeaponDataBase weaponDataBase;
    protected virtual void Start()
    {
        item = weaponDataBase.GetItemReference(itemName);
        animator = GetComponent<Animator>();
    }
    public virtual void GetPositionToUseSpell(Vector3 pos)
    {

    }
    public virtual void GetSelectedUnit(GameObject target)
    {
        this.target = target;
    }
    public virtual void PerformAttack(bool isCrit, float valueCritMulty)
    {
        //animator.SetTrigger("Base_Attack");
    }

    public virtual void SpecialAttack(bool isCrit, float valueCritMulty)
    {
        //animator.SetTrigger("Base_Attack");
    }

    public virtual void CastProjectile(bool isCrit, float valueCritMulty)
    {
        //Debug.Log("cast");
        //Fireball fireballInstance = Instantiate(fireball, ProjectileSpawn.position, ProjectileSpawn.rotation);
        //fireballInstance.direction = ProjectileSpawn.forward;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "Enemy")
        //{
        //    Debug.Log("hit enemy");
        //    other.GetComponent<IEnemy>().TakeDamage(damage);
        //}
        //else if (other.tag == "Player")
        //{
        //    // Debug.Log("hit player");
        //}
        //else
        //{
        //    Debug.Log("hit " + other.tag);
        //}
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        LoadRoles();
    }

    private void LoadRoles()
    {
        weaponDataBase = FindAssetByType<WeaponDataBase>("Assets/Resources/DataBase");
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
