using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        GlobalInventory.Instance.Reset();
        SceneManager.LoadScene("CutScene");
    }

    public void GoToSettingsMenu()
    {
        SceneManager.LoadScene("Settings");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    
    public void GoToHowToPlay()
    {
        SceneManager.LoadScene("How to play");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
