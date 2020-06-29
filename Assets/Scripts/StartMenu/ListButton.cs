using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListButton : MonoBehaviour
{
    private void Start()
    {
        transform.Find("NewGame").GetComponent<UI_Button>().ClickFunc = () => Debug.Log("click");
        transform.Find("NewGame").GetComponent<UI_Button>().AddButtonSounds();

        transform.Find("Options").GetComponent<UI_Button>().ClickFunc = () =>
        {
            Debug.Log("click");
            Loader.Load(Loader.Scene.SettingMenu);
        };
        transform.Find("Options").GetComponent<UI_Button>().AddButtonSounds();

        transform.Find("Quit").GetComponent<UI_Button>().ClickFunc = () => Debug.Log("click");
        transform.Find("Quit").GetComponent<UI_Button>().AddButtonSounds();
    }
}
