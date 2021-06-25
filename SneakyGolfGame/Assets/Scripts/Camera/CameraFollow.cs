using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform camTarget_;
    private Transform currentTarget_;
    public float posLerp = .02f;
    public float zDif = 5;
    public float yDif = 7;
    private Vector3 desiredPos = Vector3.zero;
    Vector3 tmpPos;
    public float rotLerp = .01f;
    //[HideInInspector]
    public bool transition = true;

  
    private void Start()
    {
        currentTarget_ = camTarget_;
    }
    void Update()
    {  
        if (transition == false)
        {
            desiredPos = currentTarget_.position;
            desiredPos.z = currentTarget_.position.z - zDif;
            desiredPos.y = currentTarget_.position.y + yDif;
            tmpPos = Vector3.Lerp(transform.position, desiredPos, posLerp);
            transform.position = tmpPos;
        }
        //transform.rotation = Quaternion.Lerp(transform.rotation, camTarget_.rotation, rotLerp);
    }

   

}
