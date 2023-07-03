using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
public class Player : MonoBehaviour
{
    public float speed;
    public float space = 1.5f;
    public FixedJoystick joystick;
    public GameObject BodyPrefab;
    public List<GameObject> BodyParts = new List<GameObject>();

    public float xBoundary = 15f;
    public float topBoundary = 41f;
    public float bottomBoundary = -35f;
    public bool isStart = false;
    public bool isEnd;
    public bool isRun = true;

    public List<GameObject> wayPoint;
    int index = 0;
    public static Player Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        UIManager.Instance.SetScoreText(0);
        BodyParts.Add(gameObject);
        GrowSnake();
        StartCoroutine(Delay());
    }

    void Update()
    {
        if (isEnd)
        {
            speed = 25;
            transform.forward += wayPoint[index].transform.position - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, wayPoint[index].transform.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, wayPoint[index].transform.position) < 0.5f)
            {
                index = 1;
            }
        }
        if (isRun)
        {
            UIManager.Instance.screen.SetActive(true);
            speed = 20;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, transform.position.y, -20), speed * Time.deltaTime);
        }
        else
        {
            UIManager.Instance.screen.SetActive(false);
        }
        if (GameManager.Instance.isGameOver == false && isEnd == false && !SpawnManager.Instance.isPause)//
        {
            float vertical = joystick.Vertical;
            float horizontal = joystick.Horizontal;
            if (vertical != 0 && horizontal != 0 && !isRun)
            {
                isStart = true;
                speed = 8;
                Vector3 movement = new Vector3(horizontal, 0f, vertical);
                transform.Translate(movement.normalized * speed * Time.deltaTime, Space.World);
                transform.rotation = Quaternion.LookRotation(movement, Vector3.up);
            }
            else if (isStart && vertical == 0 && horizontal == 0 && !SpawnManager.Instance.isPause)//
            {
                speed = 3;
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }

            if (transform.position.x < -xBoundary)
            {
                transform.position = new Vector3(-xBoundary, transform.position.y, transform.position.z);
            }
            if (transform.position.x > xBoundary)
            {
                transform.position = new Vector3(xBoundary, transform.position.y, transform.position.z);
            }
            if (transform.position.z < bottomBoundary)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, bottomBoundary);
            }
            if (transform.position.z > topBoundary)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, topBoundary);
            }
        }

        for (int i = BodyParts.Count - 1; i > 0; i--)
        {
            int current = i;
            int after = i - 1;
            if (BodyParts[current] != null && BodyParts[after] != null)
            {
                Vector3 moveDir = new Vector3(BodyParts[after].transform.position.x, 0.74f, BodyParts[after].transform.position.z);
                if (Vector3.Distance(BodyParts[current].transform.position, BodyParts[after].transform.position) > space)
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

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        isRun = false;
    }
    public void GrowSnake()
    {
        Vector3 posBody = BodyParts[BodyParts.Count - 1].transform.position;
        GameObject body = Instantiate(BodyPrefab, new Vector3(posBody.x, 0.74f, posBody.z), Quaternion.identity);
        BodyParts.Add(body);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            GrowSnake();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("top"))
        {
            GameManager.Instance.level++;
            SpawnManager.Instance.SetLevel();
            ResetPosition(-36);
            index = 0;
            isEnd = false;
            isRun = true;
            isStart = false;
            UIManager.Instance.UpdateScreen();
            StartCoroutine(Delay());
            SpawnManager.Instance.canSpawn = true;
            GameManager.Instance.onetime = false;
        }
        if (other.gameObject.CompareTag("PowerupHealth"))
        {
            GetComponent<HealthBar>().updateHealth(5);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            GetComponent<HealthBar>().updateHealth(-1);
            UIManager.Instance.UpdateSlider(1);
            if (GetComponent<HealthBar>().getHealth() <= 0)
            {
                UIManager.Instance.GameOver();
            }
            Destroy(other.gameObject);
        }
    }
    public void ResetPosition(float posZ)
    {
        transform.position = new Vector3(0, transform.position.y, posZ);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        for (int i = 0; i < BodyParts.Count - 1; i++)
        {
            int current = i;
            int after = i + 1;
            if (BodyParts[current] != null && BodyParts[after] != null)
            {
                BodyParts[after].transform.position = new Vector3(BodyParts[current].transform.position.x, BodyParts[current].transform.position.y, BodyParts[current].transform.position.z - space);
            }
        }
    }
}
