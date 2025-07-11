using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioListener : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField] private bool _isMusic;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        AudioManager.Instance.OnMusicToggle.AddListener(OnMusicToggle);
        AudioManager.Instance.OnSoundToggle.AddListener(OnSoundToggle);
        OnSoundToggle();
        OnMusicToggle();
    }


    public void OnMusicToggle()
    {
        if (_isMusic)
        {
            _audioSource.mute = AudioManager.Instance.IsMusicMuted;
        }
    }

    public void OnSoundToggle()
    {
        if (!_isMusic)
        {
            _audioSource.mute = AudioManager.Instance.IsSoundMuted;
        }
    }
}
