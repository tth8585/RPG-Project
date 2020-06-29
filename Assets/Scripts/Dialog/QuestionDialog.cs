using System;
using UnityEngine;

public class QuestionDialog : MonoBehaviour
{
    public static QuestionDialog Instance { get; set; }
    public event Action OnYesEvent;
    public event Action OnNoEvent;

    public GameObject QuestionDialogObject;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    public void Show()
    {
        QuestionDialogObject.SetActive(true);
        OnYesEvent = null;
        OnNoEvent = null;
        QuestionDialogObject.GetComponent<RectTransform>().SetAsLastSibling();
    }
    public void Hide()
    {
        QuestionDialogObject.SetActive(false);
    }
    public void OnYesBtnClick()
    {
        if (OnYesEvent != null)
        {
            OnYesEvent();
        }

        Hide();
    }

    public void OnNoBtnClick()
    {
        if (OnNoEvent != null)
        {
            OnNoEvent();
        }
        Hide();
    }
}
