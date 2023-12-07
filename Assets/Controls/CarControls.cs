using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControls : MonoBehaviour
{
    [Header("Car settings")]
    float accelerationFactor = 2f;
    float turnFactor = 3f;
    float driftFactor = 0.85f;
    float maxSpeed = 7f;
    float bouncyness = 10f;
    

    //LocalVariables
    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;
    float velocityVsUp = 0;
    float bouncynessWall = 3f;
    Vector2 newVelocity = new Vector2(0,1);
    

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
        RaycastHit2D hit = Physics2D.Raycast(carRigidbody2D.position,carRigidbody2D.velocity, carRigidbody2D.velocity.magnitude);
        if(hit) {
            if(hit.collider.tag == "WorldBorder") {
                newVelocity = Vector2.Reflect(carRigidbody2D.velocity, hit.normal)*bouncynessWall;
                //Debug.Log(Vector2.Reflect(carRigidbody2D.velocity, hit.normal));               
            } else {
                newVelocity = Vector2.Reflect(carRigidbody2D.velocity,hit.normal)*bouncyness;
            }
        } else {
            newVelocity = carRigidbody2D.velocity;
        }
        
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

    void OnCollisionEnter2D() {
        //print(carRigidbody2D.velocity);
        if(newVelocity.magnitude > maxSpeed) {
            newVelocity = newVelocity * (1/newVelocity.magnitude) * maxSpeed;
        }
        if(newVelocity.magnitude < -maxSpeed/2) {
            newVelocity = newVelocity * -(1/newVelocity.magnitude) * maxSpeed/2;
        }
        carRigidbody2D.velocity = newVelocity;
    }
}
