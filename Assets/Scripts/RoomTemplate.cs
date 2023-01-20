using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplate : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    
    public GameObject noRooms;

    public List<GameObject> rooms;

    public float waitTime;
    private bool spawnedBoss;
    public GameObject boss;
    public GameObject player;

    public GameObject test;

    private void Update()
    {
        if (waitTime <= 0 && spawnedBoss == false)
        {
            Instantiate(test, rooms[^1].transform.position + Vector3.up, Quaternion.identity);
            
            var enemy = Instantiate(boss, rooms[^1].transform.position + Vector3.up, Quaternion.identity);
            enemy.GetComponent<EnemyAI>().player = player.transform;
            spawnedBoss = true;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
