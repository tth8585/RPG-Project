
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
public class SpellDataBase : ScriptableObject
{
    [SerializeField] Spell[] Spells;
    public Spell GetSpellReference(string spellId)
    {
        foreach (Spell spell in Spells)
        {
            if (spell.spellId == spellId)
            {
                return spell;
            }
        }

        return null;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        LoadSpell();
    }
    private void OnEnable()
    {
        EditorApplication.projectChanged += LoadSpell;
    }
    private void OnDisable()
    {
        EditorApplication.projectChanged -= LoadSpell;
    }
    private void LoadSpell()
    {
        Spells = FindAssetsByType<Spell>("Assets/6. Spell/ScriptableObject");
    }

    //load item database type auto https://answers.unity.com/questions/486545/getting-all-assets-of-the-specified-type.html?childToView=1216386#answer-1216386
    public static T[] FindAssetsByType<T>(params string[] folders) where T : Object
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

        T[] assets = new T[guids.Length];

        for (int i = 0; i < guids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            assets[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);
        }

        return assets;
    }
#endif
}
