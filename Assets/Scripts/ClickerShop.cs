using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickerShop : MonoBehaviour
{
    [SerializeField] GameObject helperButtonPrefab;
    [SerializeField] Transform helperParent;
    private XPManager xpManager;

    void Start()
    {
        xpManager = FindObjectOfType<XPManager>();
        gameManager = FindObjectOfType<GameManager>(); 
        PopulateHelpers();
    }

    void PopulateHelpers()
    {
        for (int i = 0; i < xpManager.helpers.Length; i++)
        {
            int idx = i;
            var h = xpManager.helpers[i];
            GameObject btn = Instantiate(helperButtonPrefab, helperParent);
            TextMeshProUGUI[] texts = btn.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = h.name;
            Image icon = btn.GetComponentInChildren<Image>();
            if (icon && h.icon) icon.sprite = h.icon;

            Button button = btn.GetComponent<Button>();
            button.onClick.AddListener(() => TryBuyHelper(idx, btn));
            UpdateButton(idx, btn);
        }
    }

    void TryBuyHelper(int idx, GameObject btn)
    {
        if (xpManager.BuyHelper(idx))
            UpdateButton(idx, btn);
    }

    private GameManager gameManager;
    void UpdateButton(int idx, GameObject btn)
    {
        var h = xpManager.helpers[idx];
        int cost = xpManager.GetHelperCost(idx);
        TextMeshProUGUI[] texts = btn.GetComponentsInChildren<TextMeshProUGUI>();
        texts[1].text = $"Owned: {h.owned}\nCost: {xpManager.FormatXP(cost)} XP";
        btn.GetComponent<Button>().interactable = gameManager.points >= cost;
    }
}