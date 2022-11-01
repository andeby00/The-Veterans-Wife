using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsText;
    //[SerializeField] AudioSource coindSound;
    int _coins = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            _coins++;
            coinsText.text = _coins + "$";
            //coindSound.Play();
        }
    }
}
