using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Instance)
            transform.position = Player.Instance.transform.position + new Vector3(0, 28, -2);
    }
}
