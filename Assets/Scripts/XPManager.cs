using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPManager : MonoBehaviour
{
    [Header("XP")]
    [SerializeField] AnimationCurve xpCurve;
    public int currentlevel, totalXP;
    int previousLevelsXP, nextLevelsXP;

    [Header("Cookie Clicker")]
    [SerializeField] Button bigClickButton;
    [SerializeField] TextMeshProUGUI rpsText;
    public int clickPower = 10;
    public float xpPerSecond = 0f;

    public GameManager GameManager;
    public GameObject manager;

    [Header("Interface")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI xpText;
    [SerializeField] Image xpFill;

    [System.Serializable]
    public class Helper
    {
        public string name;
        public int baseCost = 50;
        public float baseProduction = 1f;
        public int owned;
        public float costMultiplier = 1.15f;
        public Sprite icon;
    }
    [Header("Helpers")]
    [SerializeField] public Helper[] helpers;

    void Start()
    {
        manager = GameObject.FindWithTag("Manager");
        GameManager = manager.GetComponent<GameManager>();
        if (bigClickButton) bigClickButton.onClick.AddListener(BigClick);
        InvokeRepeating(nameof(UpdatePassiveXP), 1f, 1f);
        UpdateLevel();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) AddXP(5);
        totalXP += (int)(xpPerSecond * Time.deltaTime);  // FIXED CAST

        if (currentlevel == 5) { GameManager.spawnCap = 30.0f; GameManager.playerLevel = currentlevel; }
        if (currentlevel == 15) { GameManager.spawnCap = 40.0f; GameManager.playerLevel = currentlevel; }
        if (currentlevel == 30) { GameManager.spawnCap = 50.0f; GameManager.playerLevel = currentlevel; }
        if (currentlevel >= 50) { GameManager.playerLevel = currentlevel; }

        CheckForLevelUp();
        UpdateInterface();
    }

    public void BigClick()
    {
        AddXP(clickPower);  // +50 XP + 50 Points!
    }
    private GameManager gameManager;
    public bool BuyHelper(int index)
    {
        if (index < 0 || index >= helpers.Length) return false;
        Helper h = helpers[index];
        int cost = GetHelperCost(index);
        if (gameManager.points < cost) return false;  // ← POINTS!

        gameManager.points -= cost;  // ← SPEND POINTS!
        h.owned++;
        xpPerSecond += h.baseProduction;
        return true;
    }

    public int GetHelperCost(int index)
    {
        Helper h = helpers[index];
        return (int)(h.baseCost * Mathf.Pow(h.costMultiplier, h.owned));
    }

    private void UpdatePassiveXP()
    {
        if (rpsText) rpsText.text = FormatXP(xpPerSecond) + " XP/s";
    }

    public string FormatXP(float n)
    {
        if (n >= 1000) return (n / 1000).ToString("F1") + "K";
        return n.ToString("F0");
    }

    public void AddXP(int amount)
    {
        totalXP += amount;
        GameManager.AddPoints();  // ← NEW: +1 POINT per click!

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

    public int GetLevel() => currentlevel;
}