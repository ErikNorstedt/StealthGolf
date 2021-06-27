using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public GameObject[] filledStars;

    private void Start()
    {
        parScript_ = FindObjectOfType<Par>();
        foreach(var star in filledStars)
        {
            star.SetActive(false);
        }
    }
    Par parScript_;
    public bool detected_ { get; set; }

    public void DisplayFinalScore()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        filledStars[0].SetActive(true);
        if (parScript_.CalculatePar() >= 0)
        {
            filledStars[1].SetActive(true);
        }
        if (detected_ == false)
        {
            filledStars[2].SetActive(true);
        }

        
        
    }
}
