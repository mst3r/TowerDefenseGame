using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject gameOverScreen;
    public GameObject gameWinScreen;
    public GameObject playerbase;

    public float points = 0.0f;

    private float pBaseHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1.0f;

       
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerbase == null)
        {
            playerbase = GameObject.FindGameObjectWithTag("Base");
        }

        playerbase.TryGetComponent<HomeBase>(out HomeBase healthComponent);
        pBaseHealth = healthComponent.baseHealth;

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
    }
}
