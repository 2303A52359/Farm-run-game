using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class VolumeSlider : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;
    public AudioSetting volumeSetting; 

    void Start()
    {
        // Load value from ScriptableObject
        float savedVolume = volumeSetting.volume;

        volumeSlider.value = savedVolume;
        audioSource.volume = savedVolume;

        volumeSlider.onValueChanged.AddListener(UpdateVolume);
    }

    void UpdateVolume(float value)
    {
        audioSource.volume = value;
        volumeSetting.volume = value;  // Save to SO

        // Persist the change in the editor (not included in builds)
#if UNITY_EDITOR
        EditorUtility.SetDirty(volumeSetting);
#endif
    }
}
