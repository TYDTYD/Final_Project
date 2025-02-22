using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public partial class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public enum GameState { MainMenu, Playing, Paused, GameOver }
    public GameState CurrentState { get; private set; } = GameState.MainMenu;

    [SerializeField] GameObject playerPrefab;
    Model model;
    GameObject player;
    int stageNumber = 0;

    Vector3 Stage1Start = new Vector3(10f, 5f);
    Vector3 Stage2Start = new Vector3(6f, 6f);
    Vector3 Stage3Start = new Vector3(11f, 5f);
    Vector3 Stage4Start = new Vector3(-14f, 5f);

    public Action SceneLoad;
    public Action StageLoad;

    private void Awake()
    {
        // 싱글톤 패턴 구현
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 변경 시에도 유지
        }
        else
        {
            Destroy(gameObject); // 중복 방지
            return;
        }

        CacheScenes(scenesToLoad);
        SceneManager.sceneLoaded += OnSceneLoad;
    }
    void Start()
    {
        Application.targetFrameRate = 60;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        model = Model.Instance;
    }

    public int CurrentSceneNumber
    {
        get => SceneManager.GetActiveScene().buildIndex;
        private set { }
    }
    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        SceneLoad?.Invoke();

        int sceneNum = scene.buildIndex;
        if (IsStageScene(sceneNum))
        {
            player = InstantiatePlayer(GetStageStartPosition(sceneNum));
            StageLoad?.Invoke();
            if (model != null)
                model.CurrentOnStage = true;
        }
        else
        {
            player = null;
            if (model != null)
                model.CurrentOnStage = false;
        }
    }

    Vector3 GetStageStartPosition(int buildIndex)
    {
        return buildIndex switch
        {
            1 => Stage1Start,
            2 => Stage2Start,
            3 => Stage3Start,
            4 => Stage4Start,
            _ => Vector3.zero
        };
    }
    GameObject InstantiatePlayer(Vector3 pos) => Instantiate(playerPrefab, pos, Quaternion.identity);
    bool IsStageScene(int buildIndex) => buildIndex >= 1 && buildIndex <= 4;
    public GameObject GetPlayer => player;
    public int GetStageNumber
    {
        get => model.stage.Value = stageNumber;
        set
        {
            if (value >= 1 && value <= 4)
                stageNumber = value;
        }
    }
}