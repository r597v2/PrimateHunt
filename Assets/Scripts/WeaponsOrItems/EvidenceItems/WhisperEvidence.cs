using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhisperEvidence : MonoBehaviour
{
    public AudioClip[] whispersList;
    public AudioSource aSource;
    public AudioSource aSource1;
    public LayerMask playerLM;

    private float randomModifier;
    private float nextTime;
    private Ghost ghost;

    // Start is called before the first frame update
    void Start()
    {
        ghost = FindObjectOfType<Ghost>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Ghost.HasEvidence(ghost.ghostType, GhostEvidence.Whispher) && Physics.CheckSphere(gameObject.transform.position, 
        2.5f, playerLM))
        {
            randomModifier = Random.Range(-0.1f, 150);
            nextTime = Time.time + randomModifier;
            if (Time.time > nextTime)
            {
                Whisper();
            }
        }
    }

    private void Whisper()
    {
        int randomWhisper = Random.Range(0, whispersList.Length);
        int randomWhisper1 = Random.Range(0, whispersList.Length);
        aSource.clip = whispersList[randomWhisper];
        aSource1.clip = whispersList[randomWhisper1];
        aSource.Play();
        aSource1.Play();
    }
}
