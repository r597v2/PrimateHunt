using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActivateClick : MonoBehaviour
{
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            audioSource.Play();
            Debug.Log("sound played from ItemActivateClick");
        }
    }
}
