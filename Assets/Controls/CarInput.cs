using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CarInput : MonoBehaviour
{

    public int currentWaypointNumber = 0;
    public int lapCounter = 0;
    public int cpToReach;


    TimeStopping timecontroller;
    CarControls carController;
    GameObject[] goalGO;
    Waypoint currentWaypoint = null;
    Waypoint[] allWaypoints;

    void Awake() {
        carController = GetComponent<CarControls>();
        timecontroller = transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetComponent<TimeStopping>();
        allWaypoints = FindObjectsOfType<Waypoint>();
        goalGO = GameObject.FindGameObjectsWithTag("Goal");
        cpToReach = (int) (allWaypoints.Length/2)+1;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        carController.SetInputVector(inputVector);

        if(currentWaypoint == null) {
            currentWaypoint = FindClosestWaypoint();
        }

        if(currentWaypoint != null) {

            float distanceToWaypoint = (currentWaypoint.transform.position - transform.position).magnitude;
            if(distanceToWaypoint <= currentWaypoint.minDistanceToReachWaypoint) {
                currentWaypoint = currentWaypoint.nextWaypoint[Random.Range(0,currentWaypoint.nextWaypoint.Length)];
                currentWaypointNumber += 1;
                
            }

            if(currentWaypointNumber%cpToReach == 0) {
                    foreach(GameObject go in goalGO) {
                        if((go.transform.position - transform.position).magnitude <= 2) {
                            lapCounter++; //Goal reached
                            currentWaypointNumber = 1;
                            timecontroller.resetLapTime(lapCounter);
                            return;
                        }
                    }    
                }
        }
    }

    Waypoint FindClosestWaypoint() {
        return allWaypoints.OrderBy(t =>Vector3.Distance(transform.position, t.transform.position)).FirstOrDefault();
    }
}
