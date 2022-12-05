using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    private RoomTemplate templates;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
        templates.rooms.Add(this.gameObject);
    }
}
