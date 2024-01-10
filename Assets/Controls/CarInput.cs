using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using JetBrains.Annotations;
using TMPro;

public class CarInput : MonoBehaviour
{

    int currentWaypointNumber = 0;
    int lapCounter = 0;
    int cpToReach;
    int roundsToReach = 5;
    TimeStopping timecontroller;
    CarControls carController;
    GameObject[] goalGO;
    
    Waypoint currentWaypoint = null;
    Waypoint[] allWaypoints;
    public GameObject Menu;
    public GameObject finishScreen;
    public GameObject overlay;
    public TMP_Text steuerText;

    void Start() {
        int pref = PlayerPrefs.GetInt("carSelected");
        switch (pref) {
            case 1:
                transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 2:
                transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 3:
                transform.GetChild(3).gameObject.SetActive(true);
                break;
            case 4:
                transform.GetChild(4).gameObject.SetActive(true);
                break;
            default:
                transform.GetChild(1).gameObject.SetActive(true);
                break;
        }        
    }



    void Awake() {
        Time.timeScale = 1f;
        carController = GetComponent<CarControls>();
        timecontroller = GetComponents<TimeStopping>()[0];
        allWaypoints = FindObjectsOfType<Waypoint>();
        goalGO = GameObject.FindGameObjectsWithTag("Goal");
        cpToReach = (int)(allWaypoints.Length / 2) + 1;
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(!Menu.activeInHierarchy) {
                Time.timeScale = 0;
                Menu.SetActive(true);
                steuerText.gameObject.SetActive(true);
            } 
        }
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        carController.SetInputVector(inputVector);

        if (currentWaypoint == null) {
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
                            lapCounter++;
                            if(lapCounter == roundsToReach) {
                                finishGame();
                            } else {
                                currentWaypointNumber = 1;
                                timecontroller.resetLapTime(lapCounter);
                            }
                            return;
                        }
                    }    
                }
        }
    }

    Waypoint FindClosestWaypoint() {
        return allWaypoints.OrderBy(t =>Vector3.Distance(transform.position, t.transform.position)).FirstOrDefault();
    }

    void finishGame() {
        overlay.SetActive(false);
        finishScreen.SetActive(true);


    }
}
