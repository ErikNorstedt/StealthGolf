using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    AudioSource source_;
    bool triggered = false;
    private void Start()
    {
        source_ = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && triggered == false)
        {
            source_.Play();
            triggered = true;
            FindObjectOfType<Level3Manager>().LowerGate();
            transform.GetChild(0).rotation = Quaternion.Euler(-225, 0, 0);
        }
           
    }
}
