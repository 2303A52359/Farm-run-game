using UnityEngine;

[CreateAssetMenu(fileName = "NewSkinData", menuName = "Skin Data")]
public class SkinData : ScriptableObject
{
    [Header("Skin Selection")]
    public int skinIndex = 0;

    [Header("Skin Information")]
    public string skinName = "Default Skin";
    public bool unlocked = true; // Optional: skin unlock status
}