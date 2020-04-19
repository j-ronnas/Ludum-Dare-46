using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeHealth : MonoBehaviour
{

    int health;

    [SerializeField]
    GameObject activeBarricade;
    [SerializeField]
    GameObject destroyedBarricade;

    public bool active;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        health -= 1;
        if(health == 0)
        {
            DisableBaricade();
        }
    }

    void DisableBaricade()
    {
        activeBarricade.SetActive(false);
        destroyedBarricade.SetActive(true);

        active = false;
    }

    public void EnableBarricade()
    {
        activeBarricade.SetActive(true);
        destroyedBarricade.SetActive(false);
        active = true;
        health = 5;
    }
}
