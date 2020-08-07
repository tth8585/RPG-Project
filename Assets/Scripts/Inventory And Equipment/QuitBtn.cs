
using UnityEngine;

public class QuitBtn : MonoBehaviour
{
    public void HidePanel()
    {
        gameObject.SetActive(false);
    }

    public void SaveSetting()
    {
        //LoadManager.instance.SaveData();
        gameObject.SetActive(false);
        LoadManager.instance.SetVolume();
    }
}
