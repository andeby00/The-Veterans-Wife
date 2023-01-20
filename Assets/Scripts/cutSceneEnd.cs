using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutSceneEnd : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        GlobalInventory.Instance.Reset();
        SceneManager.LoadScene("Space");
    }
}
