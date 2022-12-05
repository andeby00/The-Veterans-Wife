using UnityEngine;

public class Rotate : MonoBehaviour
{
    //[SerializeField] float speedX = 1f;
    [SerializeField] float speedY = 1f;
    //[SerializeField] float speedZ = 1f;
    
    void Update()
    {
        transform.Rotate(new Vector3 (0, 360 * speedY * Time.deltaTime, 0));
    }
}
