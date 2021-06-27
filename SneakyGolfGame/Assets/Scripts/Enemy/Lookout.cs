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
    private ScoreUI score_;
    float viewAngle = 82;
    public float DesiredRot_;
    Vector3 startPos_;

    Color yellow_ = new Color(1, 0.92f, 0.016f, 0.39f);

    bool putting = false;
    bool seen = false;

    public float viewDistance;
    public LayerMask viewMask;
    private Transform player_;
    BallControl strokeScript_;
    BumpsAndSounds smackSound_; 
    Vector3 puttDir_;

    private void Start()
    {
        score_ = FindObjectOfType<ScoreUI>();
        strokeScript_ = FindObjectOfType<BallControl>();
        smackSound_ = FindObjectOfType<BumpsAndSounds>();
        flashlightRenderer_ = flashlight.GetComponent<SpriteRenderer>();

        flashlightRenderer_.color = yellow_;
        player_ = GameObject.FindGameObjectWithTag("Player").transform;
        gangsterAnim_ = GetComponent<Animator>();
        if (gangsterAnim_ == null)
            Debug.LogError("no animator on: " + gameObject.name);
        
        startPos_ = transform.position;
        DesiredRot_ = transform.rotation.eulerAngles.y;
        gangsterAnim_.SetInteger("State", 0);

    }
    private void Update()
    {
        if (canSeePlayer() && !putting)
        {
            if (score_.detected_ == false)
                score_.detected_ = true;
            seen = true;
            flashlightRenderer_.color = new Color(1, 0, 0, 0.39f);
            
            StopAllCoroutines();
            gangsterAnim_.SetInteger("State", 1);
            transform.position = Vector3.MoveTowards(transform.position, player_.position, speed * Time.deltaTime);

            transform.LookAt(player_.position);
            if (Vector3.Distance(transform.position, player_.position) <= 1)
            {
                puttDir_ = (player_.position - transform.position).normalized;
                StartCoroutine(Putt());
            }
            
            
        }
        else if(seen)
        {
            StartCoroutine(backTrack());
            seen = false;
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
            smackSound_.spawnPrefab(1, player_.position);
            smackSound_.ShotSound();
            
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
        putting = true;
        flashlightRenderer_.color = yellow_;
        yield return StartCoroutine(TurnToFace(startPos_));
        gangsterAnim_.SetInteger("State", 1);

        while (transform.position != startPos_)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos_, speed * Time.deltaTime);
            yield return null;
        }
        gangsterAnim_.SetInteger("State", 0);
        
        yield return StartCoroutine(TurnToFace(DesiredRot_));
        yield return new WaitForSeconds(3);
        flashlightRenderer_.enabled = true;
        putting = false;
    }
    private void OnDrawGizmos()
    {
        

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }
}

