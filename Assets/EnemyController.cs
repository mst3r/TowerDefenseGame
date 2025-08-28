using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 50f;
    public float speed = 5f;

    public GameObject playerBase;
    public Transform baseLocation;

    public string state = "NoTowers";



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerBase = GameObject.FindWithTag("HomeBase");

        if (playerBase != null)
        {
            baseLocation = playerBase.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (state == "NoTowers")
        {
            MovetoBase();
        }
        else
        {

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
            state = "Towers";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Defender"))
        {
            state = "NoTowers";
        }
    }
}
