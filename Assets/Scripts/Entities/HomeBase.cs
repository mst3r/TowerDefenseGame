using UnityEngine;

public class HomeBase : MonoBehaviour
{

    [SerializeField] private float baseHealth = 100f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(""))
        {
            TakeDamage();
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
