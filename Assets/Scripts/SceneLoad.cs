using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour
{
    public Slider slider;
    public GameObject scene;
    public TextMeshProUGUI loadingText;
    /*public float time, secrond;
    public GameObject loadScene;
    public Slider slider;
    public TextMeshProUGUI loadingText;
    public bool isLoading ;
    public bool isCkeck;
    Player player;*/
    // Start is called before the first frame update
    /*   public void Start()
       {
           secrond = 5;
           player = FindAnyObjectByType<Player>();
           Invoke("LoadGame", 5f);

       }
       public void test()
       {
           time = 0;
           isCkeck = false;
           if (time < 5)
           {
               isCkeck = true;
               time += Time.deltaTime;
               loadingText.text = Mathf.Round((time / secrond) * 100) + "%";
               slider.value = time / secrond;
           }
           if (isCkeck)
           {
               loadScene.SetActive(true);
           }
           else
           {
               loadScene.SetActive(false);
           }
           Debug.Log(isCkeck);
       }
       private void Update()
       {
          *//* if (isLoading)
           {
               time = 0;
               loadScene.SetActive(true);
               Invoke("LoadGame", 5f);

           }*//*
           isLoading = false;
           if (time < 5)
           {
               isLoading = true;
               time += Time.deltaTime;
               loadingText.text = Mathf.Round((time / secrond) * 100) + "%";
               slider.value = time / secrond;
           }
       }*/

    /*    public void LoadGame()
        {
            player.isNext = true;
            loadScene.SetActive(false);
        }
    */
    //}
    /*private void Start()
    {
        LoadScene(0);
    }*/
    public void LoadScene(int index)
    {
        StartCoroutine(LoadScene_Coroutine(index));
    }
    // Update is called once per frame
    public IEnumerator LoadScene_Coroutine(int index)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
        scene.SetActive(true);
        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            slider.value = progress;
            loadingText.text = Mathf.Round(progress * 100) + "%";
            yield return null;
        }
    }
}
