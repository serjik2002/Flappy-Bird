using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Collider2D _collider;

    public UnityEvent OnPlayerDead;
    public UnityEvent OnCheckpointEnter;

    private void Start()
    {
        //GameManager.Instance.OnGameStart
        _rigidbody.bodyType = RigidbodyType2D.Static;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
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
