using UnityEngine;

public class GameManager : MonoBehaviour
{
    int StageNum = 1;
    // �̱��� �ν��Ͻ�
    public static GameManager Instance { get; private set; }

    // ���� ���� Enum
    public enum GameState { MainMenu, Playing, Paused, GameOver }
    public GameState CurrentState { get; private set; } = GameState.MainMenu;

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
    }

    public int CurrentStageNumber
    {
        get => StageNum;
        set => StageNum = value;
    }
}
