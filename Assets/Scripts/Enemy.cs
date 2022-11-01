
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] GameObject coin;

    public void Hit(float amount)
    {
        health -= amount;
        if (health <= 0f) { Die(); }
    }

    public void Die()
    {
        Destroy(gameObject);
        
        for (int i = 0; i < new System.Random().Next(3,6); i++)
        {
            GameObject currentCoin = Instantiate(coin, transform.position,Quaternion.Euler( Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360) ) );
            var currentRB = currentCoin.GetComponent<Rigidbody>();
            currentRB.AddForce((Random.Range(0, 20000) - 10000f) / 100f, Random.Range(0, 20000) / 100f, (Random.Range(0, 20000)- 10000f) / 100f);
        }
    }
}
