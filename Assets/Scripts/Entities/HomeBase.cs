using UnityEngine;

public class HomeBase : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public float CurrentHealth => currentHealth;
    [SerializeField] private HealthBar healthBar;

    public GameObject gameOverScreen;

    private float damage;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (maxHealth <= 0)
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
                TakeDamage(component.damage);
            }

            Destroy(other.gameObject);
        }
    }
    //For when enemies attack
    //Can check enemy tags for different damage amounts and pass the enemy type into the take damage method

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            GameOver();
        }

        healthBar.UpdateHealthBar(maxHealth, currentHealth);
        //Can add an effect that displays damage taken in here as well
    }

    private void GameOver()
    {
        gameOverScreen.SetActive(true);
        Destroy(gameObject);
    }
}
