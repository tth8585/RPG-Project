using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStatus))]
public class PlayerWeaponController : MonoBehaviour
{
    public GameObject playerHand;
    public GameObject playerBack;
    public GameObject equippedWeapon { get; set; }

    private string weaponPath = "Prefabs/Items/Equipment/Weapon/";
    private string projectSpawnString = "ProjectileSpawn";

    Transform spawnProjectile;
    WeaponController weaponUsing;

    private void Awake()
    {
        spawnProjectile = transform.Find(projectSpawnString);
    }
    private void Start()
    {
        EquipmentSlot item = EquipmentPanel.Instance.equipmentSlots[0];
 
        if(item.item == null)
        {
            playerBack.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            playerBack.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    PerformWeaponAttack();
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    SpecialAttack();
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    CastBar.Instance.CastSpell(3);
        //}
    }
    public void EquipWeapon(EquipableItem item)
    {
        if(equippedWeapon != null)
        {
            Destroy(playerHand.transform.GetChild(0).gameObject);
        }

        //anim.Play(equipAnim, -1, 0f);

        equippedWeapon = (GameObject)Instantiate(Resources.Load<GameObject>(weaponPath + item.itemName), playerHand.transform.position, playerHand.transform.rotation);
        equippedWeapon.transform.SetParent(playerHand.transform);
        equippedWeapon.SetActive(false);
        weaponUsing = equippedWeapon.GetComponent<WeaponController>();

        WeaponsEquipment itemWeapon = item as WeaponsEquipment;

        if(itemWeapon.weaponType == WeaponType.Staff)
        {
            equippedWeapon.GetComponent<IProjectileWeapon>().ProjectileSpawn = spawnProjectile;
        }

        PlayerAnimationController.Instance.AnimateEquip();
        StartCoroutine(AnimEquip(0.7f, equippedWeapon, playerBack));
    }


    public void UnEquipWeapon()
    {
        if (equippedWeapon != null)
        {
            PlayerAnimationController.Instance.AnimateUnequip();
            StartCoroutine(AnimUnEquip(0.66f, playerHand.transform.GetChild(0).gameObject, playerBack));
            //Destroy(equippedWeapon);
        }
    }

    private IEnumerator AnimUnEquip(float duration,GameObject target, GameObject playerBack )
    {
        yield return new WaitForSeconds(duration);
        playerBack.transform.GetChild(0).gameObject.SetActive(true);
        Destroy(target);
    }

    private IEnumerator AnimEquip(float duration, GameObject target, GameObject playerBack)
    {
        yield return new WaitForSeconds(duration);
        if (target != null)
        {
            target.SetActive(true);
            playerBack.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void PerformWeaponAttack()
    {
        if (equippedWeapon == null)
        {
            Debug.Log("no weapon");
            return;
        }
        weaponUsing.damageFromPlayer = CalPhysicDamage();

        PlayerStats playerStats = GetComponent<PlayerStats>();

        if (Random.Range(0,100+1)<= playerStats.CritChance.value){
            weaponUsing.PerformAttack(true, playerStats.CritMulty.value);
        }
        else
        {
            weaponUsing.PerformAttack(false, playerStats.CritMulty.value);
        }
        //Debug.Log("chua fix dame player");
    }

    public void SpecialAttack()
    {
        if (equippedWeapon == null)
        {
            Debug.Log("no weapon");
            return;
        }
        weaponUsing.damageFromPlayer = CalMagicDamage();
      
        weaponUsing.GetSelectedUnit(GetComponent<UserSelectMonster>().selectedUnit);

        PlayerStats playerStats = GetComponent<PlayerStats>();
        if (Random.Range(0, 100 + 1) <= GetComponent<PlayerStats>().CritChance.value)
        {
            weaponUsing.SpecialAttack(true, playerStats.CritMulty.value);
        }
        else
        {
            weaponUsing.SpecialAttack(false, playerStats.CritMulty.value);
        }
    }
    float CalMagicDamage()
    {
        Debug.Log("chua xu ly damage magic bonus tu player");
        return 0;
    }
    private float CalPhysicDamage()
    {
        return GetComponent<PlayerStats>().damage;
    }
    public void CalculateDPS()
    {
        //dps with enemy has 0 DEF
        float dps;
        int numberOfShoot = 1;
        int enemyDef = 0;

        float ATT = 0;//= GetComponent<PlayerStats>().ATT.value;
        float DEX = 0;// GetComponent<PlayerStats>().DEX.value;

        dps = (float)((((weaponUsing.item.minDamage + weaponUsing.item.maxDamage) * 0.5 * (0.5 + ATT / 50) - enemyDef) * numberOfShoot) * (1.5 + 6.5 * (DEX / 75)));

        Debug.Log(" chua fix dps: "+ dps);
    }
}
