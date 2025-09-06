using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject gameOverScreen;
    public GameObject gameWinScreen;
    public GameObject playerbase;

    public float points = 0.0f;

    private float pBaseHealth;

    [SerializeField] private TextMeshProUGUI pointsText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1.0f;

        UpdatePointsUI();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerbase == null)
        {
            playerbase = GameObject.FindGameObjectWithTag("Base");
        }

        playerbase.TryGetComponent<HomeBase>(out HomeBase healthComponent);
        pBaseHealth = healthComponent.CurrentHealth;

        if (pBaseHealth <= 0)
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0.0f;
        }

        if (points >= 20.0f)
        {
            gameWinScreen.SetActive(true);
            Time.timeScale = 0.0f;
        }
        
           
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddPoints()
    {
        points++;
        UpdatePointsUI();
    }
    private void UpdatePointsUI()
    {
        if (pointsText != null)
        {
            pointsText.text = "Points: " + points.ToString();
        }
    }
}
