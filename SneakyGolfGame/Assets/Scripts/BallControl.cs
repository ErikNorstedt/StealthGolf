using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{

    void Start()
    {
        DragVector_ = FindObjectOfType<DragVector>();
        CircleDrawer_ = FindObjectOfType<CircleDrawer>();
        ArrowDrawer_ = FindObjectOfType<ArrowDrawer>();
        findPlayerBall();
        CircleDrawer_.SetCircle(2, 0.2f);
        CircleDrawer_.SetFollowTarget(playerBallRB_.transform, Vector3.zero);
    }
    public float strokeAngle_ { get; protected set; }
    public bool overBall_ { get; set; }
    private bool charging_ = false;
    private float slowTimer_ = 1;

    private DragVector DragVector_;
    private CircleDrawer CircleDrawer_;
    private ArrowDrawer ArrowDrawer_;
    public enum StrokeMode
    {
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

    private bool insideRange()
    {

        return true;
    }
    
    private void Update()
    {
        if (StrokeMode_ == StrokeMode.AIMING)
        {
            
            if (Input.GetMouseButtonDown(0) && overBall_ == true)  //add check for range
            {
                //Cursor.visible = false;
                CircleDrawer_.SetCircle(3, 0);
                DragVector_.startPoint_ = playerBallRB_.transform.position;
                charging_ = true;
            }
            if(Input.GetMouseButton(0) && charging_ == true)
            {
                ArrowDrawer_.RenderLine(playerBallRB_.transform.position, DragVector_.getScreenPosOfMouse(), playerBallRB_.transform.position.y);
            }
            if (Input.GetMouseButtonUp(0) && charging_ == true)
            {
                ArrowDrawer_.EndLine();
                DragVector_.endPoint_ = DragVector_.getScreenPosOfMouse();
                
                StrokeMode_ = StrokeMode.DO_WHACK;
                CircleDrawer_.TurnOff();
                Cursor.visible = true;
                charging_ = false;
            }
        }
    }

    void CheckRollingStatus()
    {
        if (playerBallRB_.IsSleeping())
        {
            StrokeMode_ = StrokeMode.AIMING;
            CircleDrawer_.SetCircle(2, 0.2f);
            playerBallRB_.drag = 0.5f;
            playerBallRB_.angularDrag = 0.05f;
        }
    }
    void FixedUpdate()
    {
        if (playerBallRB_ == null)
        {
            return;
        }

        if (StrokeMode_ == StrokeMode.ROLLING)
        {
            cutoff();
            CheckRollingStatus();
            return;
        }

        if (StrokeMode_ != StrokeMode.DO_WHACK)
        {
            return;
        }

        playerBallRB_.AddForce(DragVector_.calculateDragDirection() * DragVector_.getForceOfDrag(), ForceMode.Impulse);

        StrokeMode_ = StrokeMode.ROLLING;
    }

    float currentSlowTimer = 0;
    private void cutoff()
    {
        currentSlowTimer += Time.deltaTime;
        if(playerBallRB_.velocity.magnitude < 1.5f && currentSlowTimer >= slowTimer_)
        {
            playerBallRB_.drag = 20;
            playerBallRB_.angularDrag = 20;
            currentSlowTimer = 0;
        }
    }

    public void Putt(Vector3 dir)
    {
        playerBallRB_.AddForce(dir, ForceMode.Impulse);
        ArrowDrawer_.EndLine();
        DragVector_.endPoint_ = DragVector_.getScreenPosOfMouse();

        StrokeMode_ = StrokeMode.DO_WHACK;
        CircleDrawer_.TurnOff();
        Cursor.visible = true;
        charging_ = false;

        StrokeMode_ = StrokeMode.ROLLING;
    }
}