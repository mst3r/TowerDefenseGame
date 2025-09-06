using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPManager : MonoBehaviour
{

    [Header("XP")]
    [SerializeField] AnimationCurve xpCurve;

    int currentlevel, totalXP;
    int previousLevelsXP, nextLevelsXP;

    [Header("Interface")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI xpText;
    [SerializeField] Image xpFill;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        //for testing
        if(Input.GetMouseButtonDown(0)) 
            AddXP(5);
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

}   
