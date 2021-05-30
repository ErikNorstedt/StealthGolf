using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hole : MonoBehaviour
{
    public GameObject objectToFade;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            objectToFade.SetActive(true);
        }
       
    }
}
