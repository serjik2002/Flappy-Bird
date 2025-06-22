using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private Queue<GameObject> _objectPool = new Queue<GameObject>();
    private GameObject _prefab;
    private bool _isExpandable;
    private List<GameObject> _activeObjects = new List<GameObject>();

    public List<GameObject> ActiveObjects => _activeObjects;

    public ObjectPool(GameObject prefab, int initialSize, bool isExpandable = true)
    {
        this._prefab = prefab;
        this._isExpandable = isExpandable;

        for (int i = 0; i < initialSize; i++)
        {
            CreateNewObject();
        }
    }

    public GameObject GetObjectFromPool()
    {
        if (_isExpandable && _objectPool.Count == 0)
        {
            CreateNewObject();
        }
        GameObject obj = _objectPool.Dequeue();
        obj.SetActive(true);
        _activeObjects.Add(obj);
        return obj;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        _objectPool.Enqueue(obj);
        _activeObjects.Remove(obj);
    }

    private void CreateNewObject()
    {
        GameObject newObj = Object.Instantiate(_prefab);
        newObj.SetActive(false);
        _objectPool.Enqueue(newObj);
    }

    public bool TryGetObject()
    {
        return _objectPool.Count > 0;
    }
}
