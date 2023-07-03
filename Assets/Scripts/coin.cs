using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class coin : MonoBehaviour
{
    GameObject target;
    void Start()
    {
        target = GameObject.FindWithTag("coin");
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.Slerp(transform.position, target.transform.position, 3f * Time.deltaTime);
        }
    }
}
