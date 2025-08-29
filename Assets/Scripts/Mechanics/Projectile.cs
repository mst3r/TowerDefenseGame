using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 10.0f;
    public float damage = 5.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
