using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Manager : MonoBehaviour
{
    public Transform Gate;
    public Transform Ring;
    CameraTransition tmp;
    Vector3 secondPos_ = new Vector3(28.25f, 2, 1.5f);
    void Start()
    {
        tmp = FindObjectOfType<CameraTransition>();
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LowerGate()
    {
        tmp.ShowObjective(new Vector3(12,16,-27.33f));
        StartCoroutine(gateSinking());
    }

    IEnumerator gateSinking()
    {
        while (Gate.transform.position.y >= -3.5f)
        {
            Gate.transform.position += new Vector3(0, -1f, 0) * Time.deltaTime;
            yield return new WaitForSeconds(0.005f); 
        }
        Ring.position = secondPos_;
    }
}
