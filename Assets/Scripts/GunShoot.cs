using TMPro;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    [SerializeField] float damage = -1f;
    //[SerializeField] float range = 10f;

    [SerializeField] GameObject bullet;
    [SerializeField] float shootForce, upwardForce;

    [SerializeField] float fireRate, spread, reloadTime, fireRateDuringBurst; // Krav for fireRateDuringBurst: fireRateDuringBurst > fireRate * bulletsPerTap
    [SerializeField] int magazineSize = 8;
    [SerializeField] int bulletsPerTap = 1;
    [SerializeField] bool automatic = false;

    int _bulletsLeft, _bulletsShot;
    
    bool _shooting, _readyToShoot, _reloading;
    bool _allowInvoke = true;
    
    [SerializeField] new Camera camera;
    [SerializeField] Transform attackPoint;
    [SerializeField] ParticleSystem flash;
    [SerializeField] TextMeshProUGUI ammoDisplay;

    // Update is called once per frame
    void Start()
    {
        _bulletsLeft = magazineSize;
        _readyToShoot = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (automatic)
        {
            _shooting = Input.GetButton("Fire1");
        }
        else
        {
            _shooting = Input.GetButtonDown("Fire1");
        }

        if (Input.GetKeyDown(KeyCode.R) && _bulletsLeft < magazineSize && !_reloading)
        {
            Reload();
        }

        // if (_readyToShoot && _shooting && !_reloading && _bulletsLeft < 0) // hvis vi vil reload med venstreklik når mag er tom
        // {
        //     Reload();
        // }

        if (_readyToShoot && _shooting && !_reloading && _bulletsLeft > 0)
        {
            Debug.Log("xd");
            _bulletsShot = 0;
            
            Shoot();

        }
        
        if (ammoDisplay != null)
            ammoDisplay.SetText(_bulletsLeft + " / " + magazineSize);
    }

    void Shoot()
    {
        flash.Play();
        _readyToShoot = false;

        RaycastHit hit;
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);

        Vector3 targetPoint;
        
        //if (Physics.Raycast(ray, out hit, 5f))
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point; // dette har ogs problemer
        }
        else
        {
            targetPoint = ray.GetPoint(75); 
        }

        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        // spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        currentBullet.transform.forward = directionWithSpread.normalized;
        
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(camera.transform.up * upwardForce, ForceMode.Impulse);
        currentBullet.AddComponent<BulletCollision>().Damage = damage;

        Destroy(currentBullet, 1f);
        
        _bulletsLeft--;
        _bulletsShot++;
        
        if (_allowInvoke)
        {
            Debug.Log( 60 / fireRate);
            Invoke("ResetShot", 60 / fireRate);
            _allowInvoke = false;
        }

        if (_bulletsShot < bulletsPerTap && _bulletsLeft > 0)
        {
            Invoke("Shoot", 60 / fireRateDuringBurst); // burst fire
        }
    }

    private void ResetShot()
    {
        _readyToShoot = true;
        _allowInvoke = true;
    }

    private void Reload()
    {
        _reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        _bulletsLeft = magazineSize;
        _reloading = false;
    }
}
