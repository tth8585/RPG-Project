using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

[Serializable]
public class BaseStat
{
    public float baseValue; // value 
    public virtual float value 
    { 
        get 
        {
            if (isDirty || baseValue != lastBaseValue)
            {
                lastBaseValue = baseValue;
                _value = CalculateFinalValue();
                isDirty = false;
            }
            return _value;
        } 
    }

    protected bool isDirty = true;
    protected float _value;
    protected float lastBaseValue = float.MinValue;

    public readonly List<StatModifier> statModifiers;
    public readonly ReadOnlyCollection<StatModifier> readyOnlyStatModifiers;

    public BaseStat()
    {
        statModifiers = new List<StatModifier>();
        readyOnlyStatModifiers = statModifiers.AsReadOnly();
    }
    public BaseStat(float baseValue) : this()
    {
        this.baseValue = baseValue;
    }

    public virtual void AddModifier(StatModifier mod)
    {
        isDirty = true;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder);
    }

    protected virtual int CompareModifierOrder(StatModifier a,StatModifier b)
    {
        if (a.order < b.order)
        {
            return -1;
        }else if (a.order > b.order)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public virtual bool RemoveModifier(StatModifier mod)
    {
        if (statModifiers.Remove(mod))
        {
            isDirty = true;
            return true;
        }

        return false;
    }

    public virtual bool RemoveAllModifierFromSource(object source)
    {
        bool didRemove = false;
        for(int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if(statModifiers[i].source == source)
            {
                isDirty = true;
                didRemove = true;

                statModifiers.RemoveAt(i);
            }
        }

        return didRemove;
    }

    protected virtual float CalculateFinalValue()
    {
        float finalValue = baseValue;

        float flatAdd = 0;
        float increAdd = 0;
        float moreAdd = 0;

        List<float> listFlatAdd = new List<float>();
        List<float> listIncreAdd = new List<float>();
        List<float> listMoreAdd = new List<float>();

        for(int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];

            if(mod.statModType == StatModType.Flat)
            {
                flatAdd = mod.value;
                listFlatAdd.Add(flatAdd);
                flatAdd = 0;
            }
            else if (mod.statModType == StatModType.PercentAdd)
            {
                increAdd = mod.value;
                listIncreAdd.Add((float)increAdd);
                increAdd = 0;
            }
            else if(mod.statModType == StatModType.PercentMult)
            {
                moreAdd = 1 + mod.value;
                listMoreAdd.Add((float)moreAdd);
                moreAdd = 0;
            }
           
        }

        for (int i = 0; i < listFlatAdd.Count; i++)
        {
            finalValue += listFlatAdd[i];
        }

        float total = 0;

        for (int i = 0; i < listIncreAdd.Count; i++)
        {
            total += listIncreAdd[i];
        }
        finalValue *= 1+ total;

        for (int i = 0; i < listMoreAdd.Count; i++)
        {
            finalValue *= listMoreAdd[i];
        }

        if(finalValue < 0)
        {
            finalValue = 0;
        }

        return (float)Math.Round(finalValue, 2);
    }
}
