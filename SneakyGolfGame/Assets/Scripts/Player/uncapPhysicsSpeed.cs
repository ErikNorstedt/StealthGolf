using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uncapPhysicsSpeed : MonoBehaviour
{

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if(rb == null)
        {
            Debug.LogError("No RigidBody found on: " + gameObject.name);
            return;
        }

        rb.maxAngularVelocity = Mathf.Infinity;

        Destroy(this);
    }
}
