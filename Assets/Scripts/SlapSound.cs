using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlapSound : MonoBehaviour
{
    public GameObject hand;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.tag == "Player")
        {
            hand.GetComponent<AudioSource>().Play();
        }
    }
}