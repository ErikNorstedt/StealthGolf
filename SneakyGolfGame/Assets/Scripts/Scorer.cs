using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    Par parScript_;
    public bool detected_ {get; set;}
    void Start()
    {
        parScript_ = FindObjectOfType<Par>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetFinalScore()
    {
        float score = 1;
        if(parScript_.CalculatePar() >= 0)
        {
            score += 1;
        }
        if(detected_ == false)
        {
            score += 1;
        }

        
        return score;
    }
}
