using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static bool isGamePaused = false;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject mainCanvas;

    void Start()
    {
        mainMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (isGamePaused)
            {
                Debug.Log("prlay");
                ResumeGame();
            }
            else
            {
                Debug.Log("Puase");
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        isGamePaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        mainCanvas.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        mainCanvas.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;    
        isGamePaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
