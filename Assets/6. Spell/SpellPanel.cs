
using UnityEngine;

public class SpellPanel : MonoBehaviour
{
    [SerializeField] Transform itemParent;
    public SpellSlot[] spellSlots;
    private void Awake()
    {
        if (itemParent != null)
        {
            spellSlots = itemParent.GetComponentsInChildren<SpellSlot>(includeInactive: true);
        }

        SetIndexSlot();
        Clear();
    }

    public  void SetIndexSlot()
    {
        for (int i = 0; i < spellSlots.Length; i++)
        {
            spellSlots[i].indexSlot = i+1;
        }
    }

    public  void Clear()
    {
        for (int i = 0; i < spellSlots.Length; i++)
        {
            spellSlots[i].Spell = null;
        }
    }

    public void SetSpellSlot(Spell[] listSpell)
    {
        for(int i = 0; i < spellSlots.Length; i++)
        {
            spellSlots[i].Spell = listSpell[i];
            spellSlots[i].spellLevel = listSpell[i].spellLevel;
            if (listSpell[i].spellLevel == 0)
            {
                spellSlots[i].SpellLock();
            }
            else
            {
                spellSlots[i].SpellUnlock();
            }
        }
    }

    public void UpdateLevelSpellUI()
    {
        for (int i = 0; i < spellSlots.Length; i++)
        {
            if (spellSlots[i].spellLevel > 0)
            {
                spellSlots[i].SpellUnlock();
            }
            else
            {
                spellSlots[i].SpellLock();
            }
        }
    }
    public void AuraSlotActive(Spell spell, bool isActive)
    {
        for (int i = 0; i < spellSlots.Length; i++)
        {
            if(spellSlots[i].idSlot == spell.spellId)
            {
                spellSlots[i].ToggleAuraIcon(isActive);
            }
        }
    }

    private void FixedUpdate()
    {
        for(int i = 0; i < spellSlots.Length; i++)
        {
            if (spellSlots[i].Spell != null)
            {
                if (spellSlots[i].Spell.currentSpellCoolDown > 0)
                {
                    spellSlots[i].Spell.currentSpellCoolDown -= Time.fixedDeltaTime;

                    spellSlots[i].icon.fillAmount = 1 - (float)(spellSlots[i].Spell.currentSpellCoolDown / spellSlots[i].Spell.spellCoolDown);
                }
                else if(spellSlots[i].Spell.currentSpellCoolDown < 0)
                {
                    spellSlots[i].Spell.currentSpellCoolDown = 0;
                }
            }  
        }
    }
}
