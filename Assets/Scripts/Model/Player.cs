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
        GameManager.Instance.OnGameStarted.AddListener(Play);
        StateMachine.Initialize(new WaitState(this));
    }

    private void Update()
    {
        StateMachine.CurrentState?.HandleInput();
        StateMachine.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
        {
            OnPlayerDead?.Invoke();
        }
        if (collision.tag == "checkPoint")
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
        //this.StateMachine.ChangeState(new F(this));
    }
}

