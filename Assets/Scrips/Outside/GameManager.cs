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
    public int pj_health;
    public int dado;
    public Text dado_txt;
    public Text dado_txt1;
    public int healing;
    public Text Healing;
    public Text State;
    public Button btn_Attack;
    public bool atk_state = false;    
    public Button btn_Defend;
    public bool dfd_state = false;
    public Button btn_Heal;
    public GameObject info;


    //Movimiento y camara
    public GameObject player;
    public new Camera camera;
    public bool follow;


    public Player pj;
    public static int pj_hp;
    public HealthBar healthBar;
    public Movement movement;


    public int cant_enemy;

    Vector3 pj_pos;
    public new GameObject gameObject;

    public GameObject enemyprefab;
    public GameObject[] enemy_mov;
    public GameObject[] enemy;
    public GameObject[] enemies;

    public bool player_turn;
    public int enemies_current;
    public bool end_battle;

    public DataSaving DataSaving;

    void Start()
    {
        healing = 1;
        end_battle = true;
        player_turn = true;


        enemy = GameObject.FindGameObjectsWithTag("EnemyPos");
        enemy_mov = GameObject.FindGameObjectsWithTag("Enemy_Mov");

    }

    public IEnumerator EnemyAtack(float valcrono)
    {
        // Eliminar enemigos muertos

        for (int i = 0; i < cant_enemy; i++)
        {
            if (enemies[i].activeSelf && enemies[i] != null && enemies[i].transform.position != new Vector3(enemies[i].transform.position.x, enemies[i].transform.position.y, -20f))
            {
                if (enemies[i].GetComponent<Enemy>().currentHealth <= 0)
                {
                    enemies[i].transform.position = new Vector3(enemies[i].transform.position.x, enemies[i].transform.position.y, -20f);
                    enemies_current--;
                }
                else
                {
                    //Debug.Log("wea");
                }
            }
            else
            {
                i++;
            }
            
        }

        // Atacan enemigos
        if (enemies_current > 0)
        {
            new WaitForSeconds(valcrono);
            for (int i = 0; i <= enemies_current - 1; i++)
            {
                if (enemies[i].activeSelf)
                {
                    State.text = "Enemy attack...";
                    dado = Random.Range(1, 20);
                    switch (dado)
                    {
                        case 1:
                            pj.GetComponent<Player>().TakeDamage(enemies[i].GetComponent<Enemy>().enemy_attack * 0);
                            State.text = "DMG: " + enemies[i].GetComponent<Enemy>().enemy_attack * 0;
                            dado_txt.text = "1";
                            dado_txt1.text = "Pifia";
                            break;

                        case 20:
                            pj.GetComponent<Player>().TakeDamage(enemies[i].GetComponent<Enemy>().enemy_attack * 2);
                            State.text = "DMG: " + enemies[i].GetComponent<Enemy>().enemy_attack * 2;
                            dado_txt.text = "20";
                            dado_txt1.text = "Crític";
                            break;

                        default:
                            if (dado < 10)
                            {
                                pj.GetComponent<Player>().TakeDamage(Mathf.RoundToInt(enemies[i].GetComponent<Enemy>().enemy_attack / 2));
                                State.text = "DMG: " + Mathf.RoundToInt(enemies[i].GetComponent<Enemy>().enemy_attack / 2);
                                dado_txt.text = dado.ToString();
                                dado_txt1.text = "Regular";
                            }
                            else
                            {
                                pj.GetComponent<Player>().TakeDamage(enemies[i].GetComponent<Enemy>().enemy_attack);
                                State.text = "DMG: " + enemies[i].GetComponent<Enemy>().enemy_attack;
                                dado_txt.text = dado.ToString();
                                dado_txt1.text = "Good";
                            }
                            break;
                    }                    
                    Debug.Log(dado);
                    yield return new WaitForSecondsRealtime(valcrono);
                }
                else { i = i+1; }
            }
            State.text = "Player turn...";
            player_turn = true;
        }

    }


    void Update()
    {
        
        if (player != null)
        {
            if (player.transform.rotation.z != 0)
            {
                player.transform.Rotate(new Vector3(0, 0, 0));
            }
        }
                    
        // Seguir al jugador
        if (follow && player != null)
        {
            camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, camera.transform.position.z);
        }


        // Descargar Escena batalla
        if (enemies_current == 0 && end_battle == false)
        {
            UnloadBattle();
        }

        //Game over
        if (player.GetComponent<Player>().currentHealth <= 0)
        {
            player.SetActive(false);
            Defeat();
        }

        if (!player_turn)
        {
            btn_Attack.gameObject.SetActive(false);
            btn_Defend.gameObject.SetActive(false);
            btn_Heal.gameObject.SetActive(false);
        }
        else
        {
            btn_Attack.gameObject.SetActive(true);
            btn_Defend.gameObject.SetActive(true);
            btn_Heal.gameObject.SetActive(true);
        }


        if (Input.GetKey(KeyCode.F) && end_battle)
        {
            if (healing >= 1)
            {
                if (pj.currentHealth != 100)
                {
                    healing--;
                    Healing.text = "Healing: " + healing;
                    pj.HealingPlayer(20);
                    pj.info.text = "Life healed...";
                }
                else
                {
                    pj.info.text = "Life fulled...";
                    StartCoroutine(HealWaitOC(3f));
                }
            }
            else
            {
                pj.info.text = "No healing available..."; 
                StartCoroutine(HealWaitOC(3f));
            }
        }
        

    }

    // Descargar Escena batalla
    public void UnloadBattle()
    {
        camera.orthographicSize = 8f;
        follow = true;
        end_battle = true;
        dfd_state = false;
        info.SetActive(true);

        dado_txt.text = "";
        dado_txt1.text = "";
        gameObject.SetActive(false);
        player.GetComponent<Movement>().enabled = true;
        player.transform.position = pj_pos;
        camera.orthographicSize = 8f;
        camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, camera.transform.position.z);

        for (int i = 0; i < cant_enemy; i++)
        {
            Destroy(enemies[i]);            
        }

        // Prueba desactivar otros enemigos en movimiento
        for (int i = 0; i < enemy_mov.Length; i++)
        {
            if (enemy_mov[i] == null)
            {
                i++;
            }
            else
            {
                enemy_mov[i].SetActive(true);                
            }            
        }       
    }

    // Cargar Escena de batalla
    public void LoadBattle()
    {
        camera.orthographicSize = 6f;
        //StartCoroutine(BattleAsyncScene(cant_enemy));   Prueba
        State.text = "Comienza el combate";
        end_battle = false;
        cant_enemy = Random.Range(1, 5);
        enemies_current = cant_enemy;
        player_turn = true;
        info.SetActive(false);


        // Activar enemigos
        for (int i = 0; i < enemies_current; i++)
        {                        
            Instantiate(enemyprefab, new Vector3(enemy[i].transform.position.x, enemy[i].transform.position.y, enemy[i].transform.position.z), Quaternion.identity);            
        }
                
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        follow = false;
        
        gameObject.SetActive(true);
        player.GetComponent<Movement>().enabled = false;
        pj_pos.Set(player.transform.position.x, player.transform.position.y, player.transform.position.z);

        player.transform.position = new Vector3(-0.5f, -2.5f, 0);
        camera.orthographicSize = 6f;
        camera.transform.position = new Vector3(0, 0, -10);

        if (player.transform.rotation.z != 0)
        {
            player.transform.Rotate(0,0,0);
        }

        // Prueba desactivar otros enemigos en movimiento
        for (int i = 0; i < enemy_mov.Length; i++)
        {
            if (enemy_mov[i] == null)
            {
                i++;
            }
            else
            {
                enemy_mov[i].SetActive(false);
            }
        }
    }

    // Texto Game Over y regreso al Menú
    public void Defeat()
    {
        gamedefeat.text = "Game Over";
        StartCoroutine(Defeatcrono(2f));
    }
    public IEnumerator Defeatcrono(float valcrono)
    {
        yield return new WaitForSeconds(valcrono);
        SceneManager.LoadScene("Main Menu");
    }

    //Guardar dato player
    public void SaveHealth(int x)
    {
        pj_hp = x;
        healthBar.SetHealth(pj_hp);
    }


    public void Attack()
    {
        dfd_state = false;
        atk_state = true;
        State.text = "Click the target...";        
    }

    public void Heal()
    {
        dfd_state = false;
        if (pj.currentHealth != 100)
        {
            if (healing >= 1)
            {                
                healing--;
                Healing.text = "Healing: " + healing;
                State.text = "Life healed...";
                pj.HealingPlayer(20);
                player_turn = false;
                // Enemigo ataca
                StartCoroutine(HealWait(1.5f));
            }

            State.text = "No healing available...";
        }
        else
        {
            State.text = "Life fulled...";
        }
    }

    public void Defend()
    {
        if (!dfd_state)
        {
            pj.pj_armor = pj.pj_armor * 2;
            dfd_state = true;
        }
        Defence.text = "Def: " + pj.pj_armor;
        State.text = "Increased defense...";
        player_turn = false;

        // Enemigo ataca
        StartCoroutine(DefendWait(1.5f));
    }

    public IEnumerator HealWait(float valcrono)
    {
        yield return new WaitForSecondsRealtime(valcrono);
        // Enemigo ataca
        StartCoroutine(EnemyAtack(1));
    }

    public IEnumerator DefendWait(float valcrono)
    {
        yield return new WaitForSecondsRealtime(valcrono);
        // Enemigo ataca
        StartCoroutine(EnemyAtack(1));
    }

    public IEnumerator HealWaitOC(float valcrono)
    {        
        yield return new WaitForSeconds(valcrono);
        pj.info.text = " ";
    }



}
