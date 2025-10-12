using UnityEngine;

public class PlaceDefender : MonoBehaviour
{
    public static PlaceDefender instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError("More than one PlaceDefender in scene!");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public GameObject Defender1; //prefab

    public GameObject Defender2; //prefab

    public GameObject Defender3; //prefab

    private GameObject placeDefender;

    public GameObject AbleToPlaceDefender()
    {
        return placeDefender; 
    }

    public void SetDefenderToBuild (GameObject defender)
        { placeDefender = defender; }
}
