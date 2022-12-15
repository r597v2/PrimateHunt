using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CaptureZone : MonoBehaviour
{
    public GameObject vaultDoor;

    private float captureProgress = 0f;
    private bool capturing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (capturing)
        {
            captureProgress += 5 * Time.deltaTime;
        }

        if (captureProgress >= 50f)
        {
            gameObject.SetActive(false);
        }

        Debug.Log("capture progress = " + captureProgress);
    }

    private void OnTriggerEnter(Collider collider)
    {
        capturing = true;
    }

    private void OnTriggerExit(Collider collider)
    {
        capturing = false;
    }
}
