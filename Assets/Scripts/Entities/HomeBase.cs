using UnityEngine;

public class HomeBase : MonoBehaviour
{

    [Header("Defender Type")]
    public string defenderType = "Tree";

    public float maxHealth = 100f;
    private float currentHealth = 100.0f;

    public float CurrentHealth => currentHealth;
    [SerializeField] private HealthBar healthBar;

    public GameObject gameOverScreen;

    private float damage;

    [Header("Visual Upgrade")]
    public int upgradeLevel = 0;  // Starts 0, +1 per upgrade
    private LineRenderer outline; // Glow line

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
        CreateOutline();  // NEW
    }

    // Update is called once per frame
    void Update()
    {
        if (maxHealth <= 0)
        {
            GameObject.Destroy(gameObject);
            
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BasicProjectile"))
        {
            if (other.TryGetComponent<Projectile>(out Projectile component))
            {
                damage = component.damage;
                TakeDamage(component.damage);
            }

            Destroy(other.gameObject);
        }
    }
    //For when enemies attack
    //Can check enemy tags for different damage amounts and pass the enemy type into the take damage method

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            GameOver();
        }

        healthBar.UpdateHealthBar(maxHealth, currentHealth);
        //Can add an effect that displays damage taken in here as well
    }

    private void GameOver()
    {
        gameOverScreen.SetActive(true);
        Destroy(gameObject);
    }

    void CreateOutline()
    {
        outline = gameObject.AddComponent<LineRenderer>();
        outline.material = new Material(Shader.Find("Sprites/Default"));  // Or custom glow shader
        outline.startWidth = 0.1f;
        outline.endWidth = 0.1f;
        outline.useWorldSpace = true;
        outline.positionCount = 100;  // Smooth circle
        UpdateOutline();  // Initial
    }

    public void UpgradeVisual()  // Call from ApplyUpgrade
    {
        upgradeLevel++;
        UpdateOutline();
    }

    void UpdateOutline()
    {
        if (outline == null) return;

        // Level colors (nature theme!)
        Color[] colors = {
        Color.clear,      // Lv0: No glow
        Color.green,      // Lv1
        Color.cyan,       // Lv2: Blue-ish
        Color.yellow,     // Lv3: Gold
        new Color(1,0,1)  // Lv4: Purple
    };
        outline.startColor = colors[Mathf.Clamp(upgradeLevel, 0, colors.Length - 1)];
        outline.endColor = outline.startColor;

        // Animated pulsing glow
        float radius = 0.5f + upgradeLevel * 0.1f;  // Bigger per level
        for (int i = 0; i < 100; i++)
        {
            float angle = i / 100f * Mathf.PI * 2;
            outline.SetPosition(i, transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * radius);
        }
    }
}
