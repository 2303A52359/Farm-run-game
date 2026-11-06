using UnityEngine;
using UnityEngine.InputSystem;

public class Pause: MonoBehaviour
{
    public static bool IsPaused { get; private set; }

    public GameObject PauseMenu;

    private void Awake()
    {
        PauseGame();
    }


    public static void PauseGame()
    {
        if (IsPaused) return;

        IsPaused = true;
        Time.timeScale = 0f;

        // Pause all audio except UI sounds
        AudioListener.pause = true;

        // Optional: Disable all non-UI input
        // This depends on your input system
    }

    public static void ResumeGame()
    {
        if (!IsPaused) return;

        IsPaused = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    public static void TogglePause()
    {
        if (IsPaused)
            ResumeGame();
        else
            PauseGame();
    }


}

