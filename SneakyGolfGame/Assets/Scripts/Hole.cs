using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hole : MonoBehaviour
{
    AudioManager audioscript;
    ScoreUI scoreUI_;
    private void Start()
    {
        audioscript = FindObjectOfType<AudioManager>();
        scoreUI_ = FindObjectOfType<ScoreUI>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            scoreUI_.DisplayFinalScore();
            //Time.timeScale = 0;
            /*if (delay == 0)
                objectToFade.SetActive(true);
            else if (SceneManager.GetActiveScene().name == "Level 3")
                StartCoroutine(secondFade());*/
        }
       
    }

}
