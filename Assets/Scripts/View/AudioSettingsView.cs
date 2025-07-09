using UnityEngine;
using UnityEngine.UI;
using System.Collections; // System.Collections нужен для Coroutines

public class AudioSettingsView : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image _soundSwitch;
    [SerializeField] private Image _musicSwitch;

    [Header("Animation Settings")]
    [SerializeField] private float transitionDuration = 0.3f;
    [SerializeField] private float mutedOffset = 50f;   // Смещение для заглушенного состояния
    [SerializeField] private float unmutedOffset = -50f; // Смещение для незаглушенного состояния

    // Используем отдельные корутины для каждой кнопки, чтобы они не конфликтовали
    private Coroutine _soundMoveCoroutine;
    private Coroutine _musicMoveCoroutine;

    private void Start()
    {
        // Подписываемся на события при включении объекта
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.OnSoundToggle.AddListener(UpdateSoundSwitch);
            AudioManager.Instance.OnMusicToggle.AddListener(UpdateMusicSwitch);
        }

        // Устанавливаем начальное положение переключателей
        UpdateSoundSwitch();
        UpdateMusicSwitch();
    }

    private void OnDisable()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.OnSoundToggle.RemoveListener(UpdateSoundSwitch);
            AudioManager.Instance.OnMusicToggle.RemoveListener(UpdateMusicSwitch);
        }

        // Останавливаем все активные корутины, чтобы они не пытались обновить UI уничтоженного объекта
        if (_soundMoveCoroutine != null) StopCoroutine(_soundMoveCoroutine);
        if (_musicMoveCoroutine != null) StopCoroutine(_musicMoveCoroutine);
    }

    // Этот метод будет вызываться событием OnSoundToggle
    public void UpdateSoundSwitch()
    {
        print("UpdateSoundSwitch called"); // Для отладки

        Vector3 targetPos = GetTargetPosition(AudioManager.Instance.IsSoundMuted);

        // Запускаем корутину для _soundSwitch
        if (_soundMoveCoroutine != null)
            StopCoroutine(_soundMoveCoroutine);

        _soundMoveCoroutine = StartCoroutine(MoveSwitch(_soundSwitch, targetPos));
    }

    // Этот метод будет вызываться событием OnMusicToggle
    public void UpdateMusicSwitch()
    {
        print("UpdateMusicSwitch called"); // Для отладки

        Vector3 targetPos = GetTargetPosition(AudioManager.Instance.IsMusicMuted);

        // Запускаем корутину для _musicSwitch
        if (_musicMoveCoroutine != null)
            StopCoroutine(_musicMoveCoroutine);

        _musicMoveCoroutine = StartCoroutine(MoveSwitch(_musicSwitch, targetPos));
    }

    // Вспомогательный метод для определения целевой позиции
    private Vector3 GetTargetPosition(bool isMuted)
    {
        return new Vector3(0f, isMuted ? mutedOffset : unmutedOffset, 0f);
    }

    // Общий метод для анимации перемещения переключателя
    private IEnumerator MoveSwitch(Image switchImage, Vector3 targetPos)
    {
        if (switchImage == null) yield break; // Защита от NullReference

        Vector3 startPos = switchImage.rectTransform.localPosition;
        float elapsed = 0f;

        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / transitionDuration);
            switchImage.rectTransform.localPosition = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        switchImage.rectTransform.localPosition = targetPos;
    }

    // Методы для вызова из UI (кнопок)
    public void OnSoundButtonClick()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ToggleSoundMute();
        }
    }

    public void OnMusicButtonClick()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ToggleMusicMute();
        }
    }
}