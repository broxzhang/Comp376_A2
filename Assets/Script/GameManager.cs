using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float winrate = 0.5f;

    public int NpcIndex = 1;

    public string NpcSelected;

    public bool old_man_talked = false;
    public bool plice_chief_talked = false;
    public bool mail_man_talked = false;
    public bool female_butler_talked = false;
    public bool young_doctor_talked = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }

    }
}