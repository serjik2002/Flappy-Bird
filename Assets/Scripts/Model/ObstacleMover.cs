using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private bool _isMoving = false;

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStarted.AddListener(StartMoving);
        GameManager.Instance.OnGameResumed.AddListener(StartMoving);
        GameManager.Instance.OnGameOver.AddListener(StopMoving);
        GameManager.Instance.OnGamePaused.AddListener(StopMoving);
    }

    void Update()
    {
        if(_isMoving)
        {
            transform.position += Vector3.left * _speed * Time.deltaTime;
        }
    }

    private void StartMoving()
    {
        _isMoving = true;
    }

    private void StopMoving()
    {
        _isMoving = false;
    }
}
