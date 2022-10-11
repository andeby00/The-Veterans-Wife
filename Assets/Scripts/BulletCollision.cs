using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public float Damage { get; set; }

    public BulletCollision(float damage)
    {
        Damage = damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Damage == -1f)
        {
            if (other.gameObject.tag.Equals("Enemy"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}
