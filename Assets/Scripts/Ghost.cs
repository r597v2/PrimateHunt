using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using TMPro;

public enum GhostType
{
    Ravenous, Wrathful, Hurt, Jester, Tortured, Maniac, Hellborn
}

public enum GhostEvidence
{
    Whispher, EatCandle, FootPrint, Humid, HandPrint, Blood
}

public class Ghost : MonoBehaviour
{
    public GhostType ghostType;
    public GhostEvidence[] ghostEvidence;

    private string ghostName;
    private string jesterGhostName;
    private string possibleJesterGhostName;

    public static GhostEvidence[][] ghostTypeEvidenceArray = new GhostEvidence[][]
    {
        new GhostEvidence[] {GhostEvidence.FootPrint, GhostEvidence.Humid, GhostEvidence.EatCandle}, //Ravenous evidence
        new GhostEvidence[] {GhostEvidence.Whispher, GhostEvidence.Humid, GhostEvidence.EatCandle}, //Wrathful evidence
        new GhostEvidence[] {GhostEvidence.Whispher, GhostEvidence.HandPrint, GhostEvidence.Blood}, //Hurt evidence
        new GhostEvidence[] {GhostEvidence.FootPrint}, //Jester evidence
        new GhostEvidence[] {GhostEvidence.Whispher, GhostEvidence.Humid, GhostEvidence.Blood}, //Tortured evidence
        new GhostEvidence[] {GhostEvidence.EatCandle, GhostEvidence.Blood, GhostEvidence.FootPrint}, //Maniac evidence
        new GhostEvidence[] {GhostEvidence.HandPrint, GhostEvidence.Blood, GhostEvidence.FootPrint} //Hellborn evidence
    };

    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        float ghostWallWrite = UnityEngine.Random.Range(-0.1f, 2500f);
        if (ghostWallWrite <= 0)
        {
            WriteGhostName();
            Debug.Log("write ghost name called");
        }
    }

    public void PickGhostType()
    {
        int ghostIndex = UnityEngine.Random.Range(0, Enum.GetNames(typeof(GhostType)).Length);
        // int ghostIndex = GlobalGameSettings.RandomGenerator.Next(0, Enum.GetNames(typeof(GhostType)).Length);
        ghostType = (GhostType)(ghostIndex);
        Debug.Log("Ghost is: " + ghostType);

        ghostEvidence = ghostTypeEvidenceArray[ghostIndex];
        PickGhostName();
    }

    public void PickGhostName()
    {
        if (ghostType != GhostType.Jester)
        {
            string[] firstNames = new string[] { "Michael", "Lisa", "Brandon", "Madison", "Peter", "Wilma", "Waldo", "Ruby", "Dee" };
            string[] lastNames = new string[] {"Westcott", "Smith", "Balls", "Thompson", "Cockburn", "Cumstock", "Williams",
                "Gibson", "Snutz"};

            string ghostFirstName = firstNames[UnityEngine.Random.Range(0, firstNames.Length)];
            string ghostLastName = lastNames[UnityEngine.Random.Range(0, lastNames.Length)];

            Debug.Log("Ghost name is: " + ghostFirstName + " " + ghostLastName);

            ghostName = ghostFirstName + " " + ghostLastName;
        }
        else
        {
            string[] jesterFirstNames = new string[] { "Michael", "Lisa", "Brandon", "Madison", "Peter", "Wilma", "Waldo", "Ruby", "Dee" };
            string[] jesterLastNames = new string[] {"Westcott", "Smith", "Balls", "Thompson", "Cockburn", "Cumstock", "Williams",
                "Gibson", "Snutz"};

            string ghostFirstName1 = jesterFirstNames[UnityEngine.Random.Range(0, jesterFirstNames.Length)];
            string ghostLastName1 = jesterLastNames[UnityEngine.Random.Range(0, jesterLastNames.Length)];

            string ghostFirstName2 = jesterFirstNames[UnityEngine.Random.Range(0, jesterFirstNames.Length)];
            string ghostLastName2 = jesterLastNames[UnityEngine.Random.Range(0, jesterLastNames.Length)];

            while (ghostFirstName2 == ghostFirstName1 && ghostLastName2 == ghostLastName1)
            {
                ghostFirstName2 = jesterFirstNames[UnityEngine.Random.Range(0, jesterFirstNames.Length)];
                ghostLastName2 = jesterLastNames[UnityEngine.Random.Range(0, jesterLastNames.Length)];
            }

            Debug.Log("Ghost name is: " + ghostFirstName1 + " " + ghostLastName1);
            Debug.Log("Jester name is: " + ghostFirstName2 + " " + ghostLastName2);

            ghostName = ghostFirstName1 + " " + ghostLastName1;
            jesterGhostName = ghostFirstName2 + " " + ghostLastName2;
        }
    }

    private void WriteGhostName()
    {
        if (ghostType != GhostType.Jester)
        {
            Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 4f);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.CompareTag("WallWritingSpot"))
                {
                    collider.gameObject.GetComponent<TMP_Text>().text = ghostName;
                    collider.gameObject.GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(
                        collider.gameObject.GetComponent<AudioSource>().pitch - 0.75f,
                        collider.gameObject.GetComponent<AudioSource>().pitch + 0.75f);
                    collider.gameObject.GetComponent<AudioSource>().Play();
                    Debug.Log("Wrote Name on wall");
                }
            }
        }
        else
        {
            Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 4f);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.CompareTag("WallWritingSpot"))
                {
                    int randomNumber = (int) UnityEngine.Random.Range(1,3);
                    if (randomNumber == 1)
                    {
                        possibleJesterGhostName = ghostName;
                    }
                    else if (randomNumber == 2)
                    {
                        possibleJesterGhostName = jesterGhostName;
                    }

                    collider.gameObject.GetComponent<TMP_Text>().text = possibleJesterGhostName;
                    collider.gameObject.GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(
                        collider.gameObject.GetComponent<AudioSource>().pitch - 0.75f,
                        collider.gameObject.GetComponent<AudioSource>().pitch + 0.75f);
                    collider.gameObject.GetComponent<AudioSource>().Play();
                    Debug.Log("Wrote Name on wall");
                }
            }
        }
    }

    public static bool HasEvidence(GhostType gt, GhostEvidence ge)
    {
        return Array.Exists<GhostEvidence>(ghostTypeEvidenceArray[(int)gt], element => element == ge);
    }
}
