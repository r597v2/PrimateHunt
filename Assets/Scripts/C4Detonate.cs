using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4Detonate : MonoBehaviour
{
    public C4PickUpItem c4PickUpItem;
    public Rigidbody rb;
    public ParticleSystem explosionParticles;
    public ParticleSystem explosionParticles1;
    public ParticleSystem smokeParticles;
    public ParticleSystem smokeParticles1;
    public GameObject bombLight;
    public GameObject explosionRadius;

    private float timer;
    private float waitingTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (c4PickUpItem.armed)
        {
            if (timer > waitingTime)
            {
                bombLight.SetActive(true);
                timer = 0;
            }
            else
            {
                bombLight.SetActive(false);
            }
        }

        if (c4PickUpItem.armed && Input.GetMouseButtonDown(1))
        {
            DetonateC4();
        }
    }

    private void DetonateC4()
    {
        explosionRadius.SetActive(true);
        explosionParticles.Play();
        explosionParticles1.Play();
        smokeParticles.Play();
        smokeParticles1.Play();
        gameObject.GetComponent<AudioSource>().Play();
        Invoke("DestroyC4", .75f);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 8 && c4PickUpItem.armed)
        {
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void DestroyC4()
    {
        Destroy(gameObject);
    }
}
