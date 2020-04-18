using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody rb;

    float gravity = 5;

    bool active;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (active)
        {
            rb.velocity = new Vector3(0, rb.velocity.y - gravity * Time.deltaTime, rb.velocity.z);
            rb.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    public void Fire()
    {
        rb.isKinematic = false;
        rb.velocity = transform.forward * 10f;
        transform.parent = null;

        active = true;

    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.GetComponent<Arrow>() != null)
        {
            return;
        }
        if(collision.collider.GetComponent<Enemy>() != null)
        {
            collision.collider.GetComponent<Enemy>().Die();
        }
        active = false;
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        GetComponent<BoxCollider>().enabled = false;

    }
}
