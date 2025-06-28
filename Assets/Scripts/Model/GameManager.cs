using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;

    public static GameManager Instance { get; private set; }

    public bool IsPlaying { get; private set; }
    public bool IsPaused { get; private set; }

    public UnityEvent OnGameStarted;
    public UnityEvent OnGameOver;
    public UnityEvent OnGamePaused;
    public UnityEvent OnGameResumed;
    public UnityEvent OnRestartGame;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        _player.OnPlayerDead.AddListener(GameOver);
    }

    public void StartGame()
    {
        OnGameStarted?.Invoke();
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }

    public void PauseGame()
    {
        OnGamePaused?.Invoke();
        Time.timeScale = 0f;
    }


    public void ResumeGame()
    {
        OnGameResumed?.Invoke();
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        OnRestartGame?.Invoke();
    }
}
