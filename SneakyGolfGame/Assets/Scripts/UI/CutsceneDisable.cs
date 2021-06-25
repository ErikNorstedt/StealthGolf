using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneDisable : MonoBehaviour
{
    CameraTransition camTransition_;
    private void Start()
    {
        camTransition_ = FindObjectOfType<CameraTransition>();
    }
    private void OnDisable()
    {
        if(camTransition_ != null)
            camTransition_.startCameraPan();
    }
}
