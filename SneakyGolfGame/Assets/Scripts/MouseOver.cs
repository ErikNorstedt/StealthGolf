using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOver : MonoBehaviour
{
    private BallControl mainScript_;
    private void Start()
    {
        mainScript_ = FindObjectOfType<BallControl>();
    }
    private void OnMouseEnter()
    {
        mainScript_.overBall_ = true;
    }

    private void OnMouseExit()
    {
        mainScript_.overBall_ = false;
    }

}
