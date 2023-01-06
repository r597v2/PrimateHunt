using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeHitSound : MonoBehaviour
{
    private bool swingStart;
    public PlayerMovement playerMvmt;

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && playerMvmt.currentAnimController == "axe")
        {
            swingStart = true;
            Invoke("EndSwing", 1f);
        }
    }

    private void EndSwing()
    {
        swingStart = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (swingStart)
        {
            if (collision.tag == "Head" || collision.tag == "Player")
            {
                gameObject.GetComponent<AudioSource>().Play();
            }

            swingStart = false;
        }
    }
}
