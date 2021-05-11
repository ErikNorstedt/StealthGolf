using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    
    void Start()
    {
        findPlayerBall();
        strokeForce_ = 1;
        cam_ = Camera.main;
    }

    private Camera cam_;
    public float strokeAngle_ { get; protected set; }

    public float strokeForce_ { get; protected set; }
    public float StrokeForcePerc { get { return strokeForce_ / MaxStrokeForce; } }

    float MaxStrokeForce = 10f;
    public enum StrokeMode { 
        AIMING,
        DO_WHACK,
        ROLLING,
    };
    public StrokeMode StrokeMode_ { get; protected set; }

    Rigidbody playerBallRB_;
    private void findPlayerBall()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");

        if (go == null)
        {
            Debug.LogError("Couldn't find ball");
        }

        playerBallRB_ = go.GetComponent<Rigidbody>();

        if (playerBallRB_ == null)
        {
            Debug.LogError("why doesnt the ball have a rigidbody?!?!");
        }
    }
    private Vector3 getScreenPosOfClick()
    {
        Vector3 returnVec;
        returnVec = cam_.ScreenToWorldPoint(Input.mousePosition);
        return returnVec;
    }
    private void Update()
    {
        if (StrokeMode_ == StrokeMode.AIMING)
        {
            if (Input.GetMouseButtonDown(0))
            {
                getScreenPosOfClick();
                
            }
            if (Input.GetMouseButtonDown(0))
            {
                StrokeMode_ = StrokeMode.DO_WHACK;

            }
        }
    }

    void CheckRollingStatus()
    {
        if (playerBallRB_.IsSleeping())
        {
            StrokeMode_ = StrokeMode.AIMING;
        }
    }
    void FixedUpdate()
    {
        if(playerBallRB_ == null)
        {
            return;
        }

        if (StrokeMode_ == StrokeMode.ROLLING)
        {
            CheckRollingStatus();
            return;
        }

        if (StrokeMode_ != StrokeMode.DO_WHACK)
        {
            return;
        }

        Debug.Log("Whacking it!");
        Vector3 forceVec = new Vector3(0, 0, strokeForce_);

        playerBallRB_.AddForce(Quaternion.Euler(0, strokeAngle_, 0) * forceVec, ForceMode.Impulse);

        StrokeMode_ = StrokeMode.ROLLING;
    }
}
