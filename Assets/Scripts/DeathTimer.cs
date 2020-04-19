using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTimer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float time;
    float t;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if(t> time)
        {
            Destroy(gameObject);
        }
    }
}
