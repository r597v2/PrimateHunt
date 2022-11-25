using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlapSound : MonoBehaviour
{
    public GameObject hand;
    private bool slapStart;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            slapStart = true;
            Invoke("EndSlap", 5f);
        }
    }

    private void EndSlap()
    {
        slapStart = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
       if (slapStart && (collision.tag == "Player" || collision.tag == "Head"))
        {
            slapStart = false;
            hand.GetComponent<AudioSource>().Play();
        }
    }
}