using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelButton : MonoBehaviour
{
    public GameObject objectToFade;
    public bool endPicture = false;
    private ScoreUI menu_;
    void Start()
    {
        menu_ = FindObjectOfType<ScoreUI>();


    }

    
    public void NextLevel()
    {
        if(endPicture)
        {

        }
        else
        {
            objectToFade.SetActive(true);
            menu_.gameObject.SetActive(false);
            
        }
    }
}
