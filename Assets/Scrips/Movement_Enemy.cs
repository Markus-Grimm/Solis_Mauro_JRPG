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
    Direction1 direction;

    //public float speed = 1.5f;
    public LayerMask obstacles;
    public LayerMask enemy;

    public int posx;
    public int posy;

    // Perseguir al jugador en un rango
    public float visionRadius;
    public float spd;
    GameObject player;
    Vector3 initialPosition;

    private void Start()
    {
        anim = GetComponent<Animator>();
        // Movimiento en cuadricula
        targetPosition = transform.position;
        direction = Direction1.down;

        // Perseguir al jugador en un rango
        player = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position;
        visionRadius = 8f;
        spd = 0.1f;
    }


    private void Update()
    {
        // Perseguir al jugador en un rango
        InvokeRepeating("FollowPlayer", 3.0f, 1f);

        // Movimiento en cuadricula

        if (this.transform.position.x - player.transform.position.x > 0)
        {
            posx = -1;
        }
        else
        {
            posx = 1;
        }

        if (this.transform.position.y - player.transform.position.y > 0)
        {
            posy = -1;
        }
        else
        {
            posy = 1;
        }

        
    }

    // Movimiento en cuadricula
    bool CheckCollision
    {
        get
        {
            RaycastHit2D rh;


            Vector2 dir = Vector2.zero;
            if (direction == Direction1.down) dir = Vector2.down;
            if (direction == Direction1.up) dir = Vector2.up;
            if (direction == Direction1.left) dir = Vector2.left;
            if (direction == Direction1.rigth) dir = Vector2.right;



            rh = Physics2D.Raycast(transform.position, dir, 1, obstacles);

            return rh.collider != null;
        }
    }


    
    void FollowPlayer()
    {
        // Movimiento en cuadricula
        Vector2 axisDirection = new Vector2(this.transform.position.x + posx, this.transform.position.y + posy);
        if (axisDirection != Vector2.zero && targetPosition == (Vector2)transform.position)
        {
            if (Mathf.Abs(axisDirection.x) > Mathf.Abs(axisDirection.y))
            {
                if (axisDirection.x > 0)
                {
                    direction = Direction1.rigth;
                    if (!CheckCollision) targetPosition += Vector2.right;
                }
                else
                {
                    direction = Direction1.left;
                    if (!CheckCollision) targetPosition -= Vector2.right;
                }
            }
            else
            {
                if (axisDirection.y > 0)
                {
                    direction = Direction1.up;
                    if (!CheckCollision) targetPosition += Vector2.up;
                }
                else
                {
                    direction = Direction1.down;
                    if (!CheckCollision) targetPosition -= Vector2.up;
                }
            }
        }

        

        // Perseguir al jugador en un rango

        Vector3 target = initialPosition;

        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist < visionRadius) target = player.transform.position;

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, spd * Time.deltaTime);
        //float fixedSpeed = spd * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, target, fixedSpeed);

    }




}