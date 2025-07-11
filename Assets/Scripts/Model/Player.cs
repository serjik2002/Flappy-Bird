using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Vector3 _startPosition;

    [Header("Rotation Settings")]
    [SerializeField] private float _upRotation = 45f;
    [SerializeField] private float _downRotation = -90f;
    [SerializeField] private float _rotationSpeed = 5f;

    private Quaternion _targetRotation;
    private bool _isJumpingUp;

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
        _targetRotation = Quaternion.identity;
        StateMachine.Initialize(new WaitState(this));
        GameManager.Instance.OnGameStarted.AddListener(Play);
        GameManager.Instance.OnGameOver.AddListener(GameOver);
        GameManager.Instance.OnRestartGame.AddListener(RestartGame);
    }

    private void Update()
    {
        StateMachine.CurrentState?.HandleInput();
        StateMachine.Update();
        RotateBird();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Obstacle")
        {
            OnPlayerDead?.Invoke();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "checkPoint")
        {
            OnCheckpointEnter?.Invoke();
        }
    }

    public void Jump()
    {
        _rigidbody.linearVelocity = Vector2.zero;
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

        _targetRotation = Quaternion.Euler(0, 0, _upRotation);
        _isJumpingUp = true;
    }

    private void RotateBird()
    {
        if (_rigidbody.bodyType != RigidbodyType2D.Dynamic)
            return;

        // Если летим вверх — поворачиваем к целевому углу
        if (_isJumpingUp)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);

            // Остановим наклон вверх, когда достигли угла с погрешностью
            if (Quaternion.Angle(transform.rotation, _targetRotation) < 1f)
            {
                _isJumpingUp = false;
            }
        }
        else if (_rigidbody.linearVelocity.y < 0)
        {
            // Падаем — наклон вниз
            Quaternion downRotation = Quaternion.Euler(0, 0, _downRotation);
            transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, _rotationSpeed * Time.deltaTime);
        }
    }

    public void SetStartPosition()
    {
        transform.position = _startPosition;
        transform.rotation = Quaternion.identity;
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

