using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPManager : MonoBehaviour
{

    [Header("XP")]
    [SerializeField] AnimationCurve xpCurve;

    int currentlevel, totalXP;
    int previousLevelsXP, nextLevelsXP;

    public GameManager GameManager;

    public GameObject manager;

    [Header("Interface")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI xpText;
    [SerializeField] Image xpFill;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = GameObject.FindWithTag("Manager");
        GameManager = manager.GetComponent<GameManager>();

        UpdateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        //for testing
        if(Input.GetMouseButtonDown(0)) 
            AddXP(5);

        if (currentlevel == 5)
        {
            GameManager.spawnCap = 30.0f;
            GameManager.playerLevel = currentlevel;
        }

        if (currentlevel == 15)
        {
            GameManager.spawnCap = 40.0f;
            GameManager.playerLevel = currentlevel;
        }

        if (currentlevel == 30)
        {
            GameManager.spawnCap = 50.0f;
            GameManager.playerLevel = currentlevel;
        }

        if (currentlevel >= 50)
        {
            GameManager.playerLevel = currentlevel;
        }
    }

    public void AddXP(int amount)
    {
        totalXP += amount;
        CheckForLevelUp();
        UpdateInterface();
    }

    void CheckForLevelUp()
    {
        if (totalXP >= nextLevelsXP)
        {
            currentlevel++;
            UpdateLevel();
        }
    }

    void UpdateLevel()
    {
        previousLevelsXP = (int)xpCurve.Evaluate(currentlevel);
        nextLevelsXP = (int)xpCurve.Evaluate(currentlevel + 1);

        GameManager.ResetSpawnCap();

        UpdateInterface();
    }

    void UpdateInterface()
    {
        int start = totalXP - previousLevelsXP;
        int end = nextLevelsXP - previousLevelsXP;

        levelText.text = currentlevel.ToString();
        xpText.text = start + " xp /" + end + " xp";
        xpFill.fillAmount = (float)start / (float)end;
    }
    public int GetLevel()
    {
        return currentlevel;
    }
}   
