using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Stats enemy
    public int enemy_life = 100;
    public int enemy_attack;
    public int enemy_armor;
    public HealthBar healthBar;
    public int currentHealth;


    // Perseguir al jugador en un rango
    public float visionRadius;
    public float speed;
    GameObject player;
    Vector3 initialPosition;

    // Barra de vida
    public GameManager GameManager;
    public int maxHealth = 100;


    /*Vector3 nextPosition;
    float x, y;*/


    void Start()
    {
        // Perseguir al jugador en un rango
        player = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position;
        visionRadius = 8f;

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
        InvokeRepeating("FollowPlayer",1.0f,1f);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(10);
        }
    }

    // Perseguir al jugador en un rango
    void FollowPlayer()
    {
        Vector3 target;

        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist < visionRadius) target = player.transform.position;
        else target = initialPosition;

        float fixedSpeed = speed * Time.deltaTime;

        /*x = Mathf.Round(transform.position.x) + 0.5f;
        y = Mathf.Round(transform.position.y) + 0.5f;

        nextPosition.Set(x, y, transform.position.z);*/

        transform.position = Vector3.MoveTowards(transform.position, target, fixedSpeed);
               
        
        //Debug.DrawLine(transform.position, target, Color.green);
    }

    // Cambio de escena al colisionar con el jugador
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Debug.Log("Encuentro");
            speed = 0f;
            Destroy(gameObject);            
            GameManager.LoadBattle(Random.Range(1,5));
            
        }
    }

    // Barra de vida
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        GameManager.SaveHealth(currentHealth);
        Debug.Log("enemy dmg");
    }
}
