using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManegerInside : MonoBehaviour
{
    public GameObject player;
    public new Camera camera;

    public Text gamedefeat;
    public Text gamevictory;
    public Text Damage;
    public Text Defence;
    public int healing;
    public Text Healing;
    public Text State;
    public GameObject info;


    public HealthBar healthBar;


    void Start()
    {
        
    }

    
    void Update()
    {
        if (player != null)
        {
            camera.transform.position = new Vector3(player.transform.position.x, camera.transform.position.y, camera.transform.position.z);
        }

    }
}
