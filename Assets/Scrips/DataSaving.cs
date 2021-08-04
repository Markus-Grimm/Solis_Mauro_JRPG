using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaving : MonoBehaviour
{
    public int pj_life;
    public int pj_attack;
    public int pj_armor;

    
    public int currentHealth;


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }


    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
