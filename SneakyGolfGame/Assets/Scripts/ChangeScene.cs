using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    void Start()
    {
        int tmp = SceneManager.GetActiveScene().buildIndex;
        if(tmp <= 6)
            SceneManager.LoadScene(tmp + 1);
        else
        {
            StartCoroutine(SwitchInSeconds(5));
        }
        Time.timeScale = 1;
    }

    IEnumerator SwitchInSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Main Menu");
    }
}
