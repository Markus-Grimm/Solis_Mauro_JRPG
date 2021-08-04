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
    public Text info;

    public bool door = false;


    public GameManager GameManager;

    void Start()
    {
        pj_attack = 40;
        pj_armor = 8;
        
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
        if (!GameManager.dfd_state)
        {
            pj_armor = 8;
            GameManager.Defence.text = "Def: " + pj_armor;
        }
    }

    public void TakeDamage(int damage)
    {
        if (damage - pj_armor > 0)
        {
            currentHealth -= damage - pj_armor;
        }
                
        healthBar.SetHealth(currentHealth);
        GameManager.SaveHealth(currentHealth);
    }

    public void HealingPlayer(int heal)
    {
        currentHealth += heal;

        healthBar.SetHealth(currentHealth);
        GameManager.SaveHealth(currentHealth);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Door")
        {
            info.text = "Press E to enter";
        }

        if (Input.GetKey(KeyCode.E))
        {
            SceneManager.LoadScene("Inside");
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Door")
        {
            info.text = " ";
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Healing")
        {
            GameManager.healing = +1;
        }
    }

}
