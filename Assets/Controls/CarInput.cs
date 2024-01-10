using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEditor.UI;
using JetBrains.Annotations;

public class CarInput : MonoBehaviour
{

    int currentWaypointNumber = 0;
    int lapCounter = 0;
    int cpToReach;
    TimeStopping timecontroller;
    CarControls carController;
    GameObject[] goalGO;
    public GameObject Menu;
    Waypoint currentWaypoint = null;
    Waypoint[] allWaypoints;

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



    void selectCar() {
        PlayerPrefs.SetInt("carSelected", Zahl zwischen 1 und 4, je nach Auswahl);
    }


    void Awake() {
        carController = GetComponent<CarControls>();
        timecontroller = transform.GetChild(0).transform.GetChild(0).transform.GetComponent<TimeStopping>();
        allWaypoints = FindObjectsOfType<Waypoint>();
        goalGO = GameObject.FindGameObjectsWithTag("Goal");
        cpToReach = (int)(allWaypoints.Length / 2) + 1;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)) {
            if(Menu.activeInHierarchy) {
                Time.timeScale = 1;
                Menu.SetActive(false);
            } else {
                Time.timeScale = 0;
                Menu.SetActive(true);
            }
        }
    }

    public void PlayButton()
    {
        Menu.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitButton()
    {
        Application.Quit(); 
    }

    public void OptionButton()
    {
        SceneManager.LoadScene("Start_Menu");
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
