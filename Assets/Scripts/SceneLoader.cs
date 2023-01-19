using UnityEngine;
public class SceneLoader : MonoBehaviour
{
    public void Back()
    {
        // reload the current scene
        SceneManager.LoadPreviousScene();
    }
}