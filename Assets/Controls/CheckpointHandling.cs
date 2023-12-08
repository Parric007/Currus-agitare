using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHandling : MonoBehaviour
{
    struct Function {
        float a; //a*x^2 + b*x + c
        float b;
        float c;
    public Function(float a_in, float b_in, float c_in) {
        a = a_in;
        b = b_in;
        c = c_in;
    }
    public void PrintFunc() {
        Debug.Log(a + "x^2 +" + b + "x + " + c);
    }
    public float getA() {
        return a;
    }
    public float getB() {
        return b;
    }
    public float getC() {
        return c;
    }
    }

    List<Vector2> checkpointPositions = new List<Vector2>();
    List<Function> Functions = new List<Function>();

    public float getA_AtCheckpoint(int cp) {
        return Functions[cp].getA();
    }
    public float getB_AtCheckpoint(int cp) {
        return Functions[cp].getB();
    }
    public float getC_AtCheckpoint(int cp) {
        return Functions[cp].getC();
    }


    // Start is called before the first frame update
    void Start()
    {
        GameObject parentObject = GameObject.Find("Checkpoints");
        for(int i = 0; i<3; i++) {
            checkpointPositions.Add(parentObject.transform.GetChild(i).gameObject.transform.position);
        }

        for(int j = 0; j< checkpointPositions.Count/3; j++) {
            Vector2 pos1 = checkpointPositions[j];
            Vector2 pos2 = checkpointPositions[j+1];
            Vector2 pos3 = checkpointPositions[j+2];
            float a = (pos1.x*(pos3.y-pos2.y) + pos2.x*(pos1.y-pos3.y) + pos3.x*(pos2.y-pos1.y))/((pos1.x-pos2.x)*(pos1.x-pos3.x)*(pos2.x-pos3.x));
            float b = (pos2.y-pos1.y)/(pos2.x-pos1.x) - a*(pos1.x+pos2.x);
            float c = pos1.y - a*(Mathf.Pow(pos1.x,2))-b*pos1.x;
            Functions.Add(new Function(a,b,c));
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
