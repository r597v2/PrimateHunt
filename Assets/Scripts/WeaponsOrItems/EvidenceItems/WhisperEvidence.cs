using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhisperEvidence : MonoBehaviour
{
    public AudioClip[] whispersList;
    public AudioSource aSource;
    public AudioSource aSource1;
    public LayerMask playerLM;

    private int randomModifier;
    private float nextTime;
    private Ghost ghost;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        ghost = FindObjectOfType<Ghost>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Ghost.HasEvidence(ghost.ghostType, GhostEvidence.Whispher) && Physics.CheckSphere(gameObject.transform.position, 
        2.5f, playerLM))
        {
            randomModifier = (int) Random.Range(0,8250);
            if ((Time.time - startTime > 60f) && randomModifier == 0)
            {
                Whisper();
            }
        }
    }

    private void Whisper()
    {
        aSource.clip = whispersList[(int) Random.Range(0, whispersList.Length)];
        aSource1.clip = whispersList[(int) Random.Range(0, whispersList.Length)];
        aSource.Play();
        aSource1.Play();
    }
}
