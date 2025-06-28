using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Vector3 _startPosition;

    public StateMachine StateMachine { get; private set; }
    public Vector3 StartPosition => _startPosition;

    public UnityEvent OnPlayerDead;
    public UnityEvent OnCheckpointEnter;

    private void Awake()
    {
        StateMachine = new StateMachine();
    }

    private void Start()
    {
        StateMachine.Initialize(new WaitState(this));
        GameManager.Instance.OnGameStarted.AddListener(Play);
        GameManager.Instance.OnGameOver.AddListener(GameOver);
        GameManager.Instance.OnRestartGame.AddListener(RestartGame);
    }

    private void Update()
    {
        StateMachine.CurrentState?.HandleInput();
        StateMachine.Update();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Obstacle")
        {
            OnPlayerDead?.Invoke();
        }
        if (collision.collider.tag == "checkPoint")
        {
            OnCheckpointEnter?.Invoke();
        }
    }

    public void Jump()
    {
        _rigidbody.linearVelocity = Vector2.up * _jumpForce;
    }

    public void SetStartPosition()
    {
        transform.position = _startPosition;
    }

    public void Play()
    {
        StateMachine.ChangeState(new FlyState(this));
    }

    public void GameOver()
    {
        StateMachine.ChangeState(new DieState(this));
    }

    public void RestartGame()
    {
        SetStartPosition();
        StateMachine.ChangeState(new WaitState(this));
    }
}

