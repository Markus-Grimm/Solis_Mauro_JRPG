using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    // Stats enemy
    public int enemy_life = 70;
    public int enemy_attack;
    public int enemy_armor;
    public HealthBar healthBar;
    public int currentHealth = 70;

    // Barra de vida
    public GameObject GameController;
    public GameManager GameManager;
    public int maxHealth = 70;


    public GameObject player;
    public int dado;
    public GameObject dado1;
    public GameObject dado2;
    public Text dado_txt;
    public Text dado_txt1;


    void Start()
    {
        enemy_attack = 10;
        enemy_armor = 8;
        
        player = GameObject.FindGameObjectWithTag("Player");
        GameController = GameObject.FindGameObjectWithTag("GameController");
        GameManager = GameController.GetComponent<GameManager>();

        dado1 = GameObject.FindGameObjectWithTag("Dado1");
        dado_txt = dado1.GetComponent<Text>();
        dado2 = GameObject.FindGameObjectWithTag("Dado2");
        dado_txt1 = dado2.GetComponent<Text>();


        // Barra de vida
        healthBar.SetMaxHealth(maxHealth);
        if (currentHealth == 0)
        {
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
        }
    }

    void Update()
    {
        
    }

    public void OnMouseDown()
    {        
        if (GameManager.atk_state)
        {            
            GameManager.State.text = "Enemy attacking...";
            GameManager.atk_state = false;
            GameManager.player_turn = false;            

            StartCoroutine(Atack(1.5f));
        }
    }

    public IEnumerator Atack(float valcrono)
    {
        dado = Random.Range(1, 20);
        switch (dado)
        {
            case 1:
                TakeDamage(player.GetComponent<Player>().pj_attack * 0);
                GameManager.State.text = "DMG: " + player.GetComponent<Player>().pj_attack * 0;
                dado_txt.text = "1";
                dado_txt1.text = "Blunder";
                break;

            case 20:
                TakeDamage(player.GetComponent<Player>().pj_attack * 2);
                GameManager.State.text = "DMG: " + player.GetComponent<Player>().pj_attack * 2;
                dado_txt.text = "20";
                dado_txt1.text = "Critical";
                break;

            default:
                if (dado < 10)
                {
                    TakeDamage(Mathf.RoundToInt(player.GetComponent<Player>().pj_attack / 2));
                    GameManager.State.text = "DMG: " + player.GetComponent<Player>().pj_attack / 2;
                    dado_txt.text = dado.ToString();
                    dado_txt1.text = "Regular";
                }
                else
                {
                    TakeDamage(player.GetComponent<Player>().pj_attack);
                    GameManager.State.text = "DMG: " + player.GetComponent<Player>().pj_attack;
                    dado_txt.text = dado.ToString();
                    dado_txt1.text = "Good";
                }
                break;
        }
        Debug.Log("Dado player" + dado);
        yield return new WaitForSecondsRealtime(valcrono);

        // Enemigo ataca
        StartCoroutine(GameManager.EnemyAtack(1));
    }

    // Barra de vida
    public void TakeDamage(int damage)
    {
        if (damage - enemy_armor > 0)
        {
            currentHealth -= damage - enemy_armor;
        }
        
        healthBar.SetHealth(currentHealth);         
    }
}
