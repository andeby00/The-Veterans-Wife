using System;
using Unity.VisualScripting;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public float Damage { get; set; }
    public float ExplosionRadius { get; set; } = 8f;
    public bool Explosive { get; set; }
    [SerializeField] public LayerMask enemyLayer;
    [SerializeField] public AudioSource explosionSound;

    private void OnTriggerEnter(Collider other)
    {
        if (Explosive)
        {
            explosionSound.enabled = true;
            explosionSound.Play();
            var xd = Physics.OverlapSphere(gameObject.transform.position, ExplosionRadius, enemyLayer);
            foreach (var enemyCollider in xd)
            {
                if(enemyCollider.gameObject.CompareTag("Enemy"))
                {
                    var enemy = enemyCollider.transform.GetComponent<EnemyAI>();

                    if (enemy != null)
                    {
                        enemy.TakeDamage(Damage);
                    }
                } 
                else if (enemyCollider.transform.CompareTag("Player"))
                {
                    var player = enemyCollider.transform.parent.parent.GetComponent<PlayerInventory>();
                    
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
                var player = other.transform.parent.parent.GetComponent<PlayerInventory>();
                if (player != null)
                {
                    player.TakeDamage(Damage);
                }
                Destroy(gameObject);
            }
        }
    }
}
