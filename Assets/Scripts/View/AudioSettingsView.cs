using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioSettingsView : MonoBehaviour
{
    [SerializeField] private Image _soundSwitch;
    [SerializeField] private float transitionDuration = 0.3f;

    private Coroutine moveCoroutine;

    private void Start()
    {
        AudioManager.Instance.OnMutedToggle.AddListener(ToggleSound);
        ToggleSound();
    }

    public void ToggleSound()
    {
        print("ToggleSound");

        Vector3 targetPos;

        if (AudioManager.Instance.IsMuted)
            targetPos = new Vector3(0f, 50f, 0f);
        else
            targetPos = new Vector3(0f, -50f, 0f);

        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(MoveToPosition(targetPos));
    }

    private IEnumerator MoveToPosition(Vector3 targetPos)
    {
        Vector3 startPos = _soundSwitch.rectTransform.localPosition;
        float elapsed = 0f;

        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / transitionDuration);
            _soundSwitch.rectTransform.localPosition = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        _soundSwitch.rectTransform.localPosition = targetPos;
    }
}
