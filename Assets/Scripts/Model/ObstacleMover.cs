using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameManager _gameManager;
    

    void Update()
    {
        if(GameManager.Instance.IsGamePlayed)
        {
            transform.position += Vector3.left * _speed * Time.deltaTime;
        }
    }
}
