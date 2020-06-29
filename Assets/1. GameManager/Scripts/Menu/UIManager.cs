﻿
using System;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private PauseMenu _pauseMenu;

    [SerializeField] private Camera _dummyCam;

    public Events.EventFadeComplete OnMainMenuFadeComplete;
    private void Start()
    {
        _mainMenu.OnMenuFadeCompelte.AddListener(HandleMainMenuFadeComplete);
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleMainMenuFadeComplete(bool fadeOut)
    {
        OnMainMenuFadeComplete.Invoke(fadeOut);
    }

    private void Update()
    {
        if(GameManager.Instance.CurrentGameState != GameManager.GameState.PREGAME)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //_mainMenu.FadeOut();
            GameManager.Instance.StartGame();
        }
    }
    public void SetDummyCameraActive(bool active)
    {
        _dummyCam.gameObject.SetActive(active);
    }

    void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        _pauseMenu.gameObject.SetActive(currentState == GameManager.GameState.PAUSE);
    }
}
