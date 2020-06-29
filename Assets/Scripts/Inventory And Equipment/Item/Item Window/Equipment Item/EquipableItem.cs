using System;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType 
{
    Weapon,
    Ability,
    Armor,
    Accessory,
}

public class EquipableItem : Item
{
    [Space]
    public EquipmentType equipmentType;
    [Space]

    public int HP;
    public int MP;
    [Space]

    public int STR;
    public int INT;
    public int AGI;
    [Space]
    public float hpRegen;
    public float mpRegen;

    public override Item GetCopy()
    {
        return Instantiate(this);
    }

    public override void Destroy()
    {
        Destroy(this);
    }
    public virtual void Equip(PlayerController c)
    {
        //return;//
        //if(StrengthBonus != 0)
        //{
        //    c.Strength.AddModifier(new StatModifier(StrengthBonus, StatModType.Flat, this));
        //}
        //if (AgilityBonus != 0)
        //{
        //    c.Agility.AddModifier(new StatModifier(AgilityBonus, StatModType.Flat, this));
        //}
        //if (IntelligenceBonus != 0)
        //{
        //    c.Intelligence.AddModifier(new StatModifier(IntelligenceBonus, StatModType.Flat, this));
        //}
        ////-------------------------------------------------------------------------------------------------
        //if (StrengthPercentIncre != 0)
        //{
        //    c.Strength.AddModifier(new StatModifier(StrengthPercentIncre, StatModType.PercentAdd, this));
        //}
        //if (AgilityPercentIncre != 0)
        //{
        //    c.Agility.AddModifier(new StatModifier(AgilityPercentIncre, StatModType.PercentAdd, this));
        //}
        //if (IntelligencePercentIncre != 0)
        //{
        //    c.Intelligence.AddModifier(new StatModifier(IntelligencePercentIncre, StatModType.PercentAdd, this));
        //}

        //===============================================================================================================
        //if (StrengthPercentMore != 0)
        //{
        //    c.Strength.AddModifier(new StatModifier(StrengthPercentMore, StatModType.PercentMult, this));
        //}
        //if (AgilityPercentMore != 0)
        //{
        //    c.Agility.AddModifier(new StatModifier(AgilityPercentMore, StatModType.PercentMult, this));
        //}
        //if (IntelligencePercentMore != 0)
        //{
        //    c.Intelligence.AddModifier(new StatModifier(IntelligencePercentMore, StatModType.PercentMult, this));
        //}

        //=====================================================================
        //flat
        if (HP != 0)
        {
            c.playerStats.HP.AddModifier(new StatModifier(HP, StatModType.Flat, this));
        }
        if (MP != 0)
        {
            c.playerStats.MP.AddModifier(new StatModifier(MP, StatModType.Flat, this));
        }
        if (STR != 0)
        {
            c.playerStats.STR.AddModifier(new StatModifier(STR, StatModType.Flat, this));
        }
        if (INT != 0)
        {
            c.playerStats.INT.AddModifier(new StatModifier(INT, StatModType.Flat, this));
        }
        if (AGI != 0)
        {
            c.playerStats.AGI.AddModifier(new StatModifier(AGI, StatModType.Flat, this));
        }
        if (mpRegen != 0)
        {
            c.playerStats.MpRegen.AddModifier(new StatModifier(mpRegen, StatModType.Flat, this));
        }
        if (hpRegen != 0)
        {
            c.playerStats.HpRegen.AddModifier(new StatModifier(hpRegen, StatModType.Flat, this));
        }

        c.playerStats.GetStat();
    }

    public virtual void Unequip(PlayerController c)
    {
        c.playerStats.HP.RemoveAllModifierFromSource(this);
        c.playerStats.MP.RemoveAllModifierFromSource(this);

        c.playerStats.STR.RemoveAllModifierFromSource(this);
        c.playerStats.INT.RemoveAllModifierFromSource(this);
        c.playerStats.AGI.RemoveAllModifierFromSource(this);

        c.playerStats.HpRegen.RemoveAllModifierFromSource(this);
        c.playerStats.MpRegen.RemoveAllModifierFromSource(this);

        c.playerStats.GetStat();
    }

    public override string GetItemType()
    {
        return equipmentType.ToString();
    }

    public override string GetItemDescription()
    {
        stringBuilder.Length = 0;

        AddStats(HP, "HP", StatModType.Flat);
        AddStats(MP, "MP", StatModType.Flat);

        AddStats(STR, "STR", StatModType.Flat);
        AddStats(INT, "INT", StatModType.Flat);
        AddStats(AGI, "AGI", StatModType.Flat);

        AddStats(hpRegen, "HP Regen", StatModType.Flat);
        AddStats(mpRegen, "MP Regen", StatModType.Flat);
        //AddStats(item.StrengthPercentIncre, "STR", StatModType.PercentAdd);
        //AddStats(item.AgilityPercentIncre, "AGI", StatModType.PercentAdd);
        //AddStats(item.IntelligencePercentIncre, "INT", StatModType.PercentAdd);

        //AddStats(item.StrengthPercentMore, "STR", StatModType.PercentMult);
        //AddStats(item.AgilityPercentMore, "AGI", StatModType.PercentMult);
        //AddStats(item.IntelligencePercentMore, "INT", StatModType.PercentMult);


        return stringBuilder.ToString();
    }

    private void AddStats(float value, string statName, StatModType showType)
    {
        value = (float)Math.Round(value, 2);
        if (value != 0)
        {
            if (stringBuilder.Length > 0)
            {
                stringBuilder.AppendLine();
            }

            if (value > 0)
            {
                if (showType == StatModType.Flat)
                {
                    stringBuilder.Append("+ ");
                }
                else if (showType == StatModType.PercentAdd)
                {
                    stringBuilder.Append("Inc ");
                }
                else if (showType == StatModType.PercentMult)
                {
                    stringBuilder.Append("More ");
                }

            }
            else if (value < 0)
            {
                value = value * -1;
                if (showType == StatModType.Flat)
                {
                    stringBuilder.Append("- ");
                }
                else if (showType == StatModType.PercentAdd)
                {
                    stringBuilder.Append("Dec ");
                }
                else if (showType == StatModType.PercentMult)
                {
                    stringBuilder.Append("Less ");
                }
            }

            if (showType != StatModType.Flat)
            {
                value = value * 100;
            }

            stringBuilder.Append(value);

            if (showType != StatModType.Flat)
            {
                stringBuilder.Append("%");
            }

            stringBuilder.Append(" ");

            stringBuilder.Append(statName);
        }
    }
}
