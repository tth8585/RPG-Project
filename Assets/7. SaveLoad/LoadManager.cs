
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    public static LoadManager instance;
    public Data data;
    [SerializeField] private PlayerController c;
    string dataFile = "7472756f6e6774686169686f616e67.dat";
    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else if(instance !=this)
        {
            Destroy(this.gameObject);
        }
    }

    public void LoadData()
    {
        
        string filePath = Application.persistentDataPath + "/" + dataFile;
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(filePath))
        {
            FileStream file = File.Open(filePath, FileMode.Open);
            Data loaded = (Data)bf.Deserialize(file);
            data = loaded;
            file.Close();
        }

        GetData();
        Debug.Log("Load data");
    }

    public void SaveData()
    {
        SetData();
        string filePath = Application.persistentDataPath + "/" + dataFile;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(filePath);
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Save data");
    }

    public void LoadItemData(PlayerController playerController)
    {
        ItemSaveManager.Instance.LoadEquipment(playerController);
        ItemSaveManager.Instance.LoadInventory();
        ItemSaveManager.Instance.LoadStash();
    }
    public void SaveItemData()
    {
        ItemSaveManager.Instance.SaveInventory();
        ItemSaveManager.Instance.SaveEquipment();
        ItemSaveManager.Instance.SaveStash();
    }
    private void SetData()
    {
        data.pos.x = c.transform.position.x;
        data.pos.y = c.transform.position.y;
        data.pos.z = c.transform.position.z;

        data.level = c.transform.GetComponent<LevelUpSystem>().currentLevel;
        data.exp = c.transform.GetComponent<LevelUpSystem>().currentXP;

        SetVolume();
    }
    private void GetData()
    {
        c.transform.position = data.pos.toVector();
        
        c.transform.GetComponent<LevelUpSystem>().currentLevel = data.level;
        c.transform.GetComponent<LevelUpSystem>().currentXP = data.exp;
        c.transform.GetComponent<LevelUpSystem>().AddXP(0);

        OptionsMenu.Instance.SetVolume(data.volumeSize);
        StatPanel.Instance.UpdateStatValue();
    }

    public void SetVolume()
    {
        data.volumeSize = OptionsMenu.Instance.GetVolume();
    }
}
[System.Serializable]
public class Data
{
    public Point pos;
    public int level = 1;
    public int exp = 0;

    public float volumeSize = 0;

    public Data()
    {
        level = 1;
        exp = 0;
    }
}

[System.Serializable]
public class Point
{
    public float x;
    public float y;
    public float z;

    public Point(Vector3 p)
    {
        x = p.x; y = p.y; z = p.z;
    }
    public Vector3 toVector()
    {
        return new Vector3(x, y, z);
    }
}
