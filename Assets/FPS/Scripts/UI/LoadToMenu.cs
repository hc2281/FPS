using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadToMenu : MonoBehaviour
{
    private void Start()
    {
        // You can add a delay or any other logic you need before loading the menu
        // For example, waiting for a few seconds can be done using Coroutines

        LoadMenuScene();
    }

    void LoadMenuScene()
    {
        SceneManager.LoadScene("IntroMenu");  // Make sure "Menu" matches the exact name of your menu scene
    }
}
