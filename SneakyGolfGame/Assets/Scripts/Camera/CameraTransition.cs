using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public bool cutsceneInLevel;
    CameraFollow mainCamScript_;
    public Vector3 optionalPos_ = Vector3.zero;
    private Vector3 desiredPos_;
    private float speed_ = 10;
    void Start()
    {
        mainCamScript_ = FindObjectOfType<CameraFollow>();
        desiredPos_ = mainCamScript_.camTarget_.position;
        desiredPos_.z = mainCamScript_.camTarget_.position.z - mainCamScript_.zDif;
        desiredPos_.y = mainCamScript_.camTarget_.position.y + mainCamScript_.yDif + 5;
        if (optionalPos_ != Vector3.zero)
        {
            optionalPos_.y += 5;
        }
        if (cutsceneInLevel == false)
        {
            startCameraPan();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            speed_ *= 2;
        }
        if (Input.GetMouseButtonUp(0))
        {
            speed_ /= 2;
        }
    }
    public void startCameraPan()
    {
        
        StartCoroutine(initialCameraPan());
    }
    IEnumerator initialCameraPan()
    {
        yield return new WaitForSeconds(1.5f);
        if (optionalPos_ == Vector3.zero)
        {
            while (transform.position != desiredPos_)
            {

                transform.position = Vector3.MoveTowards(transform.position, desiredPos_, speed_ * Time.deltaTime);
                yield return null;
            }
        }
        else
        {
            while (transform.position != optionalPos_)
            {

                transform.position = Vector3.MoveTowards(transform.position, optionalPos_, speed_ * Time.deltaTime);
                yield return null;
            }
            while (transform.position != desiredPos_)
            {

                transform.position = Vector3.MoveTowards(transform.position, desiredPos_, speed_ * Time.deltaTime);
                yield return null;
            }
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
