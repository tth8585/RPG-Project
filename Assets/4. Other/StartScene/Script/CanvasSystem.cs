
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasSystem : MonoBehaviour
{
    [SerializeField] GameObject startMenuOb;
    [SerializeField] GameObject selectClassOb;
    [SerializeField] GameObject imageGoSelectedClass;

    private void Start()
    {
        UIEvent.OnAddNewClass += NewClass;
    }
    private void NewClass()
    {
        startMenuOb.SetActive(false);
        selectClassOb.SetActive(true);
    }

    public void BackToStartMenu()
    {
        startMenuOb.SetActive(true);
        selectClassOb.SetActive(false);
    }

    public void NextToMainScene()
    {
        imageGoSelectedClass.GetComponent<Animator>().Play("FadeIn");
        StartCoroutine(NewGameFadeInFromSelected());
    }
    private IEnumerator NewGameFadeInFromSelected()
    {
        yield return new WaitForSeconds(4f);
        //imageGo.GetComponent<Animator>().Play("FadeOut");
        StartMenuInput.Instance.ShowUI();
        StartMenuInput.Instance.imageGoMain.GetComponent<Animator>().Play("FadeOut");

        yield return new WaitForSeconds(0.1f);
        selectClassOb.SetActive(false);
        BGMController.Instance.PlaySound(BGMController.Music.Ocean);
    }
}
