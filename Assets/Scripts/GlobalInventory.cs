using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInventory : MonoBehaviour
{    
    public static GlobalInventory Instance;

    public int coins = 0;
    [SerializeField] GameObject starterGun;
    public Transform gunContainer;

    void Awake ()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            
            Instantiate(starterGun, gunContainer);
        }
        else if (Instance != this)
        {
            Destroy (gameObject);
        }
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
