#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class SpellStat : MonoBehaviour
{
    private Spell spell;
    public SpellDataBase spellDataBase;

    public string id;
    [Space]
    public string spellName;
    public Sprite spellIcon;
    public string spellDescription;

    public float spellDamage;
    public float spellCastTime;
    public float spellCoolDown;

    private void Awake()
    {
        spell = spellDataBase.GetSpellReference(id);
        GetStat();
    }

    void GetStat()
    {
        spellName = spell.spellName;
        spellIcon = spell.spellIcon;
        spellDescription = spell.spellDescription;
        spellDamage = spell.spellDamage;
        spellCastTime = spell.spellCastTime;
        spellCoolDown = spell.spellCoolDown;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        LoadRoles();
    }

    private void LoadRoles()
    {
        spellDataBase = FindAssetByType<SpellDataBase>("Assets/Resources/DataBase");
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
