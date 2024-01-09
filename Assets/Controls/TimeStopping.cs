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
            return current.passedMinutes<=other.passedMinutes && current.passedSeconds<other.passedSeconds;
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

    TimeVal passedTime = new TimeVal(0,0f);
    TimeVal fastestTime;
    List<string> pastLaps = new List<string>();
    

    public TMP_Text currentTimeText;
    public TMP_Text passedLapsText;
    public TMP_Text fastestTimeText;

    void Awake() {
        //currentTimeText = GetComponent<TextMeshPro>();
    }


    // Start is called before the first frame update
    void Start()
    {
        fastestTime = new TimeVal(0,0f);
        passedLapsText.text = "";
        fastestTimeText.text = "";
        updateFastestTime();
    }

    // Update is called once per frame
    void Update()
    {
        passedTime += Time.deltaTime;
        string addText = passedTime.toString();
        //foreach(string str in pastLaps) {
        //    addText += str;

        currentTimeText.text = addText;

    }

    public void resetLapTime(int lapcnt) {
        string newText = "\n Lap " + lapcnt.ToString() + ": " + passedTime.toString();

        if (passedTime < fastestTime) {
            fastestTime = passedTime;
            updateFastestTime();
        }
        passedTime.reset();
        foreach(string str in pastLaps) {
            newText += str;
        }
        pastLaps.Add(newText);
        passedLapsText.text = newText;
    }
    public void  updateFastestTime() {
        fastestTimeText.text = "Schnellste Runde: " + fastestTime.toString();
    }
}
