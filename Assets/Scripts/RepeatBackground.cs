using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class RepeatBackground : MonoBehaviour
{
    SceneLoad sceneLoad;
    float speed;
    float rotate = 0;
    float rotateSpeed = 50;
    public List<GameObject> BodyParts = new List<GameObject>();

    private void Start()
    {
        sceneLoad = FindAnyObjectByType<SceneLoad>();
        UIManager.Instance.SetScoreText(PlayerPrefs.GetInt("score"));
    }

    private void Update()
    {
        if (UIManager.Instance.isPlay)
        {
            speed = 20;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            UIManager.Instance.menu.SetActive(false);
            UIManager.Instance.score.SetActive(false);
            for (int i = BodyParts.Count - 1; i > 0; i--)
            {
                int current = i;
                int after = i - 1;
                if (BodyParts[current] != null && BodyParts[after] != null)
                {
                    Vector3 moveDir = new Vector3(BodyParts[after].transform.position.x, 0.74f, BodyParts[after].transform.position.z);
                    if (Vector3.Distance(BodyParts[current].transform.position, BodyParts[after].transform.position) > 1f)
                    {
                        BodyParts[current].transform.position = Vector3.MoveTowards(BodyParts[current].transform.position, moveDir, speed * Time.deltaTime);
                    }
                    Vector3 vector = moveDir - BodyParts[current].transform.position;
                    if (vector != Vector3.zero)
                    {
                        BodyParts[current].transform.forward = vector;
                    }
                }
            }
        }
        else
        {
            if (rotate < -30)
            {
                rotate = -30;
                rotateSpeed *= -1;
            }
            
            if(rotate > 30)
            {
                rotate = 30;
                rotateSpeed *= -1;
            }
            rotate += rotateSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, rotate, 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("top"))
        {
            sceneLoad.LoadScene(0);
        }
    }
}
