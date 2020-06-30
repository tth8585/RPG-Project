using System.Text;
using UnityEditor;
using UnityEngine;

public enum ItemRarity
{
    COMMON,
    MAGIC,
    RARE,
    UNIQUE,
}
public class Item:  ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }
    public string itemName;
    [Range(1,40)]
    public int maximumStack = 1;
    public Sprite iconItem;
    public ItemRarity itemRarity;

    protected static readonly StringBuilder stringBuilder = new StringBuilder();

    private void OnValidate()
    {
    #if UNITY_EDITOR
            string path = AssetDatabase.GetAssetPath(this);
            id = AssetDatabase.AssetPathToGUID(path);
    #endif
    }

    public virtual Item GetCopy()
    {
        return this;
    }

    public virtual void Destroy()
    {

    }

    public virtual string GetItemType()
    {
        return "";
    }

    public virtual string GetItemDescription()
    {
        return "";
    }
}
