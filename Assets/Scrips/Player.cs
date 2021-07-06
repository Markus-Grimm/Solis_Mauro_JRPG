using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public int pj_attack;
    public int pj_armor;

    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;
    public Text txt_attack;
    public Text txt_armor;


    public GameManager GameManager;

    void Start()
    {
        
        txt_armor.text = "Def: " + pj_armor;
        txt_attack.text = "Dmg: " + pj_attack;

        healthBar.SetMaxHealth(maxHealth);
        if (currentHealth == 0 || GameManager.pj_hp == 0)
        {
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
            GameManager.SaveHealth(currentHealth);
        }
        else
        {
            currentHealth = GameManager.pj_hp;
        }
        
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }

    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        GameManager.SaveHealth(currentHealth);
        Debug.Log("danio");
    }



}
