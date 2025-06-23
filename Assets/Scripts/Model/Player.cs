using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private Rigidbody2D _rigidbody;

    public StateMachine StateMachine { get; private set; }

    public UnityEvent OnPlayerDead;
    public UnityEvent OnCheckpointEnter;

    private void Awake()
    {
        StateMachine = new StateMachine();
    }

    private void Start()
    {
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
}

