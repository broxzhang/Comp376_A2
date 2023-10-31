using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float winrate = 0.0f;

    public int NpcIndex = 2;

    public string NpcSelected;

    public bool old_man_talked = false;
    public bool plice_chief_talked = false;
    public bool mail_man_talked = false;
    public bool female_butler_talked = false;
    public bool young_doctor_talked = false;

    public bool positiveEvidenceOne = false;
    public bool positiveEvidenceTwo = false;
    public bool positiveEvidenceThree = false;

    public bool mutralEvidenceOne = false;

    public bool mutralEvidenceTwo = false;

    public bool mutralEvidenceThree = false;

    public bool negativeEvidenceOne = false;

    public bool negativeEvidenceTwo = false;

    public bool negativeEvidenceThree = false;

    public bool isChiefBuffed = false;

    public bool isDoctorBuffed = false;

    public bool isChiefNerfed = false;

    public bool isDoctorNerfed = false;

    public bool isWin = false;
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