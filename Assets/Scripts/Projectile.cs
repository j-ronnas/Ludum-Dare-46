using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody rb;

    [SerializeField]
    float speed;
    
    float gravity = 10f;
    public bool active = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!active)
        {
            return;
        }

        rb.velocity = new Vector3(0, rb.velocity.y - gravity * Time.deltaTime, rb.velocity.z);
        rb.rotation = Quaternion.LookRotation(rb.velocity);
    }

    public void Fire(float force)
    {
        rb = GetComponent<Rigidbody>();

        active = true;
        rb.isKinematic = false;
        rb.velocity = transform.forward * speed*force;
        transform.parent = null;

    }


    
}
