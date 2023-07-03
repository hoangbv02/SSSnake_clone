using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (enemy && GameManager.Instance.isGameOver == false && Vector3.Distance(Player.Instance.transform.position, enemy.transform.position) < 40f)
        {
            Vector3 vector = enemy.transform.position - transform.position;
            transform.forward = Vector3.MoveTowards(transform.forward, vector, speed * Time.deltaTime);
        }
        else if(Player.Instance)
        {
            Vector3 vector = Player.Instance.transform.position - transform.position;
            transform.forward = Vector3.MoveTowards(transform.forward, vector, speed * Time.deltaTime);
        }
    }
}
