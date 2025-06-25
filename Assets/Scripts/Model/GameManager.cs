using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;

    public static GameManager Instance { get; private set; }

    public GameState CurrentState { get; private set; }

    public bool IsPlaying { get; private set; }
    public bool IsPaused { get; private set; }

    public UnityEvent OnGameStateChanged;

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

    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState) return;
        CurrentState = newState;
        OnGameStateChanged?.Invoke();
    }
    //private void Start()
    //{
    //    _player.OnPlayerDead.AddListener(Stop);
    //    IsGamePlayed = false;
    //}

    //public void Play()
    //{
    //    OnGameStarted?.Invoke();
    //    IsGamePlayed = true;
    //}

    //public void Stop()
    //{
    //    OnGameOver?.Invoke();
    //    IsGamePlayed = false;
    //}

}


public enum GameState
{

}

public abstract class GameStateHandlerBase : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChange);
    }

    private void OnDisable()
    {
        
    }

    private void HandleGameStateChange(GameState newState)
    {
        OnGameStateChanged(newState);
    }

    protected abstract void OnGameStateChanged(GameState state);

}