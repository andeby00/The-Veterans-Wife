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
            var xd = Physics.OverlapSphere(gameObject.transform.position, ExplosionRadius, enemyLayer);
            foreach (var enemyCollider in xd)
            {
                Debug.Log("EEEE" + enemyCollider.gameObject.tag + " " + Damage);
                if(enemyCollider.gameObject.CompareTag("Enemy"))
                {
                    var enemy = enemyCollider.transform.GetComponent<EnemyAI>();

                    if (enemy != null)
                    {
                        enemy.TakeDamage(Damage);
                    }
                } 
                else if (enemyCollider.gameObject.CompareTag("Player"))
                {
                    var player = enemyCollider.transform.GetComponent<PlayerInventory>();
                    
                    if (player != null)
                    {
                        player.TakeDamage(Damage);
                    }
                }
            }
            Destroy(gameObject);
        }
        else 
        {
            if(other.gameObject.CompareTag("Enemy"))
            {
                var enemy = other.transform.GetComponent<EnemyAI>();

                if (enemy != null)
                {
                    enemy.TakeDamage(Damage);
                }

                Destroy(gameObject);
            }
            else if (other.gameObject.CompareTag("Player"))
            {
                var player = other.transform.parent.GetComponent<PlayerInventory>();
                    
                if (player != null)
                {
                    player.TakeDamage(Damage);
                }
            }
        }
    }
}
