using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int progressAmount;
    public Slider progressSlider;

    public GameObject player;
    public GameObject LoadCanvas;
    public List<GameObject> levels;
    private int currentLevelIndex = 0;

    public GameObject gameOverScreen;
    public TMP_Text survivedText;
    private int survivedLevelsCount;

    public static event Action OnReset;

    public GameObject pauseMenu; // Assign the PauseMenu in the Inspector

    // Start is called before the first frame update
    void Start()
    {
        progressAmount = 0;
        progressSlider.value = 0;
        Coins.OnCoinCollect += IncreaseProgressAmount;
        HoldToLoadLevel.OnHoldComplete += LoadNextLevel;
        PlayerHealth.OnPlayedDied += GameOverScreen;
        LoadCanvas.SetActive(false);
        gameOverScreen.SetActive(false);
    }


    void GameOverScreen()
    {
        gameOverScreen.SetActive(true);
        MusicManager.PauseBackgroundMusic();
        survivedText.text = "YOU SURVIVED" + survivedLevelsCount + "LEVEL";
        if (survivedLevelsCount != 1) survivedText.text += "S";
        Time.timeScale = 0;
        //YOU SURvIVED 0 LEVELS
        //YOU SURIVVED 1 LEVELS
        //YOU SURIVVED 2 LEVELS 
    }


    public void ResetGame()
    {
        gameOverScreen.SetActive(false);
        MusicManager.PlayBackgroundMusic(true);
        survivedLevelsCount = 0;
        LoadLevel(0, false);
        OnReset.Invoke();
        Time.timeScale = 1;
    }

    void IncreaseProgressAmount(int amount)
    {
        progressAmount += amount;
        progressSlider.value = progressAmount;
        if(progressAmount >= 100)
        {
            //Level Complete!
            LoadCanvas.SetActive(true);
            Debug.Log("Level Complete`");
        }
    }

    void LoadLevel(int level, bool wantSurvivedIncrease)
    {
        LoadCanvas.SetActive(false);

        levels[currentLevelIndex].gameObject.SetActive(false);
        levels[level].gameObject.SetActive(true);

        player.transform.position = new Vector3(0, 0, 0);

        currentLevelIndex = level;
        progressAmount = 0;
        progressSlider.value = 0;
        if(wantSurvivedIncrease) survivedLevelsCount++;
    }

    void LoadNextLevel()
    {
        int nextLevelIndex = (currentLevelIndex == levels.Count - 2) ? 0 : currentLevelIndex + 1;
        LoadLevel(nextLevelIndex, true);


    }

    public void OnLevelComplete()
    {
        FindObjectOfType<EndSceneManager>().LoadEndScene();
    }


    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Stop time
        pauseMenu.SetActive(true); // Show the pause menu
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Resume time
        pauseMenu.SetActive(false); // Hide the pause menu
    }

    public void QuitGame()
    {
        // Add functionality to quit the game or return to the main menu
        // This could be implemented as needed
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}


