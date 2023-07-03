using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed;
    public ParticleSystem particleFire;
    public GameObject coinPrefab;
    public GameObject arrowPrefab;
    public ParticleSystem particleDie;
    Animator animator;
    Camera cam;
    Vector3 coinPos;
    GameObject canvas;
    GameObject arrow;
    Vector3 enemyScreenPoint;
    public bool isOne = false;
    void Start()
    {
        cam = Camera.main;
        canvas = GameObject.Find("CanvasGame");
        enemyScreenPoint = cam.WorldToScreenPoint(transform.position);
        animator = GetComponent<Animator>();
        arrow = Instantiate(arrowPrefab, new Vector3(enemyScreenPoint.x, 1850, 0), Quaternion.identity);
        arrow.transform.SetParent(canvas.transform);
    }

    void Update()
    {
        enemyScreenPoint = cam.WorldToScreenPoint(transform.position);
        if (arrow != null)
        {
            arrow.transform.position = new Vector3(enemyScreenPoint.x, 1850, 0);
            arrow.GetComponent<Arrow>().SetTarget(transform.position);
            Vector3 screenPoint = cam.WorldToViewportPoint(transform.position);
            bool onScreen = screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
            if (!onScreen)
            {
                arrow.SetActive(true);
            }
            else
            {
                arrow.SetActive(false);
            }
        }
        if (GameManager.Instance.isGameOver == false && !isOne)
        {
            transform.forward += Player.Instance.transform.position - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, Player.Instance.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rocket") || other.gameObject.CompareTag("RocketSkill"))
        {
            ParticleSystem ps = Instantiate(particleFire, transform.position, Quaternion.identity);
            ps.Play();
            other.gameObject.SetActive(false);
            if (SoundManager.Instance && SoundManager.Instance.isToggle)
            {
                SoundManager.Instance.AudioMusic.PlayOneShot(SoundManager.Instance.AudioRocket);
            }
            gameObject.GetComponent<HealthBar>().updateHealth(-other.GetComponent<Rocket>().GetAtk());
            if (gameObject.GetComponent<HealthBar>().getHealth() <= 0 && !isOne)
            {
                Destroy(GetComponent<BoxCollider>());
                transform.Find("CanvasHealth").gameObject.SetActive(false);
                if (animator)
                {
                    animator.SetBool("isDie", true);
                }
                gameObject.transform.DOMove(transform.position, 1).OnComplete(() =>
                {
                    ParticleSystem pDie = Instantiate(particleDie, transform.position, Quaternion.identity);
                    pDie.Play();
                    SpawnManager.Instance.SpawnPowerup(transform.position);
                    coinPos = cam.WorldToScreenPoint(transform.position);
                    GameObject coin = Instantiate(coinPrefab, coinPos, Quaternion.identity);
                    coin.transform.SetParent(canvas.transform);
                    coin.transform.DOScale(0.8f, 0.3f).SetEase(Ease.OutBack);
                    //coin.transform.DORotate(new Vector3(90, 0, 0), 1.5f).SetDelay(0.3f).SetEase(Ease.OutBack).Loops();
                    coin.transform.DOMove(GameObject.FindWithTag("coin").transform.position, 1.5f).SetDelay(0.3f).SetEase(Ease.InBack)
                    .OnComplete(() =>
                    {
                        Destroy(coin);
                    });
                    Destroy(gameObject);
                });
                speed = 0;
                Destroy(arrow);
                UIManager.Instance.UpdateSlider(1);
                GameManager.Instance.updateScore(5);
                isOne = true;
            }
        }

    }

}
