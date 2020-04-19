using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

    [SerializeField]
    Text ammoText;

    public int ammo;

    GameObject currentArrow;


    float timer;
    float maxTime = 1.5f;

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

        if (Input.GetMouseButton(0) && timer <= maxTime)
        {
            timer += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            ReleaseBow();
        }

    }

    void ReadyBow()
    {
        if (ammo <= 0)
        {
            FindObjectOfType<Popup>().ShowMessage("No ammo!");
            return;
        }
        if (readyBow != null)
        {
            readyBow.SetActive(true);
            unReadyBow.SetActive(false);
        }

        currentArrow = Instantiate(arrowPrefab, transform.position, transform.rotation, this.transform);
        timer = 0;
    }

    void ReleaseBow()
    {
        if (ammo <= 0)
        {
            return;
        }
        if (readyBow != null)
        {
            readyBow.SetActive(false);
            unReadyBow.SetActive(true);
        }
        currentArrow.GetComponent<Projectile>().Fire(1);
        DecreaseAmmo();
    }


    public void IncreaseAmmo()
    {
        ammo++;
        ammoText.text = ammo.ToString();
    }
    public void DecreaseAmmo()
    {
        ammo--;
        ammoText.text = ammo.ToString();
    }
}
