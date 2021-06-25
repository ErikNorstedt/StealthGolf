using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Par : MonoBehaviour
{
    public float ParOnCurrentLevel_ = 2;
    private float puttAmount_ = 0;
    public void AddPutt()
    {
        puttAmount_++;
    }

    public float CalculatePar()
    {
        float tmp = ParOnCurrentLevel_ - puttAmount_;
        return tmp;
    }
}
