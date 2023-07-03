using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    int health;
    public int maxHealth;
    public TextMeshProUGUI healtText;
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        health = maxHealth;
        healtText.text = health + "/" + maxHealth;
    }

    // Update is called once per frame
    public int getHealth()
    {
        return health;
    }
    public int getMaxHealth()
    {
        return maxHealth;
    }

    public void updateHealth(int addHealth)
    {
        health += addHealth;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        slider.value = health;
        healtText.text = health + "/" + maxHealth;
    }
}
