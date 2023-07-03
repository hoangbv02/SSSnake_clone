using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int score = 0;
    public bool isGameOver;
    public bool isGameActive;
    public int addToAtk = 0;
    public GameObject bodyPrefab;

    public int chapter = 0;
    public int level = 0;

    public bool onetime = false;

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        PlayerPrefs.SetInt("chapter", 0);
        chapter = PlayerPrefs.GetInt("chapter");
        isGameActive = false;
        isGameOver = false;
    }
    public float getScore()
    {
        return score;
    }
    public void ScreenNext()
    {
        if (!onetime)
        {
            var sequence = DOTween.Sequence();
            GameObject[] powerups = GameObject.FindGameObjectsWithTag("Powerup");
            GameObject[] powerupHealths = GameObject.FindGameObjectsWithTag("PowerupHealth");
            foreach (GameObject powerup in powerups)
            {
                sequence.Join(powerup.transform.DOMove(Player.Instance.transform.position, 1).SetEase(Ease.InBack));
            }
            foreach (GameObject powerupHealth in powerupHealths)
            {
                sequence.Join(powerupHealth.transform.DOMove(Player.Instance.transform.position, 1).SetEase(Ease.InBack));
            }
            sequence.OnComplete(() =>
            {
                if (level < SpawnManager.Instance.chapterScript.chapters[chapter].levels.Length)
                {
                    UIManager.Instance.SetScreenNext();
                }
                if (level >= SpawnManager.Instance.chapterScript.chapters[chapter].levels.Length)
                {
                    Player.Instance.isEnd = true;
                    UIManager.Instance.slider.value = 0;
                }
            });
            onetime = true;
        }
    }
    public void ChapterNext()
    {
        if (chapter < SpawnManager.Instance.chapterScript.chapters.Length)
        {
            chapter++;
            PlayerPrefs.SetInt("chapter", chapter);
            level = 0;
            SpawnManager.Instance.SetLevel();
            UIManager.Instance.UpdateScreen();
        }
    }
    public void updateScore(int addScore)
    {
        score += addScore;
        UIManager.Instance.SetScoreText(score);
        PlayerPrefs.SetInt("inScore", score);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                         Application.Quit();
#endif
    }

    public void RestartGame()
    {
        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("inScore") + PlayerPrefs.GetInt("score"));
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
