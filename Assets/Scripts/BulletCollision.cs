using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public float Damage { get; set; }
    public float ExplosionRadius { get; set; } = 3f;
    public bool Explosive { get; set; }
    [SerializeField] public LayerMask enemyLayer;

    private void OnTriggerEnter(Collider other)
    {   
        if (Explosive)
        {
            Debug.Log("explosive true");
            var xd = Physics.OverlapSphere(gameObject.transform.position, ExplosionRadius, enemyLayer);
            foreach (var enemyCollider in xd)
            {
                var enemy = enemyCollider.transform.GetComponent<EnemyAI>();

                if (enemy != null)
                {
                    enemy.TakeDamage(Damage);
                }
            }
            Destroy(gameObject);
        }
        else 
        {
            Debug.Log("explosive false");
            var enemy = other.transform.GetComponent<EnemyAI>();
            
            if (enemy != null)
            {
                enemy.TakeDamage(Damage);
                
            }
            Destroy(gameObject);
        }
    }
}
