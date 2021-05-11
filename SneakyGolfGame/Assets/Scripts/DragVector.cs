using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragVector : MonoBehaviour
{
    public Vector3 startPoint_ { get; set; }
    public Vector3 endPoint_ { get; set; }
    private Camera cam_;

    public float minForce = 1.5f;
    public float maxForce = 7f;
    void Start()
    {
        cam_ = Camera.main;
    }
    public Vector3 getScreenPosOfMouse()
    {
        Vector3 returnVec;
        returnVec = cam_.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam_.transform.position.z));
        return returnVec;
    }
    public Vector3 calculateDragDirection()
    {
        Vector3 returnVec;

        returnVec = new Vector3(startPoint_.x - endPoint_.x, 0, startPoint_.z - endPoint_.z);
        returnVec.Normalize();

        return returnVec;
    }

    public float getForceOfDrag()
    {
        float returnfloat;
        returnfloat = Vector3.Distance(startPoint_, endPoint_);
        return Mathf.Clamp(returnfloat,minForce, maxForce);
    }
}
