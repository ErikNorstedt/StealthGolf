using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hole : MonoBehaviour
{
    public int NextLevel;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            switch (NextLevel)
            {
                case 1:
                    SceneManager.LoadScene("Level 1");
                    break;
                case 2:
                    SceneManager.LoadScene("Level 2");
                    break;
                case 3:
                    SceneManager.LoadScene("Level 3");
                    break;
                case 4:
                    SceneManager.LoadScene("Main Menu");
                    break;
                default:
                    Debug.Log("Wrong int");
                    break;
            }
        }
       
    }
}
