using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveVehicle : MonoBehaviour
{
    // public Transform gravityTarget;

    public float power = 15000f;
    public float brakePower = 20000f;
    // public float torque = 500f;
    // public float gravity = 9.81f;

    // public bool autoOrient = false;
    // public float autoOrientSpeed = 1f;

    private float horInput;
    private float verInput;
    private float steerAngle;
    private float brakeInput;

    public VehicleWheel[] wheels;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProcesInput();
        // Vector3 diff = transform.position - gravityTarget.position;
    }

    void FixedUpdate()
    {
        ProcessForces();
    }

    void ProcesInput()
    {
        verInput = Input.GetAxis("Vertical");
        horInput = Input.GetAxis("Horizontal");
    }

    void ProcessForces()
    {
        foreach (VehicleWheel wheel in wheels)
        {
            wheel.Steer(horInput);
            wheel.Accelerate(verInput * power);
            wheel.UpdatePos();
            if(Input.GetKey(KeyCode.Space)) wheel.Brake();
        }
    }
}
