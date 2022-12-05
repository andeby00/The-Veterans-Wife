using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(PlanetEnd), 300);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlanetEnd()
    {
        SceneManager.LoadScene("Space");
    }
}
