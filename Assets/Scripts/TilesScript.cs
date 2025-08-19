using UnityEngine;

public class TilesScript : MonoBehaviour, IInteractable
{
    private Renderer rend;

    [Header("Tile Settings")]
    public bool isBuildable = true;   // e.g. grass = buildable, tree = not
    public Color hoverColor = Color.yellow;
    public Color selectedColor = Color.green;
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
        rend.material.color = originalColor;
    }
}
