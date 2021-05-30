using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    bool triggered = false; 
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && triggered == false)
        {
            triggered = true;
            FindObjectOfType<Level3Manager>().LowerGate();
            transform.GetChild(0).rotation = Quaternion.Euler(-225, 0, 0);
        }
           
    }
}
