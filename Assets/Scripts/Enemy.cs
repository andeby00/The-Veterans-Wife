using UnityEngine;

public class Enemy : MonoBehaviour
{
    //[SerializeField] float health = 50f;

    // public void Hit()
    // {
    //     health -= amount;
    //     if (health <= 0f) { Die(); }
    // }

    public void Die()
    {
        Destroy(gameObject);
    }
}
