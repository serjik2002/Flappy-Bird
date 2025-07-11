using UnityEngine;
using System.Collections.Generic;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private int _clonesCount = 2; // Обычно хватает 2-3 копий

    [SerializeField] private float _firstLayerSpeed;
    [SerializeField] private float _secondLayerSpeed;
    [SerializeField] private float _thirdLayerSpeed;

    [SerializeField] private GameObject _firstLayerPrefab;
    [SerializeField] private GameObject _secondLayerPrefab;
    [SerializeField] private GameObject _thirdLayerPrefab;

    private List<GameObject> _firstLayer;
    private List<GameObject> _secondLayer;
    private List<GameObject> _thirdLayer;

    private bool _isMoving = true;

    private float[] _layerWidths = new float[3];

    private void InitializeLayers()
    {
        // Инициализируем списки
        _firstLayer = new List<GameObject>();
        _secondLayer = new List<GameObject>();
        _thirdLayer = new List<GameObject>();

        // Создаем и позиционируем первый слой
        for (int i = 0; i < _clonesCount; i++)
        {
            var firstLayerObj = Instantiate(_firstLayerPrefab, transform);
            _firstLayer.Add(firstLayerObj);

            if (i == 0)
            {
                _layerWidths[0] = Mathf.Round(firstLayerObj.GetComponent<SpriteRenderer>().bounds.size.x * 100f) / 100f;
                firstLayerObj.transform.position = Vector3.zero;
            }
            else
            {
                
                firstLayerObj.transform.position = new Vector3(i * (_layerWidths[0]), 0, 0);
            }
        }

        // Создаем и позиционируем второй слой
        for (int i = 0; i < _clonesCount; i++)
        {
            var secondLayerObj = Instantiate(_secondLayerPrefab, transform);
            _secondLayer.Add(secondLayerObj);

            if (i == 0)
            {
                _layerWidths[1] = secondLayerObj.GetComponent<SpriteRenderer>().bounds.size.x;
                secondLayerObj.transform.position = Vector3.zero;
            }
            else
            {
                secondLayerObj.transform.position = new Vector3(i * _layerWidths[1], 0, 0);
            }
        }

        // Создаем и позиционируем третий слой
        for (int i = 0; i < _clonesCount; i++)
        {
            var thirdLayerObj = Instantiate(_thirdLayerPrefab, transform);
            _thirdLayer.Add(thirdLayerObj);

            if (i == 0)
            {
                _layerWidths[2] = thirdLayerObj.GetComponent<SpriteRenderer>().bounds.size.x;
                thirdLayerObj.transform.position = Vector3.zero;
            }
            else
            {
                thirdLayerObj.transform.position = new Vector3(i * _layerWidths[2], 0, 0);
            }
        }
    }

    private void Start()
    {
        InitializeLayers();
        GameManager.Instance.OnGameStarted.AddListener(() => _isMoving = true);
        GameManager.Instance.OnGameOver.AddListener(() => _isMoving = false);
        GameManager.Instance.OnRestartGame.AddListener(() => _isMoving = true);
    }

    private void Update()
    {
        if (!_isMoving)
            return;
        MoveLayer(_firstLayer, _firstLayerSpeed, _layerWidths[0]);
        MoveLayer(_secondLayer, _secondLayerSpeed, _layerWidths[1]);
        MoveLayer(_thirdLayer, _thirdLayerSpeed, _layerWidths[2]);
    }

    private void MoveLayer(List<GameObject> layer, float speed, float width)
    {
        for (int i = 0; i < layer.Count; i++)
        {
            // Двигаем объект
            layer[i].transform.position += Vector3.left * speed * Time.deltaTime;

            // Проверяем, нужно ли переместить объект в конец
            if (layer[i].transform.position.x < -width)
            {
                // Находим самый правый объект в слое
                GameObject rightmost = GetRightmostObject(layer);

                // Перемещаем текущий объект за самый правый
                layer[i].transform.position = new Vector3(
                    rightmost.transform.position.x + width - 0.01f,
                    rightmost.transform.position.y,
                    rightmost.transform.position.z);
            }
        }
    }

    private GameObject GetRightmostObject(List<GameObject> objects)
    {
        GameObject rightmost = objects[0];
        foreach (var obj in objects)
        {
            if (obj.transform.position.x > rightmost.transform.position.x)
            {
                rightmost = obj;
            }
        }
        return rightmost;
    }
}