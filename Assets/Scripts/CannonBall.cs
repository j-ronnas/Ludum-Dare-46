using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float range;

    [SerializeField]
    GameObject explosionEffect;
    [SerializeField]
    GameObject soundEffect;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log(collision.collider);
        if (collision.collider.GetComponent<Projectile>() != null)
        {
            return;
        }

        foreach (Collider coll in Physics.OverlapSphere(transform.position, range))
        {
            if (coll.GetComponent<Enemy>() != null)
            {
                coll.GetComponent<Enemy>().Die();
            }
        }

        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Instantiate(soundEffect);

        Destroy(gameObject);

    }
}
