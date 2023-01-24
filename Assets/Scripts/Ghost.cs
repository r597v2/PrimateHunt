using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public enum GhostType
{
    Ravenous, Wrathful, Hurt, Jester, Tortured, Maniac
}

public enum GhostEvidence
{
    Whispher, EatCandle, Spit, Humid, HandPrint, Blood
}

public class Ghost : MonoBehaviour
{
    public GhostType ghostType;
    public GhostEvidence[] ghostEvidence;

    public static GhostEvidence[][] ghostTypeEvidenceArray = new GhostEvidence[][]
    {
        new GhostEvidence[] {GhostEvidence.Spit, GhostEvidence.Humid, GhostEvidence.EatCandle}, //Ravenous evidence
        new GhostEvidence[] {GhostEvidence.Whispher, GhostEvidence.Humid, GhostEvidence.EatCandle}, //Wrathful evidence
        new GhostEvidence[] {GhostEvidence.Whispher, GhostEvidence.HandPrint, GhostEvidence.Blood}, //Hurt evidence
        new GhostEvidence[] {GhostEvidence.Spit}, //Jester evidence
        new GhostEvidence[] {GhostEvidence.Whispher, GhostEvidence.Humid, GhostEvidence.Blood}, //Tortured evidence
        new GhostEvidence[] {GhostEvidence.EatCandle, GhostEvidence.Blood, GhostEvidence.Spit} //Maniac evidence
    };

    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
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
        string[] firstNames = new string[] { "Michael", "Lisa", "Brandon", "Madison", "Peter", "Wilma", "Waldo", "Ruby", "Dee" };
        string[] lastNames = new string[] {"Westcott", "Smith", "Balls", "Thompson", "Cockburn", "Cumstock", "Williams",
        "Gibson", "Snutz"};

        string ghostFirstName = firstNames[UnityEngine.Random.Range(0, firstNames.Length)];
        string ghostLastName = lastNames[UnityEngine.Random.Range(0, lastNames.Length)];

        Debug.Log("Ghost name is: " + ghostFirstName + " " + ghostLastName);
    }

    public static bool HasEvidence(GhostType gt, GhostEvidence ge)
    {
        return Array.Exists<GhostEvidence>(ghostTypeEvidenceArray[(int)gt], element => element == ge);
    }
}
