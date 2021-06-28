using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public bool cutsceneInLevel;
    CameraFollow mainCamScript_;
    private Vector3 desiredPos_;
    void Start()
    {
        mainCamScript_ = FindObjectOfType<CameraFollow>();
        desiredPos_ = mainCamScript_.camTarget_.position;
        desiredPos_.z = mainCamScript_.camTarget_.position.z - mainCamScript_.zDif;
        desiredPos_.y = mainCamScript_.camTarget_.position.y + mainCamScript_.yDif + 5;
        if(cutsceneInLevel == false)
        {
            startCameraPan();
        }
    }
    
    public void startCameraPan()
    {
        
        StartCoroutine(initialCameraPan());
    }
    IEnumerator initialCameraPan()
    {
        while(transform.position != desiredPos_)
        {
           
            transform.position = Vector3.MoveTowards(transform.position, desiredPos_, 10 * Time.deltaTime);
            yield return null;
        }
        
        mainCamScript_.transition = false;


    }
    public void ShowObjective(Vector3 pos)
    {
        mainCamScript_.transition = true;
        //transform.position = pos;
        //transform.rotation.eulerAngles.Set(transform.rotation.eulerAngles.x, 90, 0);
        StartCoroutine(turnCamera(pos));
    }

    IEnumerator turnCamera(Vector3 pos)
    {
        while (transform.position != pos)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, 90, 0), mainCamScript_.rotLerp);
            transform.position = Vector3.MoveTowards(transform.position, pos, 15 * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
        mainCamScript_.transition = false;
    }
}
