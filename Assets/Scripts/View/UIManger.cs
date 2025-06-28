using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManger : MonoBehaviour
{
    [SerializeField] private Button _playBtn;

    private GameObject _gameOverPanel;

    public static UIManger Instance { get; private set; }



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        _gameOverPanel = GameObject.Find("GameOverPanel");
        GameManager.Instance.OnGameOver.AddListener(ShowGameOverPanel);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    private void ShowGameOverPanel()
    {
        _gameOverPanel.SetActive(true);
    }
    
}
