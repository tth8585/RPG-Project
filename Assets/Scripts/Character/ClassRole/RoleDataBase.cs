
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class RoleDataBase : ScriptableObject
{
    [SerializeField] RoleStat[] roles;

    public RoleStat GetRoleReference(string roleType)
    {
        foreach (RoleStat role in roles)
        {
            if (roleType == role.nameRole)
            {
                return role;
            }
        }

        return null;
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        LoadRoles();
    }
    private void OnEnable()
    {
        EditorApplication.projectChanged += LoadRoles;
    }
    private void OnDisable()
    {
        EditorApplication.projectChanged -= LoadRoles;
    }
    private void LoadRoles()
    {
        roles = FindAssetsByType<RoleStat>("Assets/Resources/Roles");
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
