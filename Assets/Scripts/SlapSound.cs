using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlapSound : MonoBehaviour
{
    public GameObject hand;
    public PlayerMovement playerMvmt;
    private bool slapStart;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && playerMvmt.currentAnimController == "default")
        {
            if (playerMvmt.isSprinting)
            {
                return;
            }
            slapStart = true;
            playerMvmt.enabled = false;
            Invoke("EndSlap", 1f);
        }
    }

    private void EndSlap()
    {
        playerMvmt.enabled = true;
        slapStart = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (slapStart && (collision.tag == "Player" || collision.tag == "Head"))
        {
            slapStart = false;
            collision.gameObject.GetComponent<PlayerMovement>().health -= 5f;
            hand.GetComponent<AudioSource>().Play();
        }
    }
}