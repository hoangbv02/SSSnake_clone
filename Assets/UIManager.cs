using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int skill1 = 0;
    public int skill2 = 0;
    public int skill3 = 0;

    public bool isPlay;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameoverScoreText;
    public TextMeshProUGUI gameoverScreenText;
    public TextMeshProUGUI healtText;
    public TextMeshProUGUI screenText;
    public TextMeshProUGUI screenChapterText;
    public TextMeshProUGUI screenLevelText;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI pauseScoreText;
    public TextMeshProUGUI pauseBodyText;

    public GameObject menu;
    public GameObject gameOver;
    public GameObject setting;
    public GameObject screenNext;
    public GameObject screen;
    public GameObject pause;
    public GameObject inGame;
    public GameObject score;
    public Slider slider;


    public bool isOne = false;

    List<int> listIndex = new List<int>();
    bool isSkillActive;
    string text;

    public static UIManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void SetScoreText(int score)
    {
        scoreText.text = "" + score;
    }
    public void UpdateScreen()
    {
        screenChapterText.text = "Chương: " + (GameManager.Instance.chapter + 1);
        screenLevelText.text = "Màn: " + (GameManager.Instance.level + 1);
    }
    public void RandomSkill()
    {
        GameObject[] btnGroup = GameObject.FindGameObjectsWithTag("btnGroup");
        List<int> list = new List<int>();
        while (list.Count < 3)
        {
            int a = Random.Range(0, btnGroup[0].transform.childCount);
            if (!list.Contains(a))
            {
                list.Add(a);
            }
        }
        for (int i = 0; i < btnGroup.Length; i++)
        {
            for (int j = 0; j < btnGroup[i].transform.childCount; j++)
            {
                if (btnGroup[i].transform.GetChild(j).gameObject.activeInHierarchy)
                {
                    btnGroup[i].transform.GetChild(j).gameObject.SetActive(false);
                }
            }
            btnGroup[i].transform.GetChild(list[i]).gameObject.SetActive(true);
        }
    }
    public void SetScreenNext()
    {
        atkText.text = "" + GameManager.Instance.addToAtk;
        screenText.text = "Màn " + (GameManager.Instance.level + 1);
        screenNext.SetActive(true);
        HealthBar health = Player.Instance.GetComponent<HealthBar>();
        healtText.text = health.getHealth() + "/" + health.getMaxHealth();
        RandomSkill();
        slider.value = 0;
    }

    public void UpdateSlider(int addToSlider)
    {
        slider.value += addToSlider;
    }
    public void OnPause()
    {
        Time.timeScale = 0;
        pause.SetActive(true);
        pauseScoreText.text = "" + GameManager.Instance.getScore();
        pauseBodyText.text = "x" + (Player.Instance.BodyParts.Count - 1);
        ShowSkill();
    }
    public void OnSetting()
    {
        setting.SetActive(true);
    }
    public void OnClose()
    {
        setting.SetActive(false);
    }
    public void OnPlay()
    {
        isPlay = true;
    }
    public void ShowSkill()
    {
        GameObject[] listSkill = GameObject.FindGameObjectsWithTag("listSkill");
        for (int j = 0; j < listIndex.Count; j++)
        {
            if (listIndex[j] == 0)
            {
                text = skill1.ToString();
            }
            if (listIndex[j] == 1)
            {
                text = skill2.ToString();
            }
            if (listIndex[j] == 2)
            {
                text = skill3.ToString();
            }
            foreach (GameObject skill in listSkill)
            {
                for (int i = 0; i < skill.transform.childCount; i++)
                {
                    if (skill.transform.GetChild(i).gameObject.activeInHierarchy)
                    {
                        isSkillActive = false;
                        break;
                    }
                    else isSkillActive = true;
                }
                if (skill.transform.GetChild(listIndex[j]).gameObject.activeInHierarchy)
                {
                    TextMeshProUGUI skillText = skill.transform.GetChild(listIndex[j]).GetChild(1).GetComponent<TextMeshProUGUI>();
                    skillText.text = text;
                    break;
                }
                if (isSkillActive)
                {
                    skill.transform.GetChild(listIndex[j]).gameObject.SetActive(true);
                    TextMeshProUGUI skillText = skill.transform.GetChild(listIndex[j]).GetChild(1).GetComponent<TextMeshProUGUI>();
                    skillText.text = text;
                    break;
                }
            }
        }
    }
    public void SetIndex(int i)
    {
        if (listIndex.Contains(i) == false)
        {
            listIndex.Add(i);
        }
    }
    public void OnContinue()
    {
        pause.SetActive(false);
        Time.timeScale = 1;
    }
    public void GameOver()
    {
        inGame.gameObject.SetActive(false);
        GameManager.Instance.isGameOver = true;
        gameOver.SetActive(true);
        Image bg = gameOver.transform.GetChild(0).GetComponent<Image>();
        bg.DOFade(0.95f, 3).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            StartCoroutine(GameOverCou());
        });
        gameoverScoreText.text = "" + PlayerPrefs.GetInt("inScore");
    }
    IEnumerator GameOverCou()
    {
        for (int i = 1; i < gameOver.transform.childCount; i++)
        {
            gameOver.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.7f);
        }
    }
    void Start()
    {
    }
    void Update()
    {

    }
}
