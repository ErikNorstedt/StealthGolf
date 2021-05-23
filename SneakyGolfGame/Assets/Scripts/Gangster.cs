using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gangster : MonoBehaviour
{
    public enum GangsterState
    {
        ROAMING,
        LOOKOUT
    }
    public GangsterState state_;
    public float speed = 5;
    public float turnSpeed = 3;
    public float waitTime = 0.3f;
    public Transform pathHolder_;
    Vector3[] waypoints;
    int targetWaypointIndex = 1;
    Vector3 targetWaypoint;

    private Animator gangsterAnim_;
    public Light spotlight;
    float viewAngle;
    bool pathing = false;
    bool putting = false;

    public float viewDistance;
    public LayerMask viewMask;
    private Transform player_;
    BallControl strokeScript_;
    Vector3 puttDir_;

    private void Start()
    {
        strokeScript_ = FindObjectOfType<BallControl>();
        player_ = GameObject.FindGameObjectWithTag("Player").transform;
        viewAngle = spotlight.spotAngle;
        gangsterAnim_ = GetComponent<Animator>();
        if (gangsterAnim_ == null)
            Debug.LogError("no animator on: " + gameObject.name);
        
        if(state_ == GangsterState.ROAMING)
        {
            waypoints = new Vector3[pathHolder_.childCount];
            for (int i = 0; i < waypoints.Length; i++)
            {
                waypoints[i] = pathHolder_.GetChild(i).position;
                waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
            }

            targetWaypoint = waypoints[targetWaypointIndex];
            StartCoroutine(FollowPath(waypoints));
        }
        else
        {

        }
        
    }
    private void Update()
    {
        if(canSeePlayer() && !putting)
        {
            StopAllCoroutines();
            pathing = false;
            gangsterAnim_.SetInteger("State", 1);
            transform.position = Vector3.MoveTowards(transform.position, player_.position, speed * Time.deltaTime);
           
            transform.LookAt(player_.position);
            if (Vector3.Distance(transform.position, player_.position) <= 1)
            {
                puttDir_ = (player_.position - transform.position).normalized;
                StartCoroutine(Putt());
            }
        }
        else 
        {
            if (state_ == GangsterState.ROAMING)
            {
                if (!pathing && !putting)
                {
                    StartCoroutine(FollowPath(waypoints));
                }
            }
            else
            {

            }
            
        }
    }
    bool canSeePlayer()
    {
        if(Vector3.Distance(transform.position, player_.position) < viewDistance)
        {
            Vector3 dirToPlayer = (player_.position - transform.position).normalized;
            float angleBetweenGangsterAndBall = Vector3.Angle(transform.forward, dirToPlayer);
            if(angleBetweenGangsterAndBall < viewAngle / 2f){
                if (!Physics.Linecast(transform.position, player_.position, viewMask)){
                    return true;
                }
            }
        }
        return false;
    }

    IEnumerator Putt()
    {
        putting = true;
        gangsterAnim_.SetInteger("State", 2);

        yield return new WaitForSeconds(0.8f);
        if (Vector3.Distance(transform.position, player_.position) <= 1)
        {
            strokeScript_.Putt(puttDir_ * 12);
        }
        putting = false;
    }

    IEnumerator FollowPath(Vector3[] waypoints)
    {
        //transform.position = waypoints[0];
        pathing = true;
        
        transform.LookAt(targetWaypoint);
        gangsterAnim_.SetInteger("State", 1);

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
            if(transform.position == targetWaypoint)
            {
                gangsterAnim_.SetInteger("State", 0);
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
                yield return new WaitForSeconds(waitTime);
                yield return StartCoroutine(TurnToFace(targetWaypoint));
                gangsterAnim_.SetInteger("State", 1);
            }
            yield return null;
        }
    }
    IEnumerator TurnToFace(Vector3 lookTarget)
    {
        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }
    private void OnDrawGizmos()
    {
        Vector3 startPos_ = pathHolder_.GetChild(0).position;
        Vector3 previousPos_ = startPos_;
        foreach(Transform waypoint in pathHolder_)
        {
            Gizmos.DrawSphere(waypoint.position, 0.3f);
            Gizmos.DrawLine(previousPos_, waypoint.position);
            previousPos_ = waypoint.position;
        }
        Gizmos.DrawLine(previousPos_, startPos_);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }
}
