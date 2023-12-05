using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControls : MonoBehaviour
{
    [Header("Car settings")]
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float driftFactor = 0.9f;
    public float maxSpeed = 5f;

    //LocalVariables
    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;
    float velocityVsUp = 0;

    Rigidbody2D carRigidbody2D;

    void Awake() {
        carRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        ApplyEngineForce();
        KillOrthogonalVelocity();
        ApplySteering();
    }

    void ApplyEngineForce() {

        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        if(velocityVsUp > maxSpeed && accelerationInput > 0) {
            return;
        }
        if(velocityVsUp < -maxSpeed/2 && accelerationInput < 0) {
            return;
        }
        if(carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0) {
            return;
        }

        if(accelerationInput == 0) {
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        }        
        else carRigidbody2D.drag = 0;

        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering() {

        float minSpeedFactor = (carRigidbody2D.velocity.magnitude/8);
        minSpeedFactor = Mathf.Clamp01(minSpeedFactor);


        rotationAngle -= steeringInput * turnFactor * minSpeedFactor;
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    void KillOrthogonalVelocity() {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        carRigidbody2D.velocity = forwardVelocity + rightVelocity*driftFactor;
    }

    float GetLateralVelocity() {
        return Vector2.Dot(transform.right, carRigidbody2D.velocity);
    }

    public bool IsTireScreeching(out float lateralVelocity, out bool isBraking) {
        lateralVelocity = GetLateralVelocity();
        isBraking = false;

        if(accelerationInput < 0 && velocityVsUp > 0) {
            isBraking = true;
            return true;
        }
        if(Mathf.Abs(GetLateralVelocity())>0.75f) {
            return true;
        }
        return false;
    }


    public void SetInputVector(Vector2 inputVector) {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

}
