using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    Vector2 minBounds;
    [SerializeField]
    Vector2 maxBounds;

    Quaternion baseAngle;
    // Start is called before the first frame update
    void Start()
    {
        baseAngle = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        float xPos = Mathf.InverseLerp(minBounds.x, maxBounds.x, target.position.x);
        float yPos = Mathf.InverseLerp(minBounds.y, maxBounds.y, target.position.y);

        Quaternion lookAngle = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Lerp(baseAngle, lookAngle, xPos);
    }
}
