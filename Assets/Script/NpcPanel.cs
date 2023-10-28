using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class NpcPanel : MonoBehaviour
{
    public Button[] npcButtons;

    public Button next_chapter;

    [SerializeField]
    private TextMeshProUGUI winRateText;

    public GameObject currentPanel;
    public GameObject seleactionPanel;

    float winRate = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Button btn in npcButtons)
        {
            btn.onClick.AddListener(() => OnButtonClick(btn));
        }

        this.winRate = GameManager.instance.winrate * 100.0f;
        UpdateWinRate(this.winRate);

    }

    // Update is called once per frame
    void Update()
    {
        
        foreach (Button btn in npcButtons) {
            if (btn.name == "old_man"){
                btn.gameObject.SetActive(!GameManager.instance.old_man_talked);
            }
            if (btn.name == "plice_chief"){
                btn.gameObject.SetActive(!GameManager.instance.plice_chief_talked);
            }
            if (btn.name == "mail_man") {
                btn.gameObject.SetActive(!GameManager.instance.mail_man_talked);
            }
            
            if (btn.name == "female_butler") {
                btn.gameObject.SetActive(!GameManager.instance.female_butler_talked);
            }
            
            if (btn.name == "young_doctor") {
                btn.gameObject.SetActive(!GameManager.instance.young_doctor_talked);
            }

            next_chapter.gameObject.SetActive(
                GameManager.instance.old_man_talked &&
                GameManager.instance.plice_chief_talked &&
                GameManager.instance.mail_man_talked &&
                GameManager.instance.female_butler_talked &&
                GameManager.instance.young_doctor_talked
            );
            
        }
    }

    void OnButtonClick(Button buttonClicked)
    {
        if(buttonClicked.name == "old_man") {
            GameManager.instance.NpcIndex = 1;
        } 
        if (buttonClicked.name == "plice_chief") {
            GameManager.instance.NpcIndex = 2;
        }
        if (buttonClicked.name == "mail_man") {
            GameManager.instance.NpcIndex = 3;
        }
        if (buttonClicked.name == "female_butler") {
            GameManager.instance.NpcIndex = 4;
        } 
        if (buttonClicked.name == "young_doctor"){
            GameManager.instance.NpcIndex = 5;
        }

        currentPanel.SetActive(false);
        seleactionPanel.SetActive(true);
    }

    public void UpdateWinRate(float winRate)
    {
        winRateText.text = "Wintate: " + winRate.ToString() + "%";
    }
}
