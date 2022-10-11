using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetCollision : MonoBehaviour
{
    [SerializeField] private string sceneName;    
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Collision detected");
            SceneManager.LoadScene(sceneName);
        }
    }
}
