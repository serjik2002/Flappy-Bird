using UnityEngine;
using UnityEngine.UI;
using System.Collections; // System.Collections ����� ��� Coroutines

public class AudioSettingsView : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image _soundSwitch;
    [SerializeField] private Image _musicSwitch;

    [Header("Animation Settings")]
    [SerializeField] private float transitionDuration = 0.3f;
    [SerializeField] private float mutedOffset = 50f;   // �������� ��� ������������ ���������
    [SerializeField] private float unmutedOffset = -50f; // �������� ��� �������������� ���������

    // ���������� ��������� �������� ��� ������ ������, ����� ��� �� �������������
    private Coroutine _soundMoveCoroutine;
    private Coroutine _musicMoveCoroutine;

    private void Start()
    {
        // ������������� �� ������� ��� ��������� �������
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.OnSoundToggle.AddListener(UpdateSoundSwitch);
            AudioManager.Instance.OnMusicToggle.AddListener(UpdateMusicSwitch);
        }

        // ������������� ��������� ��������� ��������������
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

        // ������������� ��� �������� ��������, ����� ��� �� �������� �������� UI ������������� �������
        if (_soundMoveCoroutine != null) StopCoroutine(_soundMoveCoroutine);
        if (_musicMoveCoroutine != null) StopCoroutine(_musicMoveCoroutine);
    }

    // ���� ����� ����� ���������� �������� OnSoundToggle
    public void UpdateSoundSwitch()
    {
        print("UpdateSoundSwitch called"); // ��� �������

        Vector3 targetPos = GetTargetPosition(AudioManager.Instance.IsSoundMuted);

        // ��������� �������� ��� _soundSwitch
        if (_soundMoveCoroutine != null)
            StopCoroutine(_soundMoveCoroutine);

        _soundMoveCoroutine = StartCoroutine(MoveSwitch(_soundSwitch, targetPos));
    }

    // ���� ����� ����� ���������� �������� OnMusicToggle
    public void UpdateMusicSwitch()
    {
        print("UpdateMusicSwitch called"); // ��� �������

        Vector3 targetPos = GetTargetPosition(AudioManager.Instance.IsMusicMuted);

        // ��������� �������� ��� _musicSwitch
        if (_musicMoveCoroutine != null)
            StopCoroutine(_musicMoveCoroutine);

        _musicMoveCoroutine = StartCoroutine(MoveSwitch(_musicSwitch, targetPos));
    }

    // ��������������� ����� ��� ����������� ������� �������
    private Vector3 GetTargetPosition(bool isMuted)
    {
        return new Vector3(0f, isMuted ? mutedOffset : unmutedOffset, 0f);
    }

    // ����� ����� ��� �������� ����������� �������������
    private IEnumerator MoveSwitch(Image switchImage, Vector3 targetPos)
    {
        if (switchImage == null) yield break; // ������ �� NullReference

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

    // ������ ��� ������ �� UI (������)
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