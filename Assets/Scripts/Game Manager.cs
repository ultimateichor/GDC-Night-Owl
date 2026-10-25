using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private float timeToSurvive = 10f;

    [Header("UI Elements")]
    [SerializeField] private DeathScreen deathScreen;
    [SerializeField] private WinScreen winScreen;

    private bool isAlive;
    private bool wonGame;
    private float survivalTimer;
    private int roundedSurvivalTimer;
    public UnityEvent OnRestart;

    private void Start()
    {
        isAlive = true;
        wonGame = false;
        survivalTimer = 0f;

        if (deathScreen == null || winScreen == null)
        {
            Debug.LogError("One or both of the Game Manager UI Elements isn't assigned in the Editor.");
            return;
        }

        deathScreen.gameObject.SetActive(false);
        winScreen.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isAlive && !wonGame)
        {
            survivalTimer += 1f * Time.deltaTime;
            roundedSurvivalTimer = Mathf.RoundToInt(survivalTimer);
        }

        if (survivalTimer >= timeToSurvive && !wonGame)
        {
            WinGame();
        }
    }

    public void GameOver()
    {
        if (!isAlive) return;

        isAlive = false;
        deathScreen.gameObject.SetActive(true);
        deathScreen.EnableDeathScreen(roundedSurvivalTimer);
        Debug.Log("You Died");
    }

    public void RestartGame()
    {
        isAlive = true;
        wonGame = false;
        survivalTimer = 0f;
        deathScreen.gameObject.SetActive(false);
        winScreen.gameObject.SetActive(false);
        OnRestart.Invoke();
        Debug.Log("Restarted Game");
    }

    private void WinGame()
    {
        if (wonGame) return;

        wonGame = true;
        winScreen.gameObject.SetActive(true);
        winScreen.EnableWinScreen(roundedSurvivalTimer);
        Debug.Log("You Won after " + roundedSurvivalTimer + " seconds!");
    }
}
