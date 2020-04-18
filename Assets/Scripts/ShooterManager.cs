using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    BowController[] bowControllers;
    int currentBow;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bowControllers[currentBow].enabled = false;
            currentBow = (currentBow + 1)%bowControllers.Length;
            bowControllers[currentBow].enabled = true;
        }
    }
}
