using System;
using UnityEditor;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _obstaclePrefab;

    [SerializeField] private float _spawnTime;
    [SerializeField] private float _minHeighRange;
    [SerializeField] private float _maxHeighRange;
    [SerializeField] private int _spawnCount;
    private float _timeToSpawn = 0;

    private ObjectPool _pool;

    private void Start()
    {
        _pool = new ObjectPool(_obstaclePrefab, _spawnCount, false);
    }
    private void Update()
    {
        //если игра запущена запустить спавн препятствий
        if (GameManager.Instance.IsGamePlayed)
        {
            _timeToSpawn -= Time.deltaTime;
            if (_timeToSpawn <= 0 )
            {
                Spawn();
                _timeToSpawn = _spawnTime;
            }
            for (int i = 0; i < _pool.ActiveObjects.Count; i++)
            {
                var item = _pool.ActiveObjects[i];
                if (item.transform.position.x < -5)
                {
                    _pool.ReturnObjectToPool(item);
                }
            }
        }
    }

    private void Spawn()
    {
        float spawnHeigh = UnityEngine.Random.Range(-_minHeighRange, _maxHeighRange);
        var obstacle = _pool.GetObjectFromPool();
        obstacle.transform.position = new Vector2(transform.position.x, spawnHeigh);
    }
}
