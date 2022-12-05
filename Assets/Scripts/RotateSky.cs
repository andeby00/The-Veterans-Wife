using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSky : MonoBehaviour
{

    [SerializeField] private float rotateSpeed = 1.2f;

    private static readonly int Rotation = Shader.PropertyToID("_Rotation");

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat(Rotation, Time.time * rotateSpeed);
    }
}
