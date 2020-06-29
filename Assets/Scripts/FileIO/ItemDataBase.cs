#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class ItemDataBase : ScriptableObject
{
    [SerializeField] Item[] items;

    public Item GetItemReference(string itemID)
    {
        foreach(Item item in items)
        {
            if(item.ID == itemID)
            {
                return item;
            }
        }

        return null;
    }

    public Item GetItemCopy(string itemID)
    {
        Item item = GetItemReference(itemID);
        if(item == null)
        {
            return null;
        }
        else
        {
            return item.GetCopy();
        }
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        LoadItems();
    }
    private void OnEnable()
    {
        EditorApplication.projectChanged += LoadItems;
    }
    private void OnDisable()
    {
        EditorApplication.projectChanged -= LoadItems;
    }
    private void LoadItems()
    {
        items = FindAssetsByType<Item>("Assets/Resources/Items");
    }

    //load item database type auto https://answers.unity.com/questions/486545/getting-all-assets-of-the-specified-type.html?childToView=1216386#answer-1216386
    public static T[] FindAssetsByType<T>(params string[] folders) where T: Object
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

        for(int i = 0; i < guids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            assets[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);
        }

        return assets;
    }
#endif 
}
