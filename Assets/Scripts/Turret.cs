using UnityEngine;

public class Turret : MonoBehaviour, IUpgradeVisual
{
    [Header("Turret Stats")]
    public float range = 5f;
    public float fireRate = 1f;
    public float maxHealth = 20f;
    public float currentHealth;

    [Header("Defender Type")]
    public string defenderType = "Dryad";

    [SerializeField] private HealthBar healthBar;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private float _fireCooldown;
    private Transform _target;

    [Header("Visual Upgrade")]
    public int upgradeLevel = 0;
    private LineRenderer outline;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
        CreateOutline();
    }

    void Update()
    {
        if (_target == null || Vector3.Distance(transform.position, _target.position) > range)
        {
            FindTarget();
        }

        if (_target != null)
        {
            LockIn();
            _fireCooldown -= Time.deltaTime;
            if (_fireCooldown < 0)
            {
                Shoot();
                _fireCooldown = 1f / fireRate;
            }
        }

        if (currentHealth <= 0)  // Fixed: currentHealth, not maxHealth
        {
            Destroy(gameObject);
        }
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDist = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (var enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < closestDist && dist <= range)
            {
                closestDist = dist;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            _target = closestEnemy.transform;
        }
    }

    void LockIn()
    {
        Vector3 dir = (_target.position - transform.position).normalized;
        dir.y = 0;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * 5f);
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
            bullet.SetTarget(_target);
    }

    void TakeDamge(float damage)  // Fixed typo: Damage → Damge (keep as-is if consistent)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BasicProjectile"))
        {
            if (other.TryGetComponent<Projectile>(out Projectile component))
            {
                TakeDamge(component.damage);
            }
            Destroy(other.gameObject);
        }
    }

    // ========== VISUAL UPGRADE GLOW ==========
    void CreateOutline()
    {
        outline = gameObject.AddComponent<LineRenderer>();
        outline.material = new Material(Shader.Find("Sprites/Default"));
        outline.startWidth = 0.3f;      // ← BIGGER (was 0.15f)
        outline.endWidth = 0.3f;
        outline.useWorldSpace = true;
        outline.positionCount = 64;
        outline.sortingOrder = 100;     // ← WAY ABOVE (was 10)
        outline.sortingLayerName = "Default";  // ← Match your sprites
        UpdateOutline();
    }

    public void UpgradeVisual()
    {
        upgradeLevel++;
        UpdateOutline();
    }

    void UpdateOutline()
    {
        if (outline == null) return;

        Color[] colors = {
        new Color(0,0,0,0),           // Lv0: Invisible
        new Color(0,1,0,1),           // Lv1: Bright Green
        new Color(0,1,1,1),           // Lv2: Bright Cyan
        new Color(1,1,0,1),           // Lv3: Bright Gold
        new Color(1,0,1,1)            // Lv4: Bright Purple
    };

        Color glowColor = colors[Mathf.Clamp(upgradeLevel, 0, colors.Length - 1)];
        outline.startColor = glowColor;
        outline.endColor = glowColor;

        // Pulsing + Growing
        float pulse = 1f + Mathf.Sin(Time.time * 3f + upgradeLevel) * 0.2f;
        float radius = (0.8f + upgradeLevel * 0.2f) * pulse;

        for (int i = 0; i < 64; i++)
        {
            float angle = i / 64f * Mathf.PI * 2f;
            Vector3 pos = transform.position + new Vector3(
                Mathf.Cos(angle) * radius,
                Mathf.Sin(angle) * radius,
                -0.05f  // Behind sprite
            );
            outline.SetPosition(i, pos);
        }
    }
}