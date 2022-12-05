using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignWeapons : MonoBehaviour
{
    [SerializeField] List<BuyableItem> items;
    [SerializeField] List<GameObject> possibleWeapons;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in items)
        {
            item.light.color = Color.magenta;
            item.price = 399;
            item.text.SetText(item.price + " W");
            var weapon = Instantiate(possibleWeapons[Random.Range(0, possibleWeapons.Count)], item.transform);
            weapon.GetComponent<GunShoot>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
