using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

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

    public GameObject player;
    private NavMeshAgent agent;
    [SerializeField] LayerMask groundLayer, playerLayer;
    private Vector3 destPoint;
    // bool walkPointValid;
    [SerializeField] float walkRange;
    [SerializeField] float longWalkRange;

    public GameObject[] ghostRooms = new GameObject[] { };
    private GameObject ghostRoom;
    private bool goingToRoom;
    // private bool reachedDestPoint = true;
    private bool longRoaming;
    // private bool roaming;
    public float roamCooldown = 1.75f;
    private float nextRoamTime = 0;

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
        agent = GetComponent<NavMeshAgent>();
        ghostRoom = ghostRooms[UnityEngine.Random.Range(0, ghostRooms.Length)];
        gameObject.transform.position = ghostRoom.transform.position;
        Debug.Log("Ghost room is: " + ghostRoom.name);
    }
    // Update is called once per frame
    void Update()
    {
        if ((int)UnityEngine.Random.Range(0, 10000f) == 0)
            WriteGhostName();

        NavMeshPath path = new NavMeshPath();

        if (goingToRoom && Vector3.Distance(transform.position, destPoint) <= 2.5f)
            goingToRoom = false;
        // roaming = false;
        if (path.status == NavMeshPathStatus.PathComplete || Vector3.Distance(transform.position, destPoint) <= 2.5f)
        {
            // reachedDestPoint = true;
            // roaming = false;
            longRoaming = false;
        }
        // else reachedDestPoint = false;

        if (!longRoaming && !goingToRoom &&  Time.time > nextRoamTime)
            Roam();
        else if (!goingToRoom &&  Time.time > nextRoamTime)
            LongRoam();
    }

    private void Roam()
    {
        if ((int)UnityEngine.Random.Range(0, 51) >= 2)
        {
            NavMeshPath path = new NavMeshPath();
            SearchForDest(walkRange);
            agent.SetDestination(destPoint);
            // if (Vector3.Distance(transform.position, destPoint) < 10) walkPointValid = false;
            nextRoamTime = Time.time + roamCooldown;
            if (path.status == NavMeshPathStatus.PathPartial)
                SearchForDest(walkRange);
            // roaming = true;
        }
    }
    private void LongRoam()
    {
        if ((int)UnityEngine.Random.Range(0, 51) == 0)
        {
            if ((int)UnityEngine.Random.Range(0, 21) != 0)
            {
                NavMeshPath path = new NavMeshPath();
                SearchForDest(longWalkRange);
                agent.SetDestination(destPoint);
                // if (Vector3.Distance(transform.position, destPoint) < 10) walkPointValid = false;
                nextRoamTime = Time.time + roamCooldown;
                if (path.status == NavMeshPathStatus.PathPartial)
                    SearchForDest(longWalkRange);
                longRoaming = true;
                // roaming = true;
            }
            else
            { //go to other ghost rooms
                nextRoamTime = Time.time + roamCooldown;
                agent.SetDestination(ghostRooms[UnityEngine.Random.Range(0, ghostRooms.Length)].transform.position);
                longRoaming = true;
                // roaming = true;
            }
        }
    }

    private void SearchForDest(float range)
    {
        // if (!roaming)
        // {
            if ((int)UnityEngine.Random.Range(0, 50) != 0)
            {
                float z = UnityEngine.Random.Range(-range, range);
                float x = UnityEngine.Random.Range(-range, range);

                destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
            }
            else
            {
                destPoint = ghostRoom.transform.position;
                goingToRoom = true;
                Debug.Log("Going to room");
            }
            // if (Physics.Raycast(destPoint, Vector3.down, groundLayer))
            // {
            //     walkPointValid = true;
            // }
        // }
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
                    int randomNumber = (int)UnityEngine.Random.Range(1, 3);
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
