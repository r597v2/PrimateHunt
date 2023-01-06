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
    Whispher, EatCandle, Spit, Humid, Laugh, Blood
}

public class Ghost : MonoBehaviour
{
    private GhostType ghostType;
    private GhostEvidence[] ghostEvidence;

    public static GhostEvidence[][] ghostTypeEvidenceArray = new GhostEvidence[][]
    {
        new GhostEvidence[] {GhostEvidence.Spit, GhostEvidence.Humid, GhostEvidence.EatCandle},
        new GhostEvidence[] {GhostEvidence.Whispher, GhostEvidence.Humid, GhostEvidence.EatCandle},
        new GhostEvidence[] {GhostEvidence.Whispher, GhostEvidence.EatCandle, GhostEvidence.Blood},
        new GhostEvidence[] {GhostEvidence.Laugh},
        new GhostEvidence[] {GhostEvidence.Whispher, GhostEvidence.Humid, GhostEvidence.Blood},
        new GhostEvidence[] {GhostEvidence.EatCandle, GhostEvidence.Laugh, GhostEvidence.Spit}
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
}
