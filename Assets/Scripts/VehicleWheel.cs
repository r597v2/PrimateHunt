using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleWheel : MonoBehaviour
{
    public bool powered = false;
    public float maxAngle = 45f;
    public float offset = 0f;

    private float turnAngle;
    private WheelCollider wCol;
    private Transform wMesh;

    // Start is called before the first frame update
    void Start()
    {
        wCol = GetComponentInChildren<WheelCollider>();
        wMesh = transform.Find("WheelMesh");
    }

    public void Steer(float steerInput)
    {
        turnAngle = steerInput * maxAngle + offset;
        wCol.steerAngle = turnAngle;
    }
    public void Accelerate(float powerInput)
    {
        if(powered) wCol.motorTorque = powerInput;
        else wCol.brakeTorque = 0;
    }
    public void Brake()
    {
        wMesh.transform.rotation = Quaternion.identity;
        wCol.brakeTorque = 30000;
    }
    public void StopBrake()
    {
        wCol.brakeTorque = 0;
    }
    public void UpdatePos()
    {
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;

        wCol.GetWorldPose(out pos, out rot);
        wMesh.transform.position = pos;
        wMesh.transform.rotation = rot;
    }
}
