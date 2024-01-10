using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeStopping : MonoBehaviour
{
    public class TimeVal {
        public int passedMinutes;
        public float passedSeconds;

        public TimeVal(int minIn, float secIn) {
            passedMinutes = minIn;
            passedSeconds = secIn;
        }

        public static TimeVal operator +(TimeVal current, TimeVal other) {
            return new TimeVal(current.passedMinutes+other.passedMinutes, current.passedSeconds+other.passedSeconds);
        }
        public static TimeVal operator +(TimeVal current, float addTime) {
            if (current.passedSeconds + addTime < 60f) {
                return new TimeVal(current.passedMinutes, current.passedSeconds+addTime);
            }else {
                return new TimeVal(current.passedMinutes+1, current.passedSeconds+addTime-60);
            }
            
        }
        public static bool operator <(TimeVal current, TimeVal other) {
            if(current.passedMinutes<other.passedMinutes) {
                return true;
            } else if(current.passedMinutes==other.passedMinutes) {
                return current.passedSeconds<other.passedSeconds;
            }
            return false;
        }
        public static bool operator >(TimeVal current, TimeVal other) {
            return current.passedMinutes>=other.passedMinutes && current.passedSeconds>other.passedSeconds;
        }
        public string toString() {
            return passedMinutes.ToString() + ":" + passedSeconds.ToString("00.00");
        }
        public void reset() {
            passedMinutes = 0;
            passedSeconds = 0;
        }
    }
    public class MultipleTimes {
        public TimeVal first;
        public TimeVal second;
        public TimeVal third;

        public MultipleTimes(TimeVal _first, TimeVal _second, TimeVal _third) {
            first = _first;
            second = _second;
            third = _third;
        }

        public bool checkTime(TimeVal toCheck) {
            //Debug.Log(toCheck.toString());
            //Debug.Log(first.toString());
            if(toCheck < first) {
                third = second;
                second = first;
                first = toCheck;
                return true;
            }else if(toCheck < second) {
                third = second;
                second = toCheck;
                return false;
            } else if(toCheck < third) {
                third = toCheck;
                return false;
            }
            return false;
        }
        public TimeVal getFirst() {
            return first;
        }
    }

    IDataService dataService = new JsonDataService();
    TimeVal passedTime = new TimeVal(0,0f);
    MultipleTimes top3Times;
    TimeVal fastestTime;
    List<string> pastLaps = new List<string>();
    

    public TMP_Text currentTimeText;
    public TMP_Text passedLapsText;
    public TMP_Text fastestTimeText;

    void Awake() {
        
    }


    // Start is called before the first frame update
    void Start()
    {
        //top3Times = new MultipleTimes(new TimeVal(4,0f), new TimeVal(5,0f), new TimeVal(6,0f));
        try {
            top3Times = dataService.LoadData<MultipleTimes>("/top3Times.json", false);
        }           
        catch {
            top3Times = new MultipleTimes(new TimeVal(4,0f), new TimeVal(5,0f), new TimeVal(6,0f));
        }
        
        fastestTime = top3Times.getFirst();//dataService.LoadData<TimeVal>("/fastestTime.json", false);
        passedLapsText.text = "";
        fastestTimeText.text = "Schnellste Runde: " + fastestTime.toString();
    }

    // Update is called once per frame
    void Update()
    {
        passedTime += Time.deltaTime;
        string addText = passedTime.toString();
        currentTimeText.text = addText;

    }

    public void resetLapTime(int lapcnt) {
        string newText = "\n Lap " + lapcnt.ToString() + ": " + passedTime.toString();
        //top3Times = dataService.LoadData<MultipleTimes>("/top3Times.json", false);

        if (top3Times.checkTime(passedTime)) {
            fastestTime = passedTime;
            fastestTimeText.text = "Schnellste Runde: " + fastestTime.toString();
        }
        serializeJson();
        passedLapsText.text += newText;
        passedTime.reset();
    }

    public void serializeJson() {
        if (dataService.SaveData("/top3Times.json", top3Times, false)) {
            //Debug.Log("Succes");
        } else {
            Debug.LogError("AHH");
        }
    }
}
