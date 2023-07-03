using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Device;

public class SpawnManager : MonoBehaviour
{
    public ChapterScriptable chapterScript;
    public Transform[] spawnPoints;
    public bool canSpawn = true;
    public bool isPause = false;
    float nextSpawnTime;
    int numberEnemy;
    int timeSpawn;
    public static SpawnManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        SetLevel();
    }
    void Update()
    {
        isPause = false;
        UIManager.Instance.screenText.text = "Màn " + (GameManager.Instance.level + 1) + "/" + chapterScript.chapters[GameManager.Instance.chapter].levels.Length;
        if (GameManager.Instance.level >= chapterScript.chapters[GameManager.Instance.chapter].levels.Length && !Player.Instance.isEnd)
        {
            GameManager.Instance.ChapterNext();
        }
        if (Player.Instance.isStart && GameManager.Instance.isGameOver == false)
        {
            SpawnEnemy();
            GameObject[] totalEnemy = GameObject.FindGameObjectsWithTag("Enemy");
            if (totalEnemy.Length == 0 && !canSpawn)
            {
                isPause = true;
                GameManager.Instance.ScreenNext();
            }
        }
    }
    public void SetLevel()
    {
        if (GameManager.Instance.level < chapterScript.chapters[GameManager.Instance.chapter].levels.Length)
        {
            numberEnemy = chapterScript.chapters[GameManager.Instance.chapter].levels[GameManager.Instance.level].numberEnemy;
            timeSpawn = chapterScript.chapters[GameManager.Instance.chapter].levels[GameManager.Instance.level].timeSpawn;
            UIManager.Instance.slider.maxValue = chapterScript.chapters[GameManager.Instance.chapter].levels[GameManager.Instance.level].numberEnemy;
        }
    }
    void SpawnEnemy()
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {
            GameObject[] enemyPrefabs = chapterScript.chapters[GameManager.Instance.chapter].levels[GameManager.Instance.level].enemyPrefabs;
            GameObject randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            numberEnemy--;
            nextSpawnTime = Time.time + timeSpawn;
            if (numberEnemy <= 0)
            {
                canSpawn = false;
            }
        }
    }
    public void SpawnPowerup(Vector3 pos)
    {
        if (Random.Range(1, 4) == 1)
        {
            int randomPowerup = Random.Range(0, chapterScript.powerupPrefabs.Length);
            Instantiate(chapterScript.powerupPrefabs[randomPowerup], pos, chapterScript.powerupPrefabs[randomPowerup].transform.rotation);
        }
    }
}
