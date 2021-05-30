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
    bool transition = false;

  
    private void Start()
    {
        currentTarget_ = camTarget_;
    }
    void Update()
    {  
        if (!transition)
        {
            desiredPos = currentTarget_.position;
            desiredPos.z = currentTarget_.position.z - zDif;
            desiredPos.y = currentTarget_.position.y + yDif;
            tmpPos = Vector3.Lerp(transform.position, desiredPos, posLerp);
            transform.position = tmpPos;
        }
        //transform.rotation = Quaternion.Lerp(transform.rotation, camTarget_.rotation, rotLerp);
    }

    public void ShowObjective(Vector3 pos)
    {
        transition = true;
        //transform.position = pos;
        //transform.rotation.eulerAngles.Set(transform.rotation.eulerAngles.x, 90, 0);
        StartCoroutine(turnCamera(pos));
    }

    IEnumerator turnCamera(Vector3 pos)
    {
        while (transform.position != pos)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, 90, 0), rotLerp);
            transform.position = Vector3.MoveTowards(transform.position, pos, 15 * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        transform .rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
        transition = false;
    }

}
