using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadbuttSound : MonoBehaviour
{
    public GameObject head;
    public PlayerMovement playerMvmt;
    private bool headbuttStart;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && playerMvmt.currentAnimController == "default")
        {
            if (playerMvmt.isSprinting)
            {
                return;
            }
            headbuttStart = true;
            playerMvmt.enabled = false;
            Invoke("EndHeadbutt", 1f);
        }
    }

    private void EndHeadbutt()
    {
        playerMvmt.enabled = true;
        headbuttStart = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (headbuttStart && (collision.tag == "Player" || collision.tag == "Head"))
        {
            headbuttStart = false;
            head.GetComponent<AudioSource>().Play();
        }
    }
}