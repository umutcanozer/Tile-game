using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLive = 3;
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1) Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        livesText.text = playerLive.ToString();
        scoreText.text = score.ToString();
    }
    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }
    public void ProcesssPlayerDeath()
    {
        if (playerLive >= 1) TakeLive();
        else ResetGameSession();
    }

    void TakeLive()
    {
        playerLive--;
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
        livesText.text = playerLive.ToString();
    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
