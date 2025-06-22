using UnityEngine;

public class ScoreCounterView : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private TMPro.TMP_Text _scoreText;

    private void Start()
    {
        _scoreCounter.OnScoreChanged.AddListener(UpdateScore);
    }

    public void UpdateScore()
    {
        _scoreText.text = _scoreCounter.ScoreCountetValue.ToString();
    }
}
