/*using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Screen
{
    public GameObject[] enemyPrefabs;
    public int numberEnemy;
    public float timeSpawn;
}
public class WaveSpawner : MonoBehaviour
{
    public Screen[] screens;
    public Transform[] spawnPoints;
    public GameObject[] powerupPrefabs;
    public TextMeshProUGUI screenText;

    Screen currentScreen;
    float nextSpawnTime;
    public bool canSpawn = true;
    public bool onetime = false;
    public bool isPause = false;
    public static WaveSpawner Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }else if(Instance == this)
        {
            Destroy(gameObject);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.updateScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        isPause = false;
        screenText.text = "Màn " + (GameManager.Instance.level + 1) + "/" + screens.Length;
        //GameManager.Instance.screenText1.text = "Màn " + (GameManager.Instance.level + 1);
        if (Player.Instance.isStart && GameManager.Instance.isGameOver == false)
        {
            if (GameManager.Instance.level < screens.Length)
            {
                currentScreen = screens[GameManager.Instance.level];
            }
            if (!onetime)
            {
                GameManager.Instance.slider.maxValue = currentScreen.numberEnemy;
                onetime = true;
            }
            SpawnEnemy();
            GameObject[] totalEnemy = GameObject.FindGameObjectsWithTag("Enemy");
            if (totalEnemy.Length == 0 && !canSpawn)
            {
                isPause = true;
                GameManager.Instance.ScreenNext();
            }
        }
    }

    void SpawnEnemy()
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = currentScreen.enemyPrefabs[Random.Range(0, currentScreen.enemyPrefabs.Length)];
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            currentScreen.numberEnemy--;
            nextSpawnTime = Time.time + currentScreen.timeSpawn;
            if (currentScreen.numberEnemy == 0)
            {
                canSpawn = false;
            }
        }
    }
    public void SpawnPowerup(Vector3 pos)
    {
        if (Random.Range(1, 5) == 1)
        {
            int randomPowerup = Random.Range(0, powerupPrefabs.Length);
            Instantiate(powerupPrefabs[randomPowerup], pos, powerupPrefabs[randomPowerup].transform.rotation);
        }
    }
}
*/