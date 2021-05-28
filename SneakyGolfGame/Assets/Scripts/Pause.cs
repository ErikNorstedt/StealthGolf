using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pause : MonoBehaviour
{
    public static bool isPaused = false;
    //public GameObject PauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }    
        }
    }

    public void Resume()
    {
        //PauseMenuUI.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        //PauseMenuUI.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("Main Menu");
    }
}
