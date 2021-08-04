using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerInside : MonoBehaviour
{
    bool canJump;

    public Text gamedefeat;

    void Start()
    {
        
    }


    void Update()
    {
        // Movimiento del jugador
        if ((Input.GetKey("left")) || (Input.GetKey(KeyCode.A)))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1000f * Time.deltaTime, 0));
        }
        if ((Input.GetKey("right")) || (Input.GetKey(KeyCode.D)))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1000f * Time.deltaTime, 0));
        }
        if ((Input.GetKeyDown("up") && canJump) /*|| (Input.GetKey(KeyCode.W) && canJump)*/ || (Input.GetKey(KeyCode.Space) && canJump))
        {
            canJump = false;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300f));
        }
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Colision suelo
        if (collision.transform.tag == "ground")
        {
            canJump = true;
        }
    }



    public void Dead()
    {
        GameObject.Destroy(this.gameObject);
        gamedefeat.text = "Game Over";
        StartCoroutine(Defeatcrono(2f));
    }

    public IEnumerator Defeatcrono(float valcrono)
    {
        yield return new WaitForSeconds(valcrono);
        SceneManager.LoadScene("Main Menu");
    }

}
