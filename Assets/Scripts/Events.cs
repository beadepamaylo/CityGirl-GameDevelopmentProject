using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Events : MonoBehaviour
{
    public Button Level2Button, Level3Button;
    int levelPassed;
    void Start()
    {
        levelPassed = PlayerPrefs.GetInt("LevelPassed");
        Level2Button.interactable = false;
        Level3Button.interactable = false;

        switch(levelPassed)
        {
            case 1:
                Level2Button.interactable = true;
                break;
            case 2:
                Level2Button.interactable = true;
                Level3Button.interactable = true;
                break;
            case 3:
                Level3Button.interactable = true;
                break;
        }
    }
    //For Level Scene
    public void levelToLoad(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void resetPlayerPrefs()
    {
        Level2Button.interactable = false;
        Level3Button.interactable = false;
        PlayerPrefs.DeleteAll();
    }
    
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
