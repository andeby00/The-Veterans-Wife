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

    private void Update()
    {
        if (waitTime <= 0 && spawnedBoss == false)
        {
            Instantiate(boss, rooms[^1].transform.position + Vector3.up, Quaternion.identity);
            spawnedBoss = true;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
