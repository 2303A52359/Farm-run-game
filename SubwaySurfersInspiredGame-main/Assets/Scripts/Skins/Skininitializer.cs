using UnityEngine;
using System.Collections.Generic;

public class Skininitialization : MonoBehaviour
{
    [Header("Skin Data Asset")]
    public SkinData skinData;

    [Header("Lists of Available Objects")]
    public List<GameObject> animatorObjects = new List<GameObject>();
    public List<GameObject> secondaryObjects = new List<GameObject>();

    [Header("Settings")]
    public bool autoActivateObjects = true;

    void Start()
    {
        InitializeSkin();
    }

    void InitializeSkin()
    {
        if (skinData == null)
        {
            Debug.LogError("No SkinData assigned!");
            return;
        }

        // Validate index
        if (skinData.skinIndex < 0)
        {
            Debug.LogError($"Invalid skin index: {skinData.skinIndex}");
            return;
        }

        // Get the selected objects by index
        GameObject selectedAnimatorObject = GetObjectByIndex(animatorObjects, skinData.skinIndex, "animator");
        GameObject selectedSecondaryObject = GetObjectByIndex(secondaryObjects, skinData.skinIndex, "secondary");

        if (selectedAnimatorObject == null || selectedSecondaryObject == null)
        {
            Debug.LogError($"Could not get objects for skin index {skinData.skinIndex}");
            return;
        }

        // Activate both objects if enabled
        if (autoActivateObjects)
        {
            selectedAnimatorObject.SetActive(true);
            selectedSecondaryObject.SetActive(true);
        }

        // Get animator from first object
        Animator sourceAnimator = selectedAnimatorObject.GetComponent<Animator>();

        if (sourceAnimator == null)
        {
            Debug.LogError($"No Animator found on {selectedAnimatorObject.name}!");
            return;
        }

        // Add or get the animator assigner on this object
        AnimatorAssigner myAssigner = GetComponent<AnimatorAssigner>();
        if (myAssigner == null) myAssigner = gameObject.AddComponent<AnimatorAssigner>();

        // Assign the animator
        myAssigner.animator = sourceAnimator;

        Debug.Log($"Skin '{skinData.skinName}' (Index: {skinData.skinIndex}) initialized successfully!");
    }

    GameObject GetObjectByIndex(List<GameObject> objectList, int index, string listName)
    {
        if (objectList == null || objectList.Count == 0)
        {
            Debug.LogError($"{listName} list is empty!");
            return null;
        }

        if (index >= objectList.Count)
        {
            Debug.LogError($"Index {index} is out of range for {listName} list (size: {objectList.Count})");
            return null;
        }

        if (objectList[index] == null)
        {
            Debug.LogError($"Object at index {index} in {listName} list is null!");
            return null;
        }

        return objectList[index];
    }

    // Optional: Method to change skin at runtime
    public void ChangeSkin(int newIndex)
    {
        if (skinData != null)
        {
            skinData.skinIndex = newIndex;
            InitializeSkin();
        }
    }

    // Optional: Method to change skin using a new SkinData asset
    public void ChangeSkin(SkinData newSkinData)
    {
        skinData = newSkinData;
        InitializeSkin();
    }
}