using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hole : MonoBehaviour
{
    public GameObject objectToFade;
    public float delay = 0;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(delay == 0)
                objectToFade.SetActive(true);
            if (SceneManager.GetActiveScene().name == "Level 3")
                StartCoroutine(secondFade());
        }
       
    }

    IEnumerator secondFade()
    {
        yield return new WaitForSeconds(delay);
        objectToFade.SetActive(true);

    }
}
