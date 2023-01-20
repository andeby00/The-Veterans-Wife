using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tornado : MonoBehaviour
{

    private PlayerInventory playerInv;
    private PlayerMovement playerMvm;
    private Transform player;
    [SerializeField] private float speed;
    

    // Start is called before the first frame update
    void Start()
    {
        playerInv = GameObject.Find("Player").GetComponent<PlayerInventory>();
        playerMvm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z), speed * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            playerMvm.TornadoJump();
            playerInv.TakeDamage(1);
        }
    }


    //collision med player = oneshot
    //collision med enemies kunne skyde dem random steder hen
}
