using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneSkip : MonoBehaviour
{
    public static bool cutsceneActive_ = true;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            transform.parent.gameObject.SetActive(false);
            cutsceneActive_ = false;
        }
    }
}
