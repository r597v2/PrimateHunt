using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HumidityMeter : MonoBehaviour
{
    public GameObject meterObj;
    private bool meterActive;
    public GameObject screen;
    private bool screenOn;
    public TMP_Text meterReading;
    private Ghost ghost;
    public LayerMask ghostLayer;

    // Start is called before the first frame update
    void Start()
    {
        ghost = FindObjectOfType<Ghost>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && meterActive)
        {
            if (!screenOn)
            {
                screen.SetActive(true);
                screenOn = true;
                meterReading.text = "--";
                InvokeRepeating("MeterReadings", 1f, Random.Range(1f, 2.5f));
            }
            else
            {
                screen.SetActive(false);
                screenOn = false;
            }
        }
    }

    public void EquipHumidityMeter()
    {
        meterObj.SetActive(true);
        meterActive = true;
    }

    public void UnEquipHumidityMeter()
    {
        meterObj.SetActive(false);
        meterActive = false;
    }

    private void MeterReadings()
    {
        if (Ghost.HasEvidence(ghost.ghostType, GhostEvidence.Humid) && Physics.CheckSphere(meterObj.transform.position, 
        5f, ghostLayer))
        {
            meterReading.text = Random.Range(80, 99) + "%";
        }
        else
        {
            meterReading.text = Random.Range(38, 61) + "%";
        }
    }
}
