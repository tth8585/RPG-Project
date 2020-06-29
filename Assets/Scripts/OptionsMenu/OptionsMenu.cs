using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField] HandSlider handSlider;
    private Resolution[] resolutions;
    [SerializeField] private Dropdown resolutionDropDown;
    [SerializeField] private GameObject btnQuit;
    private void Awake()
    {
        handSlider.OnValueChanged += HandSlider_OnValueChanged;
    }
    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();
        int currentIndex = 0;
        for (int i=0;i<resolutions.Length;i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if(resolutions[i].width == Screen.currentResolution.width&& resolutions[i].height == Screen.currentResolution.height)
            {
                currentIndex = i;
            }
        }
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentIndex;
        resolutionDropDown.RefreshShownValue();

        btnQuit.GetComponent<UI_Button>().ClickFunc = () => 
        { 
            Debug.Log("click");
            Loader.Load(Loader.Scene.StartMenu);
        };
        btnQuit.GetComponent<UI_Button>().AddButtonSounds();
    }
    public void SetSize(int sizeIndex)
    {
        Resolution resolution = resolutions[sizeIndex];
        Screen.SetResolution(resolution.width, resolution.height,Screen.fullScreen);
    }
    private void HandSlider_OnValueChanged(object sender, System.EventArgs e)
    {
        SetVolume(handSlider.GetValue());
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume",volume);  
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullcreen(bool isFull)
    {
        Screen.fullScreen = isFull;
    }


}
