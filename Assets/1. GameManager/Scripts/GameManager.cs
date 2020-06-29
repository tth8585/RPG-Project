
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    //what level the game curently in
    //load and unload game level
    //kepp track of the game state
    //generate other persistent systems
    public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSE,
    }

    public GameObject[] SystemPrefabs;
    public Events.EventGameState OnGameStateChanged;

    List<GameObject> _instancedSystemPrefabs;
    List<AsyncOperation> _loadOperation;
    GameState _currentGameState = GameState.PREGAME;

    private string _currentLevelName = string.Empty;
    public GameState CurrentGameState
    {
        get { return _currentGameState; }
        private set { _currentGameState = value; }
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        _instancedSystemPrefabs = new List<GameObject>();
        _loadOperation = new List<AsyncOperation>();

        InstanceSystemPrefabs();

        UIManager.Instance.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
    }
    private void Update()
    {
        if (_currentGameState == GameState.PREGAME)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    private void OnLoadOperationCompleted(AsyncOperation obj)
    {
        if (_loadOperation.Contains(obj))
        {
            _loadOperation.Remove(obj);
            if (_loadOperation.Count == 0)
            {
                UpdateState(GameState.RUNNING);
            }
        }

        Debug.Log("Load Complete");
    }
    void UpdateState(GameState state)
    {
        GameState previousGameState = _currentGameState;
        _currentGameState = state;

        switch (_currentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 1f;
                break;
            case GameState.RUNNING:
                Time.timeScale = 1f;
                break;
            case GameState.PAUSE:
                Time.timeScale = 0f;
                break;
            default:
                break;
        }

        OnGameStateChanged.Invoke(_currentGameState, previousGameState);
        //dispatch message
        //transition between scene
    }
    private void OnUnloadOperationComplete(AsyncOperation obj)
    {
        Debug.Log("Unload Complete");
    }
    void InstanceSystemPrefabs()
    {
        GameObject prefabInstance;
        for(int i = 0; i < SystemPrefabs.Length; i++)
        {
            prefabInstance = Instantiate(SystemPrefabs[i]);
            _instancedSystemPrefabs.Add(prefabInstance);
        }
    }
    void HandleMainMenuFadeComplete(bool fadeOut)
    {
        if (!fadeOut)
        {
            UnloadLevel(_currentLevelName);
        }  
    }
    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to load level " + levelName);
            return;
        }

        ao.completed += OnLoadOperationCompleted;
        _loadOperation.Add(ao);
        _currentLevelName = levelName;
    }
    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to unload level " + levelName);
            return;
        }

        ao.completed += OnUnloadOperationComplete;  
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();

        for(int i = 0; i < _instancedSystemPrefabs.Count; i++)
        {
            Destroy(_instancedSystemPrefabs[i]);
        }
        _instancedSystemPrefabs.Clear();
    }

    public void StartGame()
    {
        LoadLevel("Main");
    }
    public void TogglePause()
    {
        //if (_currentGameState == GameState.RUNNING)
        //{
        //    UpdateState(GameState.PAUSE);
        //}
        //else
        //{
        //    UpdateState(GameState.RUNNING);
        //}

        //condition ? true:false
        UpdateState(_currentGameState == GameState.RUNNING? GameState.PAUSE : GameState.RUNNING);
    }
    public void RestartGame()
    {
        UpdateState(GameState.PREGAME);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
