using UnityEngine;
using UnityEngine.EventSystems;

public class TilesScript : MonoBehaviour, IInteractable
{
    private Renderer rend;

    [Header("Tile Settings")]
    public bool isBuildable = true;   // e.g. grass = buildable, tree = not
    public Color hoverColor = Color.yellow;
    public Color selectedColor = Color.green;
    public Color pathColour = Color.grey;
    private Color originalColor;

    private PlaceDefender placeDefender;

    void Start()
    {
        placeDefender = PlaceDefender.instance;
    }

    void Awake()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            originalColor = rend.material.color;
        }
    }

    public void OnClick()
    {
        var defenderToPlace = placeDefender.AbleToPlaceDefender();
        if (defenderToPlace == null)
            return;

        if (isBuildable)
        {
            Debug.Log("Tile clicked — building defender here.");
            Instantiate(defenderToPlace, transform.position + Vector3.up * 0.5f, Quaternion.identity);
            rend.material.color = selectedColor;
            isBuildable = false;
        }
        else
        {
            Debug.Log("This tile cannot be built on!");
        }
    }

    public void OnDeselected()
    {
        
        
        rend.material.color = originalColor;
        
        
    }

    public void OnHoverEnter()
    {
        if (placeDefender.AbleToPlaceDefender() == null)
            return;

        if (isBuildable)
        {
            rend.material.color = hoverColor;
        }
    }

    public void OnHoverExit()
    {
        if (isBuildable)
        {
            rend.material.color = originalColor;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Path"))
        {
            Debug.Log("Collidion Detected");
            rend.material.color = pathColour;
            isBuildable = false;
        }
    }


}
