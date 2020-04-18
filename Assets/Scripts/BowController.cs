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
            Debug.Log(hitInfo.collider.name);
            gameObjectAim.transform.position = hitInfo.point;
            if (hitInfo.point.z > transform.position.z)
            {
                transform.LookAt(hitInfo.point);
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
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
        readyBow.SetActive(true);
        unReadyBow.SetActive(false);

        currentArrow = Instantiate(arrowPrefab, transform.position, transform.rotation, this.transform);
    }

    void ReleaseBow()
    {
        readyBow.SetActive(false);
        unReadyBow.SetActive(true);

        currentArrow.GetComponent<Arrow>().Fire();
    }
}
