using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    void Start()
    {
        string tmp = SceneManager.GetActiveScene().name;
        if(tmp == "Main Menu")
        {
            SceneManager.LoadScene("Level 1");
        }
        else if(tmp == "Level 1")
        {
            SceneManager.LoadScene("Level 2");
        }
        else if (tmp == "Level 2")
        {
            SceneManager.LoadScene("Level 3");
        }
        else if (tmp == "Level 3")
        {

        }
    }
}
