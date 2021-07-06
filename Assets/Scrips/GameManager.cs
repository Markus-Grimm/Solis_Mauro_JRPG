using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;


public class GameManager : MonoBehaviour
{


    //Interfaz, derrota y victoria
    public Text gamedefeat;
    public Text gamevictory;
    public Text Damage;
    public Text Defence;
    public bool lose;
    public int pj_health;

    //Movimiento y camara
    public GameObject player;
    public new Camera camera;
    public bool follow;


    public Player pj;
    public static int pj_hp;
    public HealthBar healthBar;
    public Movement movement;

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject enemy5;

    public GameObject enemyBar1;
    public GameObject enemyBar2;
    public GameObject enemyBar3;
    public GameObject enemyBar4;
    public GameObject enemyBar5;

    public int cant_enemy;

    Vector3 pj_pos;
    public new GameObject gameObject;

    void Start()
    {
        
        
    }

    void Update()
    {
        // Seguir al jugador
        if (follow)
        {
            camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, camera.transform.position.z);
        }


    }


    // Texto Game Over y regreso al Menú
    public void Defeat()
    {
        gamedefeat.text = "Game Over";
        StartCoroutine(Defeatcrono(6f));
    }
    public IEnumerator Defeatcrono(float valcrono)
    {
        yield return new WaitForSeconds(valcrono);
        SceneManager.LoadScene("Main Menu");
    }

    // Cargar Escena de batalla
    public void LoadBattle(int cant_enemy)
    {
        //StartCoroutine(BattleAsyncScene(cant_enemy));   Prueba
        cant_enemy = Random.Range(1, 5);
        Debug.Log(cant_enemy);

        switch (cant_enemy)
        {
            case 1:
                enemy1.SetActive(true);
                enemy2.SetActive(false);
                enemy3.SetActive(false);
                enemy4.SetActive(false);
                enemy5.SetActive(false);
                enemyBar1.SetActive(true);
                enemyBar2.SetActive(false);
                enemyBar3.SetActive(false);
                enemyBar4.SetActive(false);
                enemyBar5.SetActive(false);
                break;
            case 2:
                enemy1.SetActive(true);
                enemy2.SetActive(true);
                enemy3.SetActive(false);
                enemy4.SetActive(false);
                enemy5.SetActive(false);
                enemyBar1.SetActive(true);
                enemyBar2.SetActive(true);
                enemyBar3.SetActive(false);
                enemyBar4.SetActive(false);
                enemyBar5.SetActive(false);
                break;
            case 3:
                enemy1.SetActive(true);
                enemy2.SetActive(true);
                enemy3.SetActive(true);
                enemy4.SetActive(false);
                enemy5.SetActive(false);
                enemyBar1.SetActive(true);
                enemyBar2.SetActive(true);
                enemyBar3.SetActive(true);
                enemyBar4.SetActive(false);
                enemyBar5.SetActive(false);
                break;
            case 4:
                enemy1.SetActive(true);
                enemy2.SetActive(true);
                enemy3.SetActive(true);
                enemy4.SetActive(true);
                enemy5.SetActive(false);
                enemyBar1.SetActive(true);
                enemyBar2.SetActive(true);
                enemyBar3.SetActive(true);
                enemyBar4.SetActive(true);
                enemyBar5.SetActive(false);
                break;
            case 5:
                enemy1.SetActive(true);
                enemy2.SetActive(true);
                enemy3.SetActive(true);
                enemy4.SetActive(true);
                enemy5.SetActive(true);
                enemyBar1.SetActive(true);
                enemyBar2.SetActive(true);
                enemyBar3.SetActive(true);
                enemyBar4.SetActive(true);
                enemyBar5.SetActive(true);
                break;
            default:
                break;
        }

        follow = false;

        gameObject.SetActive(true);
        player.GetComponent<Movement>().enabled = false;
        pj_pos.Set(player.transform.position.x, player.transform.position.y, player.transform.position.z);

        player.transform.position = new Vector3(3.5f,0.5f,0);
        camera.orthographicSize = 6f;
        camera.transform.position = new Vector3(0, 0, -10);
        Debug.Log("wea");
    }

    /*
    IEnumerator BattleAsyncScene(int cant_enemy)
    {
        
        follow = false;
        player.SetActive(false);
        camera.transform.Translate(new Vector3(0, 0, -10));
        Debug.Log("wea");


        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Battle",LoadSceneMode.Additive);
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }
     Prueba */

    //Guardar dato player

    public void SaveHealth(int x)
    {
        pj_hp = x;
        healthBar.SetHealth(pj_hp);
    }


}
