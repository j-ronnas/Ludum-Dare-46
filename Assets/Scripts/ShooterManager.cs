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
        if (Input.GetKeyDown(KeyCode.Tab) && !Input.GetMouseButton(0))
        {
            bowControllers[currentBow].enabled = false;
            currentBow = (currentBow + 1)%bowControllers.Length;
            bowControllers[currentBow].enabled = true;
        }
    }

    public void Pause()
    {
        for (int i = 0; i < bowControllers.Length; i++)
        {
            bowControllers[i].enabled = false;
        }
    }

    public void UnPause()
    {
        bowControllers[currentBow].enabled = true;
    }
}
