using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Arrow : MonoBehaviour
{
    Camera cam;
    Vector3 target;
    void Start()
    {
        cam = Camera.main;
    }
    public void SetTarget(Vector3 newTarget)
    {
        target = cam.WorldToScreenPoint(newTarget) - cam.WorldToScreenPoint(transform.position);
    }
    void Update()
    {
        if (target != null)
        {
            transform.up = new Vector3(target.x, target.y, transform.position.z);
        }

    }
}
