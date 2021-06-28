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

        SceneManager.LoadScene(i);

        
        audiomanager_.PlayMusic(1);
    }
}
