using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBarScript : MonoBehaviour {

    Image healthBar;
    float maxHealth = 100.0f;
    public static float health;

	// Use this for initialization
	void Start () {
        healthBar = GetComponent<Image>();
        health = 0.75f * maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        healthBar.fillAmount = health / maxHealth;
        if (health <= 0)
        {
            SceneManager.LoadScene(4);
        }
	}

    void updateHealth(int change)
    {
        health += change;
        if (health > maxHealth)
        {
            health = maxHealth;
        } else if (health < 0)
        {
            health = 0.0f;
        }

        float result = health / maxHealth;

        if (result >= 0.75f)
        {
            healthBar.color = new Color(0f, 255f, 0f);
        } else if (result >= 0.25f && result < 0.75f)
        {
            healthBar.color = new Color(255f, 237f, 0f);
        } else
        {
            healthBar.color = new Color(255f, 0f, 0f);
        }
    }
}
