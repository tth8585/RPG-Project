
using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button ResumeBtn;
    [SerializeField] private Button RestartBtn;
    [SerializeField] private Button QuitBtn;

    private void Start()
    {
        ResumeBtn.onClick.AddListener(HandleResumeClick);
        RestartBtn.onClick.AddListener(HandleRestartClick);
        QuitBtn.onClick.AddListener(HandleQuitClick);
    }

    private void HandleResumeClick()
    {
        GameManager.Instance.TogglePause();
    }
    private void HandleRestartClick()
    {
        GameManager.Instance.RestartGame();
    }
    private void HandleQuitClick()
    {
        GameManager.Instance.QuitGame();
    }
}
