using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInventory : MonoBehaviour
{    
    public static GlobalInventory Instance;

    public int coins = 0;
    public float health = 1000;
    [SerializeField] GameObject starterGun;
    public Transform gunContainer;

    void Awake ()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            
            starterGun.GetComponent<GunShoot>().enabled = false;
            Instantiate(starterGun, gunContainer);
        }
        else if (Instance != this)
        {
            Destroy (gameObject);
        }
    }

    public void Reset()
    {
        coins = 0;
        health = 1000;

        gunContainer.DetachChildren();
        Instantiate(starterGun, gunContainer);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
