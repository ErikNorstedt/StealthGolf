using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gangster : MonoBehaviour
{
    public float speed = 5;
    public float turnSpeed = 90;
    public float waitTime = 0.3f;
    public float puttPower = 15;
    public Transform pathHolder_;
    Vector3[] waypoints;
    int targetWaypointIndex = 1;
    Vector3 targetWaypoint;

    private Animator gangsterAnim_;
    public Transform flashlight;
    private SpriteRenderer flashlightRenderer_;
    float viewAngle = 82;
    bool pathing = false;
    bool putting = false;

    public float viewDistance;
    public LayerMask viewMask;
    private Transform player_;
    BallControl strokeScript_;
    BumpsAndSounds smackSound_;
    Vector3 puttDir_;

    private void Start()
    {
        flashlightRenderer_ = flashlight.GetComponent<SpriteRenderer>();
        smackSound_ = FindObjectOfType<BumpsAndSounds>();
        flashlightRenderer_.color = new Color(1, 0.92f, 0.016f, 0.39f);
        strokeScript_ = FindObjectOfType<BallControl>();
        player_ = GameObject.FindGameObjectWithTag("Player").transform;
        gangsterAnim_ = GetComponent<Animator>();
        if (gangsterAnim_ == null)
            Debug.LogError("no animator on: " + gameObject.name);
        

        waypoints = new Vector3[pathHolder_.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder_.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }

        targetWaypoint = waypoints[targetWaypointIndex];
        transform.LookAt(targetWaypoint);
        StartCoroutine(FollowPath(waypoints));
        
    }
    private void Update()
    {
        if(canSeePlayer() && !putting)
        {
            flashlightRenderer_.color = new Color(1, 0, 0, 0.39f);
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
            if(Vector3.Distance(transform.position, player_.position) >= viewDistance)
            {
                StartCoroutine(backTrack());
            }
        }
        else 
        {
            if (!pathing && !putting)
            {
                flashlightRenderer_.color = new Color(1, 0.92f, 0.016f, 0.39f);
                StartCoroutine(FollowPath(waypoints));
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
        flashlightRenderer_.enabled = false;
        putting = true;
        gangsterAnim_.SetInteger("State", 2);

        yield return new WaitForSeconds(0.8f);
        if (Vector3.Distance(transform.position, player_.position) <= 3)
        {

            strokeScript_.Putt(puttDir_ * puttPower);
            smackSound_.spawnPrefab(1, player_.position);
            smackSound_.ShotSound();
        }
        StartCoroutine(backTrack());
    }

    IEnumerator FollowPath(Vector3[] waypoints)
    {
        //transform.position = waypoints[0];
        pathing = true;
        
        //transform.LookAt(targetWaypoint);
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

    IEnumerator backTrack()
    {
        targetWaypointIndex = (targetWaypointIndex - 1);
        if (targetWaypointIndex == -1)
            targetWaypointIndex = waypoints.Length - 1;
        targetWaypoint = waypoints[targetWaypointIndex];
        yield return StartCoroutine(TurnToFace(targetWaypoint));
        gangsterAnim_.SetInteger("State", 1);
        
        while (transform.position != targetWaypoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
            yield return null;
        }
        gangsterAnim_.SetInteger("State", 0);
        targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
        targetWaypoint = waypoints[targetWaypointIndex];
        flashlightRenderer_.color = new Color(1, 0.92f, 0.016f, 0.39f);
        flashlightRenderer_.enabled = true;
        yield return StartCoroutine(TurnToFace(targetWaypoint));

        StartCoroutine(FollowPath(waypoints));
        putting = false;
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
