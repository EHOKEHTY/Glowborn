using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundSystem : MonoBehaviour
{
    [SerializeField] private AudioClip[] Sounds;
    [SerializeField] private AudioClip[] Musics;
    [SerializeField] private float _musicVolume;
    [SerializeField] private float _SFXVolume;
    private AudioSource _audioSource => GetComponent<AudioSource>();
    private void Start()
    {
        _musicVolume = PlayerPrefs.GetFloat("musicVolume");
        _SFXVolume = PlayerPrefs.GetFloat("SFXVolume");
    }
    public void PlaySound(AudioClip clip, float p1 = 0.8f, float p2 = 1.2f)
    {
        _audioSource.pitch = Random.Range(p1, p2);
        _audioSource.PlayOneShot(clip, _SFXVolume);
    }
    public void PlayMusic(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip, _musicVolume);
    }
}