using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public void Scene1()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Scene2()
    {
        SceneManager.LoadScene("Settings");
    }
    public void Scene3()
    {
        SceneManager.LoadScene("Space");
    }


}
