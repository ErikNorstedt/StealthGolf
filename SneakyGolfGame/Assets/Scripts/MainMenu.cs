using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainButtons;
    public GameObject levelButtons;
    private AudioManager audiomanager_;
    private void Start()
    {
        audiomanager_ = FindObjectOfType<AudioManager>();
    }
    public void levelSelect()
    {
        mainButtons.SetActive(false);
        levelButtons.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SelectLevel(int i)
    {
        switch (i)
        {
            case 1:
                SceneManager.LoadScene("Level 1");
                break;
            case 2:
                SceneManager.LoadScene("Level 2");
                break;
            case 3:
                SceneManager.LoadScene("Level 3");
                break;
        }
        audiomanager_.PlayMusic(1);
    }
}
