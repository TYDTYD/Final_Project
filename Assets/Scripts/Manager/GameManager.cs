using UnityEngine;

public class GameManager : MonoBehaviour
{
    int StageNum = 1;
    // 싱글톤 인스턴스
    public static GameManager Instance { get; private set; }

    // 게임 상태 Enum
    public enum GameState { MainMenu, Playing, Paused, GameOver }
    public GameState CurrentState { get; private set; } = GameState.MainMenu;

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
        }
    }

    public int CurrentStageNumber
    {
        get => StageNum;
        set => StageNum = value;
    }
}
