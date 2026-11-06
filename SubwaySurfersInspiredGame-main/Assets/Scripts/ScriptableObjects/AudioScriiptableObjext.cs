using UnityEngine;

[CreateAssetMenu(fileName = "AudioSetting", menuName = "Audio/Volume Setting")]
public class AudioSetting : ScriptableObject
{
    [Range(0f, 1f)]
    public float volume = 0.1f;
}
