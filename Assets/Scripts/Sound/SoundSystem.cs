using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    const string SFXVOLUME = "sfxVol", MUSICVOLUME = "musicVolume";

    public static SoundSystem Instance;

    [Header("Аудиоисточники")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Громкость")]
    [Range(0f, 1f)] public float musicVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        musicVolume = PlayerPrefs.GetFloat(MUSICVOLUME, 1f);
        sfxVolume = PlayerPrefs.GetFloat(SFXVOLUME, 1f);
        ApplyVolumes();
    }

    public static void Play(AudioClip clip, AudioType type = AudioType.SFX)
    {
        if (clip == null) return;
        if (type == AudioType.Music)
            Instance.musicSource.PlayOneShot(clip, Instance.musicVolume);
        else
            Instance.sfxSource.PlayOneShot(clip, Instance.sfxVolume);
    }

    public static void Play(AudioGroup group)
    {
        if (group == null) return;
        Play(group.GetRandomClip(), group.type);
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        ApplyVolumes();
    }

    public void SetSfxVolume(float volume)
    {
        sfxVolume = volume;
        ApplyVolumes();
    }

    private void ApplyVolumes()
    {
        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
    }   
}
