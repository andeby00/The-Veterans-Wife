using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSceneEnd : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("Menu");
    }
}
