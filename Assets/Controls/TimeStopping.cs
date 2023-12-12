using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeStopping : MonoBehaviour
{

    float passedLapTime = 0;
    int passedMinutes = 0;
    List<string> pastLaps = new List<string>();

    public TMP_Text tmp;

    void Awake() {
        //tmp = GetComponent<TextMeshPro>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        passedLapTime += Time.deltaTime;  
        if(Mathf.Abs(passedLapTime-60) <= 0.05) {
            passedMinutes += 1;
            passedLapTime = 0;
        }
        string addText = passedMinutes.ToString() + ":" + passedLapTime.ToString("00.00");
        foreach(string str in pastLaps) {
            addText += str;
        }
        tmp.text = addText;

    }

    public void resetLapTime(int lapcnt) {
        string newText = "\n Lap " + lapcnt.ToString() + ": " + passedMinutes.ToString() + ":" + passedLapTime.ToString("00.00");
        pastLaps.Add(newText);
        passedMinutes = 0;
        passedLapTime = 0;
    }
}
