using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    Vector3 startPosition;
    public GameObject startObject;
    public int number;
    // Start is called before the first frame update
    void Start()
    {
        if (startObject)
            startPosition = startObject.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (GameManager.Instance.isGameActive == false)
        {
            if (number == 1)
            {
                transform.Translate(Vector3.left * 3f * Time.deltaTime);
                if (transform.position.z < -90)
                {
                    transform.position = startPosition;
                }
            }
            else if (number == 2)
            {
                transform.Translate(Vector3.right * 3f * Time.deltaTime);
                if (transform.position.z < -90)
                {
                    transform.position = startPosition;
                }
            }
            else
            {
                transform.Translate(-Vector3.forward * 3f * Time.deltaTime);
                if (transform.position.z < -40)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, startPosition.z - 8);
                }

            }
        }


    }
}
