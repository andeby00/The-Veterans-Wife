using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignWeapons : MonoBehaviour
{
    [SerializeField] List<BuyableItem> items;
    [SerializeField] List<GameObject> commonWeapons;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in items)
        {
            switch (Random.Range(0,100))
            {
                case < 45:
                    item.light.color = Color.white;
                    item.price = Random.Range(0, 200);
                    item.text.SetText(item.price + " W");
                    var common = Instantiate(commonWeapons[Random.Range(0, commonWeapons.Count)], item.transform);
                    common.GetComponent<GunShoot>().enabled = false;
                    break;
                case < 70:
                    item.light.color = Color.green;
                    item.price = Random.Range(200, 400);
                    item.text.SetText(item.price + " W");
                    var uncommon = Instantiate(commonWeapons[Random.Range(0, commonWeapons.Count)], item.transform);
                    uncommon.GetComponent<GunShoot>().enabled = false;
                    break;
                case < 85:
                    item.light.color = Color.blue;
                    item.price = Random.Range(400, 600);
                    item.text.SetText(item.price + " W");
                    var rare = Instantiate(commonWeapons[Random.Range(0, commonWeapons.Count)], item.transform);
                    rare.GetComponent<GunShoot>().enabled = false;
                    break;
                case < 95:
                    item.light.color = Color.magenta;
                    item.price = Random.Range(600, 800);
                    item.text.SetText(item.price + " W");
                    var epic = Instantiate(commonWeapons[Random.Range(0, commonWeapons.Count)], item.transform);
                    epic.GetComponent<GunShoot>().enabled = false;
                    break;
                case < 100:
                    item.light.color = Color.yellow;
                    item.price = Random.Range(800, 1000);
                    item.text.SetText(item.price + " W");
                    var legendary = Instantiate(commonWeapons[Random.Range(0, commonWeapons.Count)], item.transform);
                    legendary.GetComponent<GunShoot>().enabled = false;
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
