using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawn : EnemyAI
{
    public GameObject slimePrefab;
    // Start is called before the first frame update
    GameObject enemy;

    // Update is called once per frame
    void Update()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    public override void DestroyEnemy()
    {
        Destroy(enemy);
        Instantiate(slimePrefab, transform.position, transform.rotation);
        Instantiate(slimePrefab, transform.position, transform.rotation);         
    }
}
