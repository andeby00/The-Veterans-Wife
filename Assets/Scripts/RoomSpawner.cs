using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomSpawner : MonoBehaviour
{

    [SerializeField] private int openingDirection;
    // 1: bottomdoor
    // 2: topdoor
    // 3: leftdoor
    // 4: rightdoor

    private RoomTemplate templates;
    private int rand;
    private bool spawned = false;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
        Invoke(nameof(Spawn), 0.1f);
    }

    void Spawn()
    {
        
        if (spawned == false)
        {
            
            if (openingDirection == 1)
            {
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position,
                    templates.topRooms[rand].transform.rotation);
            } else if (openingDirection == 2)
            {
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position,
                    templates.bottomRooms[rand].transform.rotation);
            }else if (openingDirection == 3)
            {
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position,
                    templates.rightRooms[rand].transform.rotation);
            }else if (openingDirection == 4)
            {
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position,
                    templates.leftRooms[rand].transform.rotation);
            }

            spawned = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                Destroy(gameObject);
            }

            spawned = true;
        }
    }
}
