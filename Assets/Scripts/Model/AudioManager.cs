using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource[] _allAudioSources;
    public bool IsMuted {  get; private set; }
    public UnityEvent OnMutedToggle;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            IsMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
            _allAudioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.InstanceID);
            ApplyMute();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleMute()
    {
        IsMuted = !IsMuted;
        PlayerPrefs.SetInt("Muted", IsMuted ? 1 : 0);
        ApplyMute();
        OnMutedToggle.Invoke();
    }

    void ApplyMute()
    {
        foreach (var source in _allAudioSources)
        {
            source.mute = IsMuted;
        }
    }
}
