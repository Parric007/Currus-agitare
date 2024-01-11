using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeStopping : MonoBehaviour
{
    public class TimeVal {
        public int passedMinutes;
        public float passedSeconds;

        public TimeVal(int minIn, float secIn) {
            if(minIn == 0 && secIn == 0f) {
                throw new Exception("Zeit gleich Null");
            }
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
            first = new TimeVal(_first.passedMinutes, _first.passedSeconds);
            second = new TimeVal(_second.passedMinutes, _second.passedSeconds);
            third = new TimeVal(_third.passedMinutes, _third.passedSeconds);
        }
        public TimeVal getFirst() {
            return first;
        }
        public MultipleTimes checkTime(TimeVal toCheck, out bool bol) {
            //Debug.Log(toCheck.toString());
            //Debug.Log(first.toString());
            bol = false;
            if(toCheck < first) {
                bol = true;
                return new MultipleTimes(toCheck, first, second);
            }else if(toCheck < second) {
                return new MultipleTimes(first, toCheck, second);
            } else if(toCheck < third) {
                return new MultipleTimes(first, second, toCheck);
            }
            return this;
        }
        public string toString() {
            return $"{first.toString()}, {second.toString()}, {third.toString()}";
        }
    }

    IDataService dataService = new JsonDataService();
    TimeVal passedTime = new TimeVal(0,0.001f);
    TimeVal totalTime = new TimeVal(0,0.001f);
    MultipleTimes top3Times;
    TimeVal fastestTime;
    List<string> pastLaps = new List<string>();    

    public TMP_Text currentTimeText;
    public TMP_Text passedLapsText;
    public TMP_Text fastestTimeText;
    public TMP_Text totalTimeText;

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
        fastestTimeText.text = fastestTime.toString();
    }

    // Update is called once per frame
    void Update()
    {
        passedTime += Time.deltaTime; totalTime += Time.deltaTime;
        currentTimeText.text = passedTime.toString();
        totalTimeText.text = totalTime.toString();
    }

    public void resetLapTime(int lapcnt) {
        string newText = $"Runde {lapcnt.ToString()}: {passedTime.toString()} \n";//"\n Lap " + lapcnt.ToString() + ": " + passedTime.toString();
        //top3Times = dataService.LoadData<MultipleTimes>("/top3Times.json", false);
        top3Times = top3Times.checkTime(passedTime, out bool bol);
        if (bol) {
            fastestTime = passedTime;
            fastestTimeText.text = fastestTime.toString();
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
