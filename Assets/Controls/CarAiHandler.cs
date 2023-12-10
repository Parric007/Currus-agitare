using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class CarAiHandler : MonoBehaviour
{

    public enum AIMode { followPlayer, followWaypoints };
    public AIMode aIMode;


    Vector3 targetPosition = Vector3.zero;
    Transform targetTransform = null;
    CarControls carControls;
    Waypoint currentWaypoint = null;
    Waypoint[] allWaypoints;
    // Start is called before the first frame update

    void Awake() {
        carControls = GetComponent<CarControls>();
        allWaypoints = FindObjectsOfType<Waypoint>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 inputVector = Vector2.zero;

        switch(aIMode) {
            case AIMode.followPlayer:
                FollowPlayer();
                break;
            case AIMode.followWaypoints:
                FollowWaypoint();
                break;
        }

        inputVector.x = TurnTowardTarget();
        inputVector.y = 0.9f;

        carControls.SetInputVector(inputVector);
    }

    void FollowPlayer() {
        if(targetTransform == null) {
            targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if(targetTransform != null) {
            targetPosition = targetTransform.position;
        }
    }

    void FollowWaypoint() {
        if(currentWaypoint == null) {
            currentWaypoint = FindClosestWaypoint();
        }

        if(currentWaypoint != null) {
            targetPosition = currentWaypoint.transform.position;

            float distanceToWaypoint = (targetPosition - transform.position).magnitude;
            if(distanceToWaypoint <= currentWaypoint.minDistanceToReachWaypoint) {
                currentWaypoint = currentWaypoint.nextWaypoint[Random.Range(0,currentWaypoint.nextWaypoint.Length)];
            }
        }
    }

    Waypoint FindClosestWaypoint() {
        return allWaypoints.OrderBy(t =>Vector3.Distance(transform.position, t.transform.position)).FirstOrDefault();
    }

    float TurnTowardTarget() {
        Vector2 vectorToTarget = targetPosition-transform.position;
        vectorToTarget.Normalize();
        float angleToTarget = Vector2.SignedAngle(transform.up, vectorToTarget);
        angleToTarget *= -1;
        float steerAmount = angleToTarget / 45.0f;
        steerAmount = Mathf.Clamp(steerAmount, -1.0f, 1.0f);
        return steerAmount;
    }
}
