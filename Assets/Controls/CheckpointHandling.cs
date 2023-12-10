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

    List<Vector2> checkpointOrthogonal = new List<Vector2>(); 

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
    public Vector2 getCheckpointAt(int cp) {
        return checkpointPositions[cp];
    }
    public Vector2 getOrthogonalAtCheckpoint(int cp) {
        return checkpointOrthogonal[cp];
    }
    public int NumberOfCheckpoints() {
        return checkpointPositions.Count;
    }


    // Start is called before the first frame update
    void Start()
    {
        Vector2 pos1,pos2,pos3,orthogonalVector, derivate, current;
        float a,b,c;
        AddAllCheckpoints();
        /**GameObject parentObject = GameObject.Find("Checkpoints");
        for(int i = 0; i<parentObject.transform.childCount; i++) {
            GameObject child = parentObject.transform.GetChild(i).gameObject;
            if(child.tag == "Checkpoint") {
                checkpointPositions.Add(child.transform.position);
            }
        }
        */
    
        for(int j = 0; j< checkpointPositions.Count/3; j++) {
            pos1 = checkpointPositions[j];
            pos2 = checkpointPositions[j+1];
            pos3 = checkpointPositions[j+2];
            a = (pos1.x*(pos3.y-pos2.y) + pos2.x*(pos1.y-pos3.y) + pos3.x*(pos2.y-pos1.y))/((pos1.x-pos2.x)*(pos1.x-pos3.x)*(pos2.x-pos3.x));
            b = (pos2.y-pos1.y)/(pos2.x-pos1.x) - a*(pos1.x+pos2.x);
            c = pos1.y - a*(Mathf.Pow(pos1.x,2))-b*pos1.x;
            
            Functions.Add(new Function(a,b,c));
            new Function(a,b,c).PrintFunc();
        }

        int secCounter = 0;
        for(int k = 0; k<checkpointPositions.Count; k++) {
            current = checkpointPositions[k];
            derivate = new Vector2(1,getA_AtCheckpoint(secCounter)*2*current.x + getB_AtCheckpoint(secCounter));

            orthogonalVector = Vector2.Perpendicular(derivate.normalized);
            checkpointOrthogonal.Add(orthogonalVector);
    
            if(k%4 == 3) {
                secCounter++;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

        
    }


    void AddAllCheckpoints() {
        //1
        checkpointPositions.Add(new Vector2(-21.5f,17.5f));
        checkpointPositions.Add(new Vector2(-17f,21.75f));
        checkpointPositions.Add(new Vector2(-11.75f,19.75f));
        //2
        checkpointPositions.Add(new Vector2(-9.5f,19.25f));
        checkpointPositions.Add(new Vector2(-5.75f,19f));
        checkpointPositions.Add(new Vector2(-0.5f,19.25f));
        //3
        checkpointPositions.Add(new Vector2(4f,20.25f));
        checkpointPositions.Add(new Vector2(10f,20.25f));
        checkpointPositions.Add(new Vector2(17f,18.5f));
        //4
        checkpointPositions.Add(new Vector2(23f,13.25f));
        checkpointPositions.Add(new Vector2(22f,10f));
        checkpointPositions.Add(new Vector2(17.5f,8.75f));
        //5
        checkpointPositions.Add(new Vector2(15f,8.5f));
        checkpointPositions.Add(new Vector2(11f,7.25f));
        checkpointPositions.Add(new Vector2(26f,-11f));
        //6
        checkpointPositions.Add(new Vector2(31.5f,-17f));
        checkpointPositions.Add(new Vector2(30.25f,-20f));
        checkpointPositions.Add(new Vector2(22.5f,-19.5f));
        //7
        checkpointPositions.Add(new Vector2(14.5f,-17.75f));
        checkpointPositions.Add(new Vector2(10f,-13.75f));
        checkpointPositions.Add(new Vector2(4.5f,-15.5f));
        //8
        checkpointPositions.Add(new Vector2(-3.25f,-16.5f));
        checkpointPositions.Add(new Vector2(-8f,-13.5f));
        checkpointPositions.Add(new Vector2(-7.5f,-8f));
        //9
        checkpointPositions.Add(new Vector2(-6.5f,-3f));
        checkpointPositions.Add(new Vector2(-10.5f,0f));
        checkpointPositions.Add(new Vector2(-15.5f,-3f));
        //10
        checkpointPositions.Add(new Vector2(-15.25f,-14.5f));
        checkpointPositions.Add(new Vector2(-18.75f,-17.75f));
        checkpointPositions.Add(new Vector2(-22f,-7.75f));
    }

}
