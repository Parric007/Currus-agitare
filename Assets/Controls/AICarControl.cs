using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarControl : MonoBehaviour
{
    GameObject checkpointManager;
    CheckpointHandling cpH;
    Rigidbody2D aiCarRB;
    public int currentCheckpoint = 0;
    float x; 

    void Awake() {
        aiCarRB = GetComponent<Rigidbody2D>();
    }

    void Start() {
        checkpointManager = GameObject.Find("Checkpoints");
        cpH = checkpointManager.GetComponent<CheckpointHandling>();

    }

    void FixedUpdate() {
        int currentCheckpointTriple = (int)(currentCheckpoint)/3;
        x = aiCarRB.position.x + 0.2f;
        Vector2 targetPosition = new Vector2(x, cpH.getA_AtCheckpoint(currentCheckpointTriple)*x*x + cpH.getB_AtCheckpoint(currentCheckpointTriple) * x + cpH.getC_AtCheckpoint(currentCheckpointTriple));
        //Debug.Log(currentCheckpointTriple);
        aiCarRB.AddForce(targetPosition-aiCarRB.position, ForceMode2D.Force);
        //aiCarRB.velocity = 5*(targetPosition-aiCarRB.position);
        if(aiCarRB.velocity == Vector2.zero) return;
        Quaternion qt = Quaternion.LookRotation(aiCarRB.velocity);
        qt *= Quaternion.Euler(90, 0, 0);
        aiCarRB.MoveRotation(qt);

        Vector2 currentOrthogonal = cpH.getOrthogonalAtCheckpoint(currentCheckpoint);
        float factorX = currentOrthogonal.x/aiCarRB.position.x;
        float factorY = currentOrthogonal.y/aiCarRB.position.y;
        

        if(Mathf.Abs(factorX-factorY) < 0.5 && Mathf.Abs((aiCarRB.position-cpH.getCheckpointAt(currentCheckpoint)).magnitude) < 5) {
            currentCheckpoint += 1;
            if(currentCheckpoint==cpH.NumberOfCheckpoints()) {
                currentCheckpoint = 0;
            }
        }
    }
    void Update() {}
}
