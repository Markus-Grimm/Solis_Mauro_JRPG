using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum Direction
{
    up, down, rigth, left
}

public class Movement : MonoBehaviour
{

    Animator anim;
    Vector2 targetPosition;
    Direction direction;

    public float speed = 1.5f;
    public LayerMask obstacles;
    public LayerMask enemy;

    private void Start()
    {
        anim = GetComponent<Animator>();
        targetPosition = transform.position;
        direction = Direction.down;
    }


    private void Update()
    {
        Vector2 axisDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (axisDirection != Vector2.zero && targetPosition == (Vector2)transform.position)
        {
            if (Mathf.Abs(axisDirection.x) > Mathf.Abs(axisDirection.y))
            {
                if (axisDirection.x > 0)
                {
                    direction = Direction.rigth;
                    if (!CheckCollision) { targetPosition += Vector2.right; }
                    else
                    {

                    }

                }
                else
                {
                    direction = Direction.left;
                    if (!CheckCollision) { targetPosition -= Vector2.right; }
                    else
                    {

                    }
                }
            }
            else
            {
                if (axisDirection.y > 0)
                {
                    direction = Direction.up;
                    if (!CheckCollision) { targetPosition += Vector2.up; }
                    else
                    {

                    }
                }
                else
                {
                    direction = Direction.down;
                    if (!CheckCollision) { targetPosition -= Vector2.up; }
                    else
                    {

                    }
                }
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);


    }

    bool CheckCollision
    {
        get
        {            
            RaycastHit2D rh;


            Vector2 dir = Vector2.zero;
            if (direction == Direction.down) dir = Vector2.down;
            if (direction == Direction.up) dir = Vector2.up;
            if (direction == Direction.left) dir = Vector2.left;
            if (direction == Direction.rigth) dir = Vector2.right;



            rh = Physics2D.Raycast(transform.position, dir, 1, obstacles);

            return rh.collider != null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            
        }
    }
}
