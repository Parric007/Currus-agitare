using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    float movementSpeed = 5f;
    float rotationSpeed = 200f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKey(KeyCode.W)) {
			transform.position += transform.up * Time.deltaTime * movementSpeed;
		}
		else if(Input.GetKey(KeyCode.S)) {
            transform.position -= transform.up * Time.deltaTime * movementSpeed/2;
		}
	

		if(Input.GetKey(KeyCode.A)) {
            if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) {
                transform.position += transform.up * Time.deltaTime * movementSpeed;
            }
			transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
		}
		else if(Input.GetKey(KeyCode.D)) {
            if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) {
                transform.position += transform.up * Time.deltaTime * movementSpeed;
            }
			transform.Rotate(0, 0, -Time.deltaTime * rotationSpeed);
		}
    }
}
