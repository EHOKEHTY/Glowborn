using UnityEngine;

[CreateAssetMenu(menuName = "Audio/AudioGroup")]
public class AudioGroup : ScriptableObject
{
    public AudioType type;
    public AudioClip[] clips;

    public AudioClip GetRandomClip()
    {
        if (clips.Length == 0) return null;
        return clips[Random.Range(0, clips.Length)];
    }
}

public enum AudioType
{
    Music,
    SFX
}
