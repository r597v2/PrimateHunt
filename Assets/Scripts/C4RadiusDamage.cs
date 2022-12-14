using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4RadiusDamage : MonoBehaviour
{
    public GameObject explosionRadius;
    public GameObject vaultDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider otherCollision)
    {
        if (explosionRadius.activeInHierarchy)
        {
            if (otherCollision.tag == "Player")
            {
                Debug.Log("Player damaged by c4");
            }
            if (otherCollision.tag == "VaultDoor")
            {
                vaultDoor.GetComponent<Animator>().SetTrigger("TrOpenVault");
            }
        }
        
    }
}
