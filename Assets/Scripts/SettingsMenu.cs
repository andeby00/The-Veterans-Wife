using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public void Start()
    {
        { Time.timeScale = 1; }
    }

    public void GoToSettingsMenu()
    {
        SceneManager.LoadScene("Settings");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GoToSoundMenu()
    {
        SceneManager.LoadScene("Sound");
    }
}
