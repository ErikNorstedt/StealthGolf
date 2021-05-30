using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookout : MonoBehaviour
{
    public float speed = 5;
    public float turnSpeed = 90;
    public float waitTime = 0.3f;
    public float puttPower  = 15;
    public Transform head_;

    private Animator gangsterAnim_;
    public Transform spotlight;
    public Transform flashlight;
    private SpriteRenderer flashlightRenderer_;
    float viewAngle = 82;
    public float DesiredRot_;
    Vector3 startPos_;

    bool putting = false;

    public float viewDistance;
    public LayerMask viewMask;
    private Transform player_;
    BallControl strokeScript_;
    Vector3 puttDir_;

    private void Start()
    {
        strokeScript_ = FindObjectOfType<BallControl>();
        flashlightRenderer_ = flashlight.GetComponent<SpriteRenderer>();

        flashlightRenderer_.color = Color.yellow;
        player_ = GameObject.FindGameObjectWithTag("Player").transform;
        gangsterAnim_ = GetComponent<Animator>();
        if (gangsterAnim_ == null)
            Debug.LogError("no animator on: " + gameObject.name);
        
        startPos_ = transform.position;
        DesiredRot_ = transform.rotation.eulerAngles.y;

    }
    private void Update()
    {
        if (canSeePlayer() && !putting)
        {
            flashlightRenderer_.color = Color.red;
            StopAllCoroutines();
            gangsterAnim_.SetInteger("State", 1);
            transform.position = Vector3.MoveTowards(transform.position, player_.position, speed * Time.deltaTime);

            transform.LookAt(player_.position);
            if (Vector3.Distance(transform.position, player_.position) <= 1)
            {
                puttDir_ = (player_.position - transform.position).normalized;
                StartCoroutine(Putt());
            }
            else if (Vector3.Distance(transform.position, player_.position) >= viewDistance)
            {
                StartCoroutine(backTrack());
            }
        }
        spotlight.transform.rotation = head_.rotation;
    }
    bool canSeePlayer()
    {
        if (Vector3.Distance(transform.position, player_.position) < viewDistance)
        {
            Vector3 dirToPlayer = (player_.position - transform.position).normalized;
            float angleBetweenGangsterAndBall = Vector3.Angle(spotlight.transform.forward, dirToPlayer);
            if (angleBetweenGangsterAndBall < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, player_.position, viewMask))
                {
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
        }
        StartCoroutine(backTrack());
    }

    IEnumerator TurnToFace(float angle)
    {
        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, angle)) > 0.05f)
        {
            float angle_ = Mathf.MoveTowardsAngle(transform.eulerAngles.y, angle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle_;
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
        flashlightRenderer_.color = Color.yellow;
        yield return StartCoroutine(TurnToFace(startPos_));
        gangsterAnim_.SetInteger("State", 1);

        while (transform.position != startPos_)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos_, speed * Time.deltaTime);
            yield return null;
        }
        gangsterAnim_.SetInteger("State", 0);
        flashlightRenderer_.enabled = true;
        yield return StartCoroutine(TurnToFace(DesiredRot_));
        putting = false;
    }
    private void OnDrawGizmos()
    {
        

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }
}

