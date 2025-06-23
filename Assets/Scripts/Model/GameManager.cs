using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;

    public bool IsGamePlayed {  get; private set; }
    public static GameManager Instance { get; private set; }

    public UnityEvent OnGameStart;
    public UnityEvent OnGameEnd;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        _player.OnPlayerDead.AddListener(Stop);
        IsGamePlayed = false;
    }

    public void Play()
    {
        OnGameStart?.Invoke();
        _player.StateMachine.ChangeState(new WaitState(_player));
        IsGamePlayed = true;
    }

    public void Stop()
    {
        OnGameEnd?.Invoke();
        IsGamePlayed = false;
    }

}
