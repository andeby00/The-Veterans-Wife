using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsText;

    [SerializeField] new Camera camera;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI ammoDisplay;
    [SerializeField] TextMeshProUGUI healthDisplay;

    [SerializeField] Transform gunContainer;
        
    //[SerializeField] AudioSource coindSound;
    
    [SerializeField] float health = 1000;
    [SerializeField] int coins = 0;

    private void Start()
    {
        healthDisplay.SetText(health + "");
        
        coins = GlobalInventory.Instance.coins;
        coinsText.SetText(coins + "");

        var tempGunContainer = GlobalInventory.Instance.gunContainer;

        var newGun = tempGunContainer.GetChild(0);
        gunContainer.DetachChildren();
                
        newGun.SetParent(gunContainer);
        newGun.localPosition = Vector3.zero;
        newGun.localRotation = Quaternion.Euler(Vector3.zero);
        newGun.localScale = Vector3.one;

        var gunShoot = newGun.GetComponent<GunShoot>();
        gunShoot.camera = camera;
        gunShoot.ammoDisplay = ammoDisplay;
        gunShoot.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coins++;
            coinsText.SetText(coins + "");
            //coindSound.Play();
        }
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
        
        if (Physics.Raycast(ray, out hit, 5f))
        {
            if (hit.transform.CompareTag("Door")) 
                text.SetText("Press \"E\" to Exit");
            
            if (Input.GetKeyDown(KeyCode.E) && hit.transform.CompareTag("Door")) {
                SceneManager.LoadSceneAsync("Space");
                SavePlayer();
            }
            
            var x = hit.transform.GetComponent<BuyableItem>();
            
            if (x == null) return;
            
            if (hit.transform.CompareTag("BuyArea") && coins > x.price)
            {
                text.SetText("Press \"E\" to buy");
            }

            if (Input.GetKeyDown(KeyCode.E) && coins > x.price && hit.transform.CompareTag("BuyArea"))
            {
                coins -= x.price;

                foreach (Transform child in gunContainer)
                {
                    Destroy(child.gameObject);
                }

                gunContainer.DetachChildren();

                var newGun = hit.transform.GetChild(0);
                
                newGun.SetParent(gunContainer);
                newGun.localPosition = Vector3.zero;
                newGun.localRotation = Quaternion.Euler(Vector3.zero);
                newGun.localScale = Vector3.one;

                var gunShoot = newGun.GetComponent<GunShoot>();
                gunShoot.enabled = true;

                gunShoot.camera = camera;
                gunShoot.ammoDisplay = ammoDisplay;

                Destroy(hit.transform.parent.gameObject);
                
                coinsText.SetText(coins + "");
            }
        }
        else
        {
            text.SetText("");
        }
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        healthDisplay.SetText(health + "");
    }
    
    public void SavePlayer()
    {
        GlobalInventory.Instance.coins = coins;
        gunContainer.GetChild(0).SetParent(GlobalInventory.Instance.gunContainer);
    }
}
