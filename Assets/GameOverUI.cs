using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public TMP_Text scoreText;
    public GameObject player;
    public GameObject gameOverPanel;

    private float startX;

    void Start()
    {
        gameOverPanel.SetActive(true);
        if (player != null)
            startX = player.transform.position.x;
    }

    public void ShowGameOver()
    {
        float distance = 0f;

        if (player != null)
        {
            float currentX = player.transform.position.x;
            distance = Mathf.Max(0, currentX - startX);
        }

        scoreText.text = "Wynik: " + distance.ToString("F2") + " m";
        gameOverPanel.SetActive(true);

        // Zatrzymaj grê
        Time.timeScale = 0f;

    }

    public void RestartGame()
    {
        // Przywróæ czas gry
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Debug.Log("Wyjœcie z gry.");
        Application.Quit();
    }
}
