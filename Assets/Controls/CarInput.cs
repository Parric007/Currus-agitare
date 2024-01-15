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
    bool isEnabled = true;
    int currentWaypointNumber = 0;
    int lapCounter = 0;
    int cpToReach;
    int roundsToReach = 1;
    TimeStopping timecontroller;
    CarControls carController;
    GameObject[] goalGO;
    GameObject[] aiCars;
    Waypoint currentWaypoint = null;
    Waypoint[] allWaypoints;
    List<GameObject> positions = new List<GameObject>();
    public GameObject Menu;
    public GameObject finishScreen;
    public GameObject overlay;
    public TMP_Text steuerText;
    public TMP_Text placementText;
    public TMP_Text finalPositionText;
    public TMP_Text totalTimeText;
    public TMP_Text bestTimeText;

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
        isEnabled = true;
        Time.timeScale = 1f;
        carController = GetComponent<CarControls>();
        timecontroller = GetComponents<TimeStopping>()[0];
        allWaypoints = FindObjectsOfType<Waypoint>();
        goalGO = GameObject.FindGameObjectsWithTag("Goal");
        aiCars = GameObject.FindGameObjectsWithTag("NPC-Car");
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
        if(!isEnabled) {
            return;
        }
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
                            timecontroller.resetLapTime(lapCounter);
                            if(lapCounter == roundsToReach) {
                                finishGame();
                            } else {
                                currentWaypointNumber = 1;   
                            }
                            return;
                        }
                    }    
                }
        }
        determinePlacement();
    }

    Waypoint FindClosestWaypoint() {
        return allWaypoints.OrderBy(t =>Vector3.Distance(transform.position, t.transform.position)).FirstOrDefault();
    }

    void finishGame() {
        overlay.SetActive(false);
        finishScreen.SetActive(true);
        int playerRank = positions.IndexOf(positions.Where(i => i.name == "Player").FirstOrDefault());
        foreach(GameObject go in positions) {
            if(go.name != "Player") {
                go.SetActive(false);
            }else {
                go.GetComponent<CarAiHandler>().aIMode = CarAiHandler.AIMode.followWaypoints;
                isEnabled = false;
            }     
        }
        finalPositionText.text += $"{playerRank}. Platz";
        totalTimeText.text += timecontroller.totalTime.toString();
        bestTimeText.text += timecontroller.fastestLocalTime.toString();

    }

    void determinePlacement() {
        positions = aiCars.OrderByDescending(t => t.GetComponent<CarAiHandler>().currentWaypointPosition).ThenBy(i => i.GetComponent<CarAiHandler>().distanceToWaypoint).ToList();
        placementText.text = placementToText();

    }

    string placementToText() {
        return $"Platz 1: {positions[0].name} \nPlatz 2: {positions[1].name} \nPlatz 3: {positions[2].name} \nPlatz 4: {positions[3].name} \nPlatz 5: {positions[4].name}";
    }
}
