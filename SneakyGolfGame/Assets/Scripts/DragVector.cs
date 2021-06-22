using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragVector : MonoBehaviour
{
    public Vector3 startPoint_ { get; set; }
    public Vector3 endPoint_ { get; set; }
    private Camera cam_;
    [SerializeField]
    private LayerMask rayLayer_;

    public float minForce = 1.5f;
    public float maxForce = 7;
    void Start()
    {
        cam_ = Camera.main;
    }
    public Vector3 getScreenPosOfMouse()
    {
        Vector3 returnVec = Vector3.zero;
        var ray = cam_.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, rayLayer_))
        {
            returnVec = hit.point;
        }
        return returnVec;
    }
    public Vector3 calculateDragDirection()
    {
        Vector3 returnVec;

        returnVec = new Vector3(startPoint_.x - endPoint_.x, 0, startPoint_.z - endPoint_.z);
        returnVec.Normalize();

        return returnVec;
    }

    public Vector3 secondArrowPos()
    {
        Vector3 returnVec;

        returnVec = new Vector3(startPoint_.x - endPoint_.x, 0, startPoint_.z - endPoint_.z);
        returnVec.Normalize();

        returnVec *= Vector3.Distance(startPoint_, endPoint_);
        return returnVec;
    }

    public float getForceOfDrag()
    {
        float returnfloat;
        returnfloat = Vector3.Distance(startPoint_, endPoint_);
        if(returnfloat > maxForce)
        {
            returnfloat = maxForce;
        }
        if (returnfloat < minForce)
        {
            returnfloat = minForce;
        }
        
        return returnfloat * 2;
    }
}
