using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTrailRendererHandler : MonoBehaviour
{
    CarControls carControls;
    TrailRenderer trailRenderer;


    void Awake() {
        carControls = GetComponentInParent<CarControls>();
        trailRenderer = GetComponent<TrailRenderer>();

        trailRenderer.emitting = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(carControls.IsTireScreeching(out float lateralVelocity, out bool isBraking)) {
            trailRenderer.emitting = true;
        } else {
            trailRenderer.emitting = false;
        }
    }
}
