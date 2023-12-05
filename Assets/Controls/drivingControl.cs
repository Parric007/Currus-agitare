using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    float maxSpeed = 5f;
    float rotationSpeed = 200f;
    public float velocity = 0f;
    float rotVelocity = 2.5f;
    float acceleration = 0.025f;
    float deceleration = 0.005f;
    float breakingFactor = 0.05f;
    bool keysPressed = false;



    // Start is called before the first frame update
    void Start()
    {
        /*
        String selectedCar = getCarSelection();

        Switch selectedCar:
            Case "rennauto": maxSpeed = 5f;
            Case "traktor": maxSpeed = 3f;
            Case "Gabelstapler": maxSpeed = 3.5f;
            Case "Rennauto": maxSpeed = 4f;
            */
    }
    // Update is called once per frame
    void Update()
    {
        
        transform.position += transform.up * Time.deltaTime * velocity;
        if(Input.GetKey(KeyCode.W)) {
            if(velocity < maxSpeed) {
            velocity += acceleration;;
            }
            keysPressed = true;
        } else if(Input.GetKey(KeyCode.S)) {
            if(velocity > -maxSpeed/2) {
                velocity -= breakingFactor;
            }
            keysPressed = true;
        }else {
            keysPressed = false;
        }
        if(Input.GetKey(KeyCode.A)) {
            if(velocity<maxSpeed/2 && velocity>=0) {
                velocity += acceleration;
            }else if(velocity>-maxSpeed/2 && velocity<0){
                velocity -= acceleration;
            }
            transform.Rotate(0, 0, Time.deltaTime*rotationSpeed);
        } else if(Input.GetKey(KeyCode.D)) {
            if(velocity<maxSpeed/2 && velocity>=0) {
                velocity += acceleration;
            }else if(velocity>-maxSpeed/2 && velocity<0){
                velocity -= acceleration;
            }
            transform.Rotate(0, 0, -Time.deltaTime*rotationSpeed);
        } else if(!keysPressed) {
            if(velocity > 0.5) {
                velocity -= deceleration;
            } else if(velocity < -0.5) {
                velocity += deceleration;
            } else {
                velocity = 0;
            }
        }
    }
    private void onTriggerEnter(Collider other) {
        velocity = -velocity/2;
    }
}
