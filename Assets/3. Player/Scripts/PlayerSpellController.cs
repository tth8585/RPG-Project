
using System.Collections;
using UnityEngine;

public class PlayerSpellController : MonoBehaviour
{
    [SerializeField] private Spell[] listSpells;
    [SerializeField] public Spell[] listCurrentSpells;
    [SerializeField] private SpellPanel spellPanel;
    [SerializeField] GameObject auraGameObject;
    Vector3 cursorPos;
    [SerializeField] private GameObject circleMagicObject;
    bool makeCircleSpell;
    [SerializeField] private LayerMask targetLayerMask;
    [SerializeField] private LayerMask circleLayerMask;
    [SerializeField] private GameObject[] listTarget;
    [SerializeField] private GameObject spawnProjectObject;

    Spell _spell;
    GameObject targetUnit;
    PlayerStats playerStats;
    PlayerController character;
    [SerializeField] private Transform auraSpellPlace;

    private void Start()
    {
        makeCircleSpell = false;
 
        playerStats = GetComponent<PlayerStats>();
        character = GetComponent<PlayerController>();

        listCurrentSpells = new Spell[4];

        for(int i=0;i< listCurrentSpells.Length; i++)
        {
            if (i < listSpells.Length)
            {
                listCurrentSpells[i] = listSpells[i];
                
                if (i == 0)
                {
                    listCurrentSpells[i].spellLevel = 1;
                }
                else
                {
                    listCurrentSpells[i].spellLevel = 0;
                }
            }
        }

        spellPanel.SetSpellSlot(listCurrentSpells);

        ResetSpellAura();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Spell1"))
        {
            CancelAreaSpell();
            if (listCurrentSpells[0] != null&& listCurrentSpells[0].spellLevel>0)
            {
                _spell = listCurrentSpells[0];
                CheckCanCast();
            }
            else
            {
                Debug.Log("no spell at slot 1");
            }
        }
        if (Input.GetButtonDown("Spell2"))
        {
            CancelAreaSpell();
            if (listCurrentSpells[1] != null && listCurrentSpells[1].spellLevel > 0)
            {
                _spell = listCurrentSpells[1];
                CheckCanCast();
            }
            else
            {
                Debug.Log("no spell at slot 2");
            }
        }
        if (Input.GetButtonDown("Spell3"))
        {
            CancelAreaSpell();
            if (listCurrentSpells[2] != null && listCurrentSpells[2].spellLevel > 0)
            {
                _spell = listCurrentSpells[2];
                CheckCanCast();
            }
            else
            {
                Debug.Log("no spell at slot 3");
            }
        }
        if (Input.GetButtonDown("Spell4"))
        {
            CancelAreaSpell();
            if (listCurrentSpells[3] != null && listCurrentSpells[3].spellLevel > 0)
            {
                _spell = listCurrentSpells[3];
                CheckCanCast();
            }
            else
            {
                Debug.Log("no spell at slot 4");
            }
        }

        //Debug.Log(isCastCircle);

        if (makeCircleSpell == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _spell.spellRange, circleLayerMask))
            {
                if (hit.transform.tag == "Untagged")
                {
                    cursorPos = hit.point;
                    circleMagicObject.transform.position = cursorPos;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                //cast
                CheckCanCast();
                SearchTarget();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                CancelAreaSpell();
            }
        }
    }
    private void SearchTarget()
    {
        
        listTarget = new GameObject[_spell.spellNumberTarget];

        Collider[] withinAggroCollifer = Physics.OverlapSphere(cursorPos, _spell.spellAoe, targetLayerMask);
        int i = 0;
        while (i < withinAggroCollifer.Length)
        {
            if (withinAggroCollifer[i].transform.tag == "Enemy")
            {
                if (i < listTarget.Length)
                {
                    listTarget[i] = withinAggroCollifer[i].gameObject;
                }
            }
            i++;
        }
        StartCoroutine(DelayDamage());
    }
    public void DealDamageAfterFX()
    {
        for (int i = 0; i < listTarget.Length; i++)
        {
            if (listTarget[i] != null)
            {
                listTarget[i].GetComponent<EnemyController>().TakeDamage(_spell.spellDamage+ _spell.spellLevel/100*10*_spell.spellDamage, false);
            }
        }
        //listTarget = new GameObject[0];

        Debug.Log("chua xu ly magic crit dame");
    }

    private IEnumerator DelayDamage()
    {
        float timedelay = 2f;
        yield return new WaitForSeconds(timedelay);
        DealDamageAfterFX();
    }
    private void OnDrawGizmos()
    {
        if(cursorPos!=null&&_spell!=null&& makeCircleSpell == true)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(cursorPos, _spell.spellAoe);
        }
    }
    private void CancelAreaSpell()
    {
        makeCircleSpell = false;
        circleMagicObject.SetActive(false);
    }
    private void PlayAnimType(SpellAbility spellAbility)
    {
        switch (spellAbility)
        {
            case SpellAbility.TargetUnit:
                PlayerAnimationController.Instance.AnimateSpellTarget();
                SoundManager.PlaySound(SoundManager.Sound.Spell1);
                break;
            case SpellAbility.TargetPoint:
                break;
            case SpellAbility.TargetArea:
                PlayerAnimationController.Instance.AnimateSpellArea();
                SoundManager.PlaySound(SoundManager.Sound.Spell3);
                break;
            case SpellAbility.NoTarget:
                PlayerAnimationController.Instance.AnimateSpellBuff();
                SoundManager.PlaySound(SoundManager.Sound.Spell2);
                break;
            case SpellAbility.Toggle:
                break;
            case SpellAbility.Passive:
                PlayerAnimationController.Instance.AnimateSpellAura();
                SoundManager.PlaySound(SoundManager.Sound.Spell4);
                break;
            default:
                break;
        }
    }

    public void CreateSpellFX()
    {
        switch (_spell.spellAbility)
        {
            case SpellAbility.TargetUnit:
                GameObject go = Instantiate(_spell.spellFX, spawnProjectObject.transform.position, Quaternion.identity);
                go.GetComponent<RangeSpell>().SetSpell(_spell.spellDamage+ _spell.spellLevel/100*10* _spell.spellDamage, targetUnit);
                break;
            case SpellAbility.TargetPoint:
                break;
            case SpellAbility.TargetArea:
                Instantiate(_spell.spellFX, cursorPos, Quaternion.identity);
                break;
            case SpellAbility.NoTarget:
                Instantiate(_spell.spellFX, auraSpellPlace.position, Quaternion.identity);
                break;
            case SpellAbility.Toggle:
                break;
            case SpellAbility.Passive:
                Instantiate(_spell.spellFX, auraSpellPlace.position, Quaternion.identity);
                break;
            default:
                break;
        }
        _spell = null;
    }

    private void CheckCanCast()
    {
        if (_spell.spellAbility == SpellAbility.TargetUnit)
        {
            targetUnit = GetComponent<UserSelectMonster>().selectedUnit;

            if (targetUnit != null)
            {
                float distance = Vector3.Distance(character.transform.position, targetUnit.transform.position);
                if(distance<= _spell.spellRange)
                {
                    CheckManaAndCoolDown();
                }
                else
                {
                    Debug.Log("out of range");
                }
            }
            else
            {
                Debug.Log("target null");
            }
        }
        else if (_spell.spellAbility == SpellAbility.Passive)
        {
            CheckManaReverse();
        }
        else if (_spell.spellAbility == SpellAbility.TargetArea)
        {
            if(makeCircleSpell == true)
            {
                CheckManaAndCoolDown();
                makeCircleSpell = false;
                //circleMagicObject.SetActive(true);
            }
            else
            {
                makeCircleSpell = true;
                circleMagicObject.SetActive(true);
            }
        }
        else
        {
            CheckManaAndCoolDown();
        }
    }

    private void CheckManaAndCoolDown()
    {
        if (_spell.manaCost <= playerStats.currentMP && _spell.currentSpellCoolDown == 0)
        {
            PlayAnimType(_spell.spellAbility);

            _spell.currentSpellCoolDown = _spell.spellCoolDown;
            playerStats.currentMP -= _spell.manaCost;

            if(_spell.spellAbility == SpellAbility.NoTarget)
            {
                BuffSpell spell = _spell as BuffSpell;
                spell.ActiveSpell(character);
            }
        }
        else
        {
            if (_spell.currentSpellCoolDown != 0)
            {
                Debug.Log("spell is not ready");
            }
            else if (_spell.manaCost > playerStats.currentMP)
            {
                Debug.Log("not enough mana");
            }
        }
        CancelAreaSpell();
    }

    private void CheckManaReverse()
    {
        AuraSpell spellUsing = _spell as AuraSpell;

        if (spellUsing.isActive == false)
        {
            if(playerStats.MP.value>= spellUsing.manaReserved)
            {
                spellUsing.ActiveSpell(character);
                character.UpdateStatValues();
                spellUsing.isActive = true;

                PlayAnimType(spellUsing.spellAbility);
                spellPanel.AuraSlotActive(spellUsing, true);
                auraGameObject.SetActive(true);
            }
            else
            {
                Debug.Log("not enough mana reverse");
            }
        }
        else if(spellUsing.isActive == true)
        {
            spellUsing.isActive = false;
            spellUsing.InactiveSpell(character);
            character.UpdateStatValues();
            spellPanel.AuraSlotActive(spellUsing, false);
            auraGameObject.SetActive(false);
        }
    }

    void ResetSpellAura()
    {
        for(int i=0;i< listSpells.Length; i++)
        {
            if(listSpells[i].spellAbility == SpellAbility.Passive)
            {
                AuraSpell auraSpell = listSpells[i] as AuraSpell;
                auraSpell.isActive = false;
            }
        }
    }
}
