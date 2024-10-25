using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Enter", LoadSceneMode.Single);
    }

    public void LoadSettingsScene()
    {
        SceneManager.LoadScene("SettingsScene", LoadSceneMode.Single);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }
}