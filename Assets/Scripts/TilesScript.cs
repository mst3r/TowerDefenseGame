using UnityEngine;

public class TilesScript : MonoBehaviour, IInteractable
{
    private Renderer rend;

    [Header("Tile Settings")]
    public bool isBuildable = true;   // e.g. grass = buildable, tree = not
    public Color hoverColor = Color.yellow;
    public Color selectedColor = Color.green;
    public Color pathColour = Color.grey;
    private Color originalColor;

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
        if (isBuildable)
        {
            Debug.Log("Tile clicked — ready to build here.");
            rend.material.color = selectedColor;
            // later: open tower build UI
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
