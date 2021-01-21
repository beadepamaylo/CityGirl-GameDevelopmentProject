using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool NextLevel;
    public GameObject GameOverPanel;
    public GameObject LevelCompletedPanel;
    public static int numberOfCoins;
    public Text CoinsText;
    int sceneIndex, levelPassed;
    public static bool isGameStarted;
    public GameObject startingText;

    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        numberOfCoins = 0;
        NextLevel = false;
        isGameStarted = false;
        //To activate level2 and 3 button
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
		levelPassed = PlayerPrefs.GetInt ("LevelPassed");
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver || NextLevel)
        {
            Time.timeScale = 0;
            if(gameOver)
                GameOverPanel.SetActive(true);
            else if(NextLevel)
            {
                LevelCompletedPanel.SetActive(true);
                //To activate level2 and 3 button
                PlayerPrefs.SetInt ("LevelPassed", sceneIndex);
            }
        }
        CoinsText.text = "Coins: " + numberOfCoins;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            isGameStarted = true;
            Destroy(startingText);
        }
    }
}
