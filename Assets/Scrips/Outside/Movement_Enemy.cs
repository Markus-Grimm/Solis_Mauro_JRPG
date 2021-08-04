using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum Direction1
{
    up, down, rigth, left
}

public class Movement_Enemy : MonoBehaviour
{

    Animator anim;
    Vector2 targetPosition;    

    //public float speed = 1.5f;
    public LayerMask obstacles;
    public LayerMask enemy;

    public int posx;
    public int posy;

    // Perseguir al jugador en un rango
    public float visionRadius;
    public float speed;
    GameObject player;
    Vector3 initialPosition;

    // Barra de vida
    public GameManager GameManager;
    public int maxHealth = 70;


    private void Start()
    {
        anim = GetComponent<Animator>();
        // Movimiento en cuadricula
        targetPosition = transform.position;
        
        // Perseguir al jugador en un rango
        player = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position;
        visionRadius = 6f;
        speed = 0.1f;
    }


    private void Update()
    {
        // Perseguir al jugador en un rango
        InvokeRepeating("FollowPlayer", 3.5f, 1.5f);              
    }


    // Cambio de escena al colisionar con el jugador
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {            
            speed = 0f;
            Destroy(gameObject);
            GameManager.LoadBattle();

        }
    }



    void FollowPlayer()
    {
        Vector3 target;

        if (player != null)
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);
            if (dist < visionRadius) target = player.transform.position;
            else target = initialPosition;

            speed = 0.1f;
            float fixedSpeed = speed * Time.deltaTime;

            /*x = Mathf.Round(transform.position.x) + 0.5f;
            y = Mathf.Round(transform.position.y) + 0.5f;

            nextPosition.Set(x, y, transform.position.z);*/

            transform.position = Vector3.MoveTowards(transform.position, target, fixedSpeed);


            //Debug.DrawLine(transform.position, target, Color.green);
        }

    }
    
}