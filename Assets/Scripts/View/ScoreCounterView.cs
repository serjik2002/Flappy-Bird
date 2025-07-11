using System.Collections;
using UnityEngine;

public class ScoreCounterView : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private TMPro.TMP_Text _scoreText;
    
    [Header("Animation Settings")]
    [SerializeField] private TMPro.TMP_Text _scoreTextAnimation;
    [SerializeField] private float _animationDuration = 0.5f; // Длительность анимации

    private void Start()
    {
        _scoreCounter.OnScoreChanged.AddListener(UpdateScore);
        GameManager.Instance.OnGameOver.AddListener(AnimateScore);
    }

    public void UpdateScore()
    {
        _scoreText.text = _scoreCounter.ScoreCountetValue.ToString();
    }

    public void AnimateScore()
    {
        StopAllCoroutines(); // Остановим предыдущую анимацию, если она была
        StartCoroutine(AnimateScoreCoroutine());
    }

    private IEnumerator AnimateScoreCoroutine()
    {
        yield return new WaitForSeconds(1f); // Небольшая задержка перед началом анимации
        float elapsed = 0f;
        int startValue = 0;
        int endValue = _scoreCounter.ScoreCountetValue;

        while (elapsed < _animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / _animationDuration);
            int currentValue = Mathf.RoundToInt(Mathf.Lerp(startValue, endValue, t));
            _scoreTextAnimation.text = currentValue.ToString();
            yield return null;
        }

        // Финальное значение, чтобы точно совпадало
        _scoreTextAnimation.text = endValue.ToString();
    }

}
