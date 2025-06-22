using UnityEngine;
using UnityEngine.UI;

public class UIManger : MonoBehaviour
{
    [SerializeField] private GameObject _playBtn;

    public static UIManger Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        GameManager.Instance.OnGameStart.AddListener(Play);
        GameManager.Instance.OnGameEnd.AddListener(Stop);
    }

    private void Play()
    {
        _playBtn.SetActive(false);
    }

    private void Stop()
    {
        _playBtn.SetActive(true);
    }
}
