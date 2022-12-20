using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeHitSound : MonoBehaviour
{
    private bool swingStart;
    private bool lightAttackStart;
    private bool heavyAttackStart;
    public PlayerMovement playerMvmt;

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && playerMvmt.currentAnimController == "axe")
        {
            swingStart = true;
            Invoke("EndSwing", 1f);
        }
        if (Input.GetMouseButtonDown(0))
        {
            lightAttackStart = true;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            heavyAttackStart = true;
        }
    }

    private void EndSwing()
    {
        swingStart = false;
        lightAttackStart = false;
        heavyAttackStart = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (swingStart) // || collision.tag == "Head"))
        {
            PlayerMovement playerMovement = null;

            if (collision.tag == "Player")
            {
                playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            }
            else if (collision.tag == "Head")
            {
                playerMovement = collision.gameObject.GetComponentInParent<PlayerMovement>();
            }

            swingStart = false;
            gameObject.GetComponent<AudioSource>().Play();
            
            if (lightAttackStart)
            {
                playerMovement.health -= 15f;
            }
            else if (heavyAttackStart)
            {
                playerMovement.health -= 30f;
            }
        }
    }
}
