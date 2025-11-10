using UnityEngine;

public class SpawnPaths : MonoBehaviour
{

    public float speed = 5f;

    public GameObject playerBase;
    public Transform baseLocation;

    public Renderer ren;

    public Color pathColour = Color.grey;

    public Material path;

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
        transform.position = Vector3.MoveTowards(transform.position, baseLocation.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tile"))
        {
            ren = other.GetComponent<Renderer>();

            ren.material.color = pathColour;
        }
    }
}
