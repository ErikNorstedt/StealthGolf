using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform camTarget_;
    public float posLerp = .02f;
    public float zDif = 5;
    public float yDif = 7;
    private Vector3 desiredPos = Vector3.zero;
    Vector3 tmpPos;
    //public float rotLerp = .01f;

    void Update()
    {
        desiredPos = camTarget_.position;
        desiredPos.z = camTarget_.position.z - zDif;
        desiredPos.y = camTarget_.position.y + yDif;
        tmpPos = Vector3.Lerp(transform.position, desiredPos, posLerp);
        transform.position = tmpPos;
        //transform.rotation = Quaternion.Lerp(transform.rotation, camTarget_.rotation, rotLerp);
    }
}
