using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public GameObject[] filledStars;
    private Scorer scorer_;

    private void Start()
    {
        scorer_ = FindObjectOfType<Scorer>();
        foreach(var star in filledStars)
        {
            star.SetActive(false);
        }
    }

    public void DisplayFinalScore()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        switch(scorer_.GetFinalScore())
        {
            case 1:
                filledStars[0].SetActive(true);
                break;
            case 2:
                filledStars[0].SetActive(true);
                filledStars[1].SetActive(true);
                break;
            case 3:
                filledStars[0].SetActive(true);
                filledStars[1].SetActive(true);
                filledStars[2].SetActive(true);
                break;
            default:
                Debug.Log("weird score");
                break;
        }
    }
}
