using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    bool dead = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            transform.position -= transform.forward * Time.deltaTime;
        }
    }

    public void Die()
    {
        dead = true;
        transform.rotation = Quaternion.Euler(0, 0, 90);
    }
}
