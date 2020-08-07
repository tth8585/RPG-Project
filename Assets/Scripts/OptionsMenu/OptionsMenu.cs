using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class OptionsMenu : MonoBehaviour
{
    public static OptionsMenu Instance { get; set; }

    public AudioMixer audioMixer;
    [SerializeField] HandSlider handSlider;
    private Resolution[] resolutions;
    [SerializeField] private Dropdown resolutionDropDown;
    //[SerializeField] private GameObject btnQuit;
    [SerializeField] Text testText;
    private void Awake()
    {
        handSlider.OnValueChanged += HandSlider_OnValueChanged;

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();
        int currentIndex = 0;
        for (int i = 0; i < resolutions.Length ; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentIndex = i;
            }
        }

        //Debug.Log(currentIndex);
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentIndex;
        resolutionDropDown.RefreshShownValue();

        testText.text = currentIndex.ToString();
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

    public float GetVolume()
    {
        float volume;
        audioMixer.GetFloat("Volume", out volume);
        return volume;
    }
}
