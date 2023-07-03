using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SkillOption : MonoBehaviour
{
    Button button;
    public int skill;
    public int numberSkill;
    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
        button = GetComponent<Button>();
        button.onClick.AddListener(options);
    }
    void options()
    {
        GameObject[] btnGroup = GameObject.FindGameObjectsWithTag("btnGroup");
        for (int i = 0; i < btnGroup.Length; i++)
        {
            for (int j = 0; j < btnGroup[i].transform.childCount; j++)
            {

                if (btnGroup[i].transform.GetChild(j).gameObject.activeInHierarchy && btnGroup[i].transform.GetChild(j).GetComponent<SkillOption>().skill != skill)
                {
                    btnGroup[i].transform.GetChild(j).gameObject.SetActive(false);
                }
            }
        }
        transform.DOMove(new Vector3(550, 960, 0), 0.5f).SetEase(Ease.InQuad);
        transform.DOScale(new Vector3(2.5f, 2.5f, 2.5f), 1f)
        .OnComplete(() =>
        {
            UIManager.Instance.screenNext.SetActive(false);
            transform.DOScale(new Vector3(0, 0, 0), 0.5f);
            transform.DOMove(Camera.main.WorldToScreenPoint(Player.Instance.transform.position), 0.5f).SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                if (skill == 1)
                {
                    UIManager.Instance.skill1++;
                    UIManager.Instance.SetIndex(0);
                    Player.Instance.GetComponent<HealthBar>().updateHealth(3);
                }
                else if (skill == 2)
                {
                    UIManager.Instance.skill2++;
                    UIManager.Instance.SetIndex(1);
                    for (int i = 1; i < Player.Instance.BodyParts.Count; i++)
                    {
                        if (!Player.Instance.BodyParts[i].CompareTag("BodySkill"))
                        {
                            Vector3 posBody = Player.Instance.BodyParts[i].transform.position;
                            GameObject body = Instantiate(GameManager.Instance.bodyPrefab, new Vector3(posBody.x, 0.74f, posBody.z), Quaternion.identity);
                            Player.Instance.BodyParts.Insert(i, body);
                            Destroy(Player.Instance.BodyParts[i + 1]);
                            Player.Instance.BodyParts.RemoveAt(i + 1);
                            break;
                        }
                    }
                }
                else if (skill == 3)
                {
                    UIManager.Instance.skill3++;
                    UIManager.Instance.SetIndex(2);
                    GameManager.Instance.addToAtk += 1;
                }
                transform.position = startPos;
                transform.localScale = new Vector3(1, 1, 1);
                for (int i = 0; i < btnGroup.Length; i++)
                {
                    for (int j = 0; j < btnGroup[i].transform.childCount; j++)
                    {
                        if (btnGroup[i].transform.GetChild(j).gameObject.activeInHierarchy)
                        {
                            btnGroup[i].transform.GetChild(j).gameObject.SetActive(false);
                        }
                    }
                }
                Player.Instance.isEnd = true;

            });

        });

    }

    void Update()
    {

    }
}
