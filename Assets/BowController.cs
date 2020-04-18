using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject gameObjectAim;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            Debug.Log(hitInfo.collider.name);
            gameObjectAim.transform.position = hitInfo.point;
            if (hitInfo.point.z > transform.position.z)
            {
                transform.LookAt(hitInfo.point);
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
            }
        }


    }
}
