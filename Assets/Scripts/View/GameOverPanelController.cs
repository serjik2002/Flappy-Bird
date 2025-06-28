using UnityEngine;

public class GameOverPanelController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
        GameManager.Instance.OnGameOver.AddListener(() => gameObject.SetActive(true));
        GameManager.Instance.OnRestartGame.AddListener(() => gameObject.SetActive(false));
    }

}
