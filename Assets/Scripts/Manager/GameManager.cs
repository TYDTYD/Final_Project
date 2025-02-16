using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class GameManager : MonoBehaviour
{
    int StageNum = 1;
    // �̱��� �ν��Ͻ�
    public static GameManager Instance { get; private set; }

    // ���� ���� Enum
    public enum GameState { MainMenu, Playing, Paused, GameOver }
    public GameState CurrentState { get; private set; } = GameState.MainMenu;

    public Action SceneLoad;

    private void Awake()
    {
        // �̱��� ���� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ���� �ÿ��� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ� ����
        }

        SceneManager.sceneLoaded += OnSceneLoad;
    }
    void Start()
    {
        Application.targetFrameRate = 60;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        
    }

    public int CurrentStageNumber
    {
        get => StageNum;
        set => StageNum = value;
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        SceneLoad();
    }
}