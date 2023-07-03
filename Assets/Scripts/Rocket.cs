using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public int atk;
    public float speed = 35.0f;
    private Vector3 target;
    private void Start()
    {
    }
    public int GetAtk()
    {
        return atk + GameManager.Instance.addToAtk;
    }
    // Start is called before the first frame update
    public void Fire(Vector3 newTarget)
    {
        target = (newTarget - transform.position).normalized;
    }

    void Update()
    {
        if (target != null && GameManager.Instance.isGameOver == false)
        {
            transform.Translate(target * speed * Time.deltaTime);
        }

    }

}
