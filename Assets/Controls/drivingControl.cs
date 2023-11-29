using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    float speed = 8f;
    float rotationSpeed = 500;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"),
			Input.GetAxis("Vertical"),0);
		


		// Update the ships position each frame
		transform.position += move * speed * Time.deltaTime;

		if(move != Vector3.zero) {
		    Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, move*Time.deltaTime*speed);
		    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed*Time.deltaTime);
		}
    }
}
