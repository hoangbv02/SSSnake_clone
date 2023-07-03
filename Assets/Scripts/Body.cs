using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Body : MonoBehaviour
{
    public GameObject rocketPrefab;
    private GameObject tmpRocket;
    Transform point;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnRocket());
    }
    public IEnumerator spawnRocket()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            LauchRocket();
        }
    }
    void LauchRocket()
    {
        GameObject enemy = GameObject.FindWithTag("Enemy");
        if (enemy && !enemy.GetComponent<Enemy>().isOne && Vector3.Distance(Player.Instance.transform.position, enemy.transform.position) < 30f)
        {
            if (gameObject.CompareTag("Body"))
            {
                point = transform.GetChild(0).GetChild(4).GetChild(0);
            }
            if (gameObject.CompareTag("BodySkill"))
            {
                point = transform.GetChild(0).GetChild(0).GetChild(1).GetChild(3);
            }
            tmpRocket = ObjectPool.Instance.GetPooledObject(rocketPrefab.tag);
            tmpRocket.transform.position = point.position;
            tmpRocket.SetActive(true);
            tmpRocket.GetComponent<Rocket>().Fire(enemy.transform.position);
        }
    }
    private void Update()
    {
        if (UIManager.Instance.isPlay && gameObject.GetComponent<HingeJoint>() != null)
        {
            Destroy(gameObject.GetComponent<HingeJoint>());
        }
    }
}
