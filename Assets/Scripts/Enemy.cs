using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100f;

    public void Hit(float amount)
    {
        health -= amount;
        if (health <= 0f) { Die(); }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
