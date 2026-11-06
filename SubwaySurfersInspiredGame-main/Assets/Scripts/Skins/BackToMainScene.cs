using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneName = "MainGameScene";
    private Button buttonComponent;
    private void Start()
    {
        buttonComponent = GetComponent<Button>();
        if (buttonComponent != null)
        {
            buttonComponent.onClick.AddListener(OnButtonClick);
        }
    }
    public void OnButtonClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}