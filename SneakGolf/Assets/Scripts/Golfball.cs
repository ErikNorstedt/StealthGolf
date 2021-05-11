using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golfball : MonoBehaviour
{
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.sleepThreshold = 0.5f; // Default is 0.005

    }

    Rigidbody rb;
}
