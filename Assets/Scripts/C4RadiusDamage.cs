using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4RadiusDamage : MonoBehaviour
{
    public GameObject explosionRadius;
    public GameObject vaultDoor;
    public bool c4Detonating;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (c4Detonating)
        {
            DamageInRadius();
        }
    }

    void DamageInRadius()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(gameObject.transform.position, 11f);
        foreach (Collider collider in objectsInRange)
        {
            if (collider.tag == "Player")
            {
                Debug.Log("Player damaged by c4");
            }
            else if (collider.tag == "VaultDoor")
            {
                collider.GetComponent<Animator>().SetTrigger("TrOpenVault");
            }
        }
    }
}
