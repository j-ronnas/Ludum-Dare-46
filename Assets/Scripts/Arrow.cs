using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody rb;
    float startAngle;
    float t = -1; 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (t >= 0 && t<1)
        {
            t += Time.deltaTime/10;
            float xAngle = Mathf.Lerp(startAngle, 90, t);
            
            rb.MoveRotation(Quaternion.Euler(xAngle, 0, 0));
            rb.velocity = transform.forward * 10f;
        }
    }

    public void Fire()
    {
        rb.isKinematic = false;
        startAngle = transform.rotation.eulerAngles.x;
        startAngle -= startAngle > 90 ? 360 : 0;
        Debug.Log(startAngle);
        Debug.Log(rb.velocity);
        Debug.Log(transform.forward);
        t = 0;


        transform.parent = null;

    }
}
