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
        if (Input.GetKeyDown(KeyCode.F) && playerMvmt.currentAnimController == "default")
        {
            headbuttStart = true;
            Invoke("EndHeadbutt", 5f);
        }
    }

    private void EndHeadbutt()
    {
        headbuttStart = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
       if (headbuttStart && collision.tag == "Player" || collision.tag == "Head")
        {
            headbuttStart = false;
            playerMvmt.health -= 4f;
            head.GetComponent<AudioSource>().Play();
        }
    }
}