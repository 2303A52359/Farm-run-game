using UnityEngine;

public class AnimatorAssigner : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        // Get the Animator from this GameObject
        

        if (animator == null)
        {
            Debug.LogError("animator error ");
            return;
        }

        // Get all MonoBehaviour scripts on this GameObject
        MonoBehaviour[] allScripts = GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour script in allScripts)
        {
            // Try to find the animator field using reflection
            var scriptType = script.GetType();
            var field = scriptType.GetField("animator",
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance);

            if (field != null && field.FieldType == typeof(Animator))
            {
                field.SetValue(script, animator);
            }
        }
    }
}