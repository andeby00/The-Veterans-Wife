using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public float Damage { get; set; }
    public bool Explosive { get; set; }
    [SerializeField] LayerMask enemyLayer;

    private void OnTriggerEnter(Collider other)
    {   
        if (Explosive)
        {
            Debug.Log("E");
            var xd = Physics.OverlapSphere(gameObject.transform.position, 3f, enemyLayer);
            foreach (var enemyCollider in xd)
            {
                Enemy enemy = enemyCollider.transform.GetComponent<Enemy>();
            
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
            Destroy(gameObject);
        }
        else 
        {
            Debug.Log("N");
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
            Destroy(gameObject);
        }
    }
}
