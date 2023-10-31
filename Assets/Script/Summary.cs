using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Summary : MonoBehaviour
{
    public TextMeshProUGUI winText;

    public TextMeshProUGUI EvidenceOneText;

    public TextMeshProUGUI EvidenceTwoText;

    public TextMeshProUGUI EvidenceThreeText;

    public TextMeshProUGUI AbilityOneText;

    public TextMeshProUGUI AbilityTwoText;

    public Button replayButton;


    public GameObject currentPanel;
    // Start is called before the first frame update
    void Start()
    {
        winText.text = GameManager.instance.isWin ? "You Win" : "You Lose";

        if (GameManager.instance.positiveEvidenceOne)
        {
            EvidenceOneText.text = "Positive";
        }
        else if (GameManager.instance.mutralEvidenceOne)
        {
            EvidenceOneText.text = "Mutral";
        }
        else if (GameManager.instance.negativeEvidenceOne)
        {
            EvidenceOneText.text = "Negative";
        }

        if (GameManager.instance.positiveEvidenceTwo)
        {
            EvidenceTwoText.text = "Positive";
        }
        else if (GameManager.instance.mutralEvidenceTwo)
        {
            EvidenceTwoText.text = "Mutral";
        }
        else if (GameManager.instance.negativeEvidenceTwo)
        {
            EvidenceTwoText.text = "Negative";
        }

        if (GameManager.instance.positiveEvidenceThree)
        {
            EvidenceThreeText.text = "Positive";
        }
        else if (GameManager.instance.mutralEvidenceThree)
        {
            EvidenceThreeText.text = "Mutral";
        }
        else if (GameManager.instance.negativeEvidenceThree)
        {
            EvidenceThreeText.text = "Negative";
        }

        if (GameManager.instance.isChiefBuffed)
        {
            AbilityOneText.text = "Buffed";
        }
        else if (GameManager.instance.isChiefNerfed)
        {
            AbilityOneText.text = "Nerfed";
        } else {
            AbilityOneText.text = "NOT GET";
        }

        if (GameManager.instance.isDoctorBuffed)
        {
            AbilityTwoText.text = "Buffed";
        }
        else if (GameManager.instance.isDoctorNerfed)
        {
            AbilityTwoText.text = "Nerfed";
        } else {
            AbilityOneText.text = "NOT GET";
        }

        replayButton.onClick.AddListener(()=> {
            SceneManager.LoadScene("Chapter_One");
        });
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
