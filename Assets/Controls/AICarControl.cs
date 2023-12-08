using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarControl : MonoBehaviour
{
    GameObject checkpointManager;
    CheckpointHandling cpH;
    Rigidbody2D aiCarRB;
    int currentCheckpoint = 0;
    float x; 

    void Awake() {
        aiCarRB = GetComponent<Rigidbody2D>();
    }

    void Start() {
        checkpointManager = GameObject.Find("Checkpoints");
        cpH = checkpointManager.GetComponent<CheckpointHandling>();

    }

    void FixedUpdate() {
        x = aiCarRB.position.x + 0.2f;
        Vector2 targetPosition = new Vector2(x, cpH.getA_AtCheckpoint(0)*x*x + cpH.getB_AtCheckpoint(0) * x + cpH.getC_AtCheckpoint(0));
        aiCarRB.AddForce(targetPosition-aiCarRB.position, ForceMode2D.Force);
        Quaternion qt = Quaternion.LookRotation(aiCarRB.velocity);
        qt *= Quaternion.Euler(90, 0, 0);
        aiCarRB.MoveRotation(qt);
    }
    void Update() {}
}
