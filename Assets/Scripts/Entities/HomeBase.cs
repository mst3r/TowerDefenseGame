using UnityEngine;

public class HomeBase : MonoBehaviour
{

    [SerializeField] private float baseHealth = 100f;

    private float damage;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (baseHealth <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BasicProjectile"))
        {
            if (other.TryGetComponent<Projectile>(out Projectile component))
            {
                damage = component.damage;
                TakeDamage();
            }

            Destroy(other.gameObject);
        }
    }
    //For when enemies attack
    //Can check enemy tags for different damage amounts and pass the enemy type into the take damage method

    public void TakeDamage()
    {
        baseHealth--;

        //Can add an effect that displays damage taken in here as well
    }
}
