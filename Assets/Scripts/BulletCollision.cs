using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public float Damage { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.transform.GetComponent<Enemy>();
        
        if (enemy != null)
        {
            if (Damage == -1f)
            {
                enemy.Die();
            }
            else
            {
                enemy.Hit(Damage);
            }
        }
    }
}
