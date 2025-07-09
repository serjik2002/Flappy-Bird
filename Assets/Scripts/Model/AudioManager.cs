using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public bool IsSoundMuted { get; private set; }
    public bool IsMusicMuted { get; private set; }

    public UnityEvent OnSoundToggle;
    public UnityEvent OnMusicToggle;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            IsSoundMuted = PlayerPrefs.GetInt("SoundMuted", 0) == 1;
            IsMusicMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleSoundMute()
    {
        IsSoundMuted = !IsSoundMuted;
        PlayerPrefs.SetInt("SoundMuted", IsSoundMuted ? 1 : 0);

        OnSoundToggle.Invoke();

        Debug.Log($"Sound Muted: {IsSoundMuted}");
    }

    public void ToggleMusicMute()
    {
        IsMusicMuted = !IsMusicMuted;
        PlayerPrefs.SetInt("MusicMuted", IsMusicMuted ? 1 : 0);

        OnMusicToggle.Invoke();

        Debug.Log($"Music Muted: {IsMusicMuted}");
    }

    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;

        if (!IsSoundMuted)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
        }
    }

    public void PlayMusic(AudioSource musicSource, AudioClip clip, float volume = 1f, bool loop = true)
    {
        if (musicSource == null || clip == null) return;

        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.loop = loop;

        musicSource.mute = IsMusicMuted;

        if (!IsMusicMuted)
        {
            musicSource.Play();
        }
        else
        {
            musicSource.Stop();
        }
    }


}