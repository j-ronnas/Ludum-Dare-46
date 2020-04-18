using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject gameObjectAim;

    [SerializeField]
    GameObject readyBow;
    [SerializeField]
    GameObject unReadyBow;

    [SerializeField]
    GameObject arrowPrefab;

    [SerializeField]
    float minAngle;
    [SerializeField]
    float maxAngle;


    GameObject currentArrow;
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
            gameObjectAim.transform.position = hitInfo.point;
            if (hitInfo.point.z > transform.position.z)
            {
                transform.LookAt(hitInfo.point);
                float xAngle = transform.rotation.eulerAngles.x > 90 ? transform.rotation.eulerAngles.x - 360 : transform.rotation.eulerAngles.x;
                transform.rotation = Quaternion.Euler(Mathf.Clamp(xAngle, minAngle, maxAngle), 0, 0);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            ReadyBow();
        }

        if (Input.GetMouseButtonUp(0))
        {
            ReleaseBow();
        }

    }

    void ReadyBow()
    {
        if (readyBow != null)
        {
            readyBow.SetActive(true);
            unReadyBow.SetActive(false);
        }

        currentArrow = Instantiate(arrowPrefab, transform.position, transform.rotation, this.transform);
    }

    void ReleaseBow()
    {
        if (readyBow != null)
        {
            readyBow.SetActive(false);
            unReadyBow.SetActive(true);
        }
        currentArrow.GetComponent<Projectile>().Fire();
    }
}
