using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    public float health;
    private float currentHealth;
    public float speed = 5f;
    public float fireRate = 2f;
    private float nextTimeToFire;

    public GameObject playerBase;
    public GameObject projectile;
    public GameObject gamerManager;

    public Transform baseLocation;
    public Transform ProjectileSpawn;
    public Transform defenderLocation;

    public GameManager manager;

    public bool Towerstate = false;
    public bool isFiring = false;
    public bool isAtBase = false;

    [SerializeField] private HealthBar healthBar;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerBase = GameObject.FindWithTag("HomeBase");
        gamerManager = GameObject.FindWithTag("Manager");

        manager = gamerManager.GetComponent<GameManager>();

        if (playerBase != null)
        {
            baseLocation = playerBase.transform;
        }

        currentHealth = health;

        healthBar.UpdateHealthBar(health, currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Towerstate == false && isAtBase == false)
        {
            gameObject.transform.LookAt(baseLocation); //When moving towards the base it looks at it
            MovetoBase();
        }
        else if (Towerstate == true && isAtBase == false)
        {
            MovetoBase();
            gameObject.transform.LookAt(defenderLocation); //When attacking the tower it looks at it 

            if (Time.time >= nextTimeToFire)
            {
                AttackDefenders();
                nextTimeToFire = Time.time + fireRate;
            }

            
        }
        else if (isAtBase == true)
        {
            gameObject.transform.LookAt(baseLocation);

            if (Time.time >= nextTimeToFire)
            {
                
                AttackDefenders();
                nextTimeToFire = Time.time + fireRate;
            }
            
        }

       
    }

   
    public void MovetoBase()
    {
        transform.position = Vector3.MoveTowards(transform.position, baseLocation.position, speed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Defender"))
        {
            //Debug.Log("TurretFound");

            defenderLocation = other.GetComponent<Transform>(); //Gets the location of the tower currently being fought with 
            
            Towerstate = true;
        }

        if (other.CompareTag("Stop"))
        {
            //Debug.Log("PlayerBase Found");
            isAtBase = true;
            gameObject.transform.LookAt(baseLocation);
            AttackDefenders();
        }

        

    }

    public void AttackDefenders()
    {
        Instantiate(projectile, ProjectileSpawn.position, ProjectileSpawn.rotation);
        Towerstate = false;
        
    }


    public void TakeDamage(float damage)
    {
        //health -= damage;
        currentHealth -= damage;

        //Debug.Log("Damage");
        //Debug.Log(damage.ToString());
        //Debug.Log("Health");
        //Debug.Log(currentHealth.ToString());

        if (currentHealth <= 0)
        {
            manager.AddPoints();
            Debug.Log(currentHealth.ToString());
            Die();
        }

        healthBar.UpdateHealthBar(health, currentHealth);
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }
    /*private void OnTriggerStay(Collider other)
    {
        if (other != null)
        {
            if (other.CompareTag("Defender"))
            {
                Debug.Log("TurretFound");
                Towerstate = true;
            }
        }
        else
        {
            Towerstate = false;
        }
    }
    */

    
}
