using UnityEngine;
using UnityEngine.Events;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private Player _player;

    private int _scoreCounter;

    public int ScoreCountetValue => _scoreCounter;
    public UnityEvent OnScoreChanged;

    private void Start()
    {
        _scoreCounter = 0;
        GameManager.Instance.OnGameStarted.AddListener(ResetScrore);
        GameManager.Instance.OnRestartGame.AddListener(ResetScrore);
        _player.OnCheckpointEnter.AddListener(ChangeScore);
    }

    private void ChangeScore()
    {
        _scoreCounter++;
        OnScoreChanged?.Invoke();
        print(_scoreCounter);
    }

    private void ResetScrore()
    {
        _scoreCounter = 0;
        OnScoreChanged?.Invoke();
    }
    
}
