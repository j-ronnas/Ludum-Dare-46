using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Projectile>() != null)
        {
            return;
        }
        if (collision.collider.GetComponent<Enemy>() != null)
        {
            collision.collider.GetComponent<Enemy>().Die();
            transform.parent = collision.transform;
        }
        
        
        GetComponent<Projectile>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<BoxCollider>().enabled = false;

    }
}
