using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryRoomDestroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
