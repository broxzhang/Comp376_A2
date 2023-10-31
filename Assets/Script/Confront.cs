using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading;

public class Confront : MonoBehaviour
{

    public bool inPhaseOne = false;
    public bool inPhaseTwo = false;
    public bool inPhaseThree = false;

    public GameObject currentPanel;
    public GameObject SummaryPanel;

    public Texture2D newCursorTexture;
    public Button replayButton;

    public int hitRemaining = 0;

    public int chanceRemaining = 3;

    public Button confrontButton;

    public Sprite MCimage;

    public Sprite NPCimage;

    public Image ShowingImage;

    public TextMeshProUGUI timer;

    public TextMeshProUGUI dialogueText;

    public TextMeshProUGUI chanceText;

    private float timeLeft = 15.0f;

    public int FirstTargetIndex = 4;
    
    public int SecondTargetIndex = 1;

    public int ThirdTargetIndex = 2;

    public string[] PhaseOneSentences = new string[5];

    public string[] PhaseTwoSentences = new string[5];

    public string[] PhaseThreeSentences = new string[5];

    public bool phaseOneEnded = false;

    public bool phaseTwoEnded = false;

    public bool phaseThreeEnded = false;

    [SerializeField]
    public TextMeshProUGUI[] speechTexts;

    public int currentSentenceIndex = 0;

    public bool timeOut = false;


    // Start is called before the first frame update
    void Start()
    {
        inPhaseOne = true;
        for (int i = 0; i < 5; i++)
        {
            speechTexts[i].text = PhaseOneSentences[i];
        }

        chanceText.text = "You have " + chanceRemaining + " chance(s) left.";

        HideOrShowDialogueTarget(false);

        print("Start");

        timer.gameObject.SetActive(false);

        ShowingImage.sprite = NPCimage;

        replayButton.onClick.AddListener(() => ReplaySentence());
        confrontButton.onClick.AddListener(() => ConfrontSentence());

        replayButton.gameObject.SetActive(false);
        confrontButton.gameObject.SetActive(false);

        dialogueText.text = PhaseOneSentences[currentSentenceIndex];
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        timer.text = Mathf.Max(timeLeft, 0).ToString("F0");

        if(timeLeft <= 0 && hitRemaining>0 && !timeOut) {
            Debug.Log("Time out");
            timeOut = true;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            replayButton.gameObject.SetActive(false);
            confrontButton.gameObject.SetActive(false);
            HideOrShowDialogueTarget(false);
            chanceRemaining--;
            chanceText.text = "You have " + chanceRemaining + " chance(s) left.";

            dialogueText.text = string.Empty;
            dialogueText.text = "I lost my chance to confront him, I have to move on.";
            if(inPhaseOne) {
                phaseOneEnded = true;
            }

            if(inPhaseTwo) {
                phaseTwoEnded = true;
            }

            if(inPhaseThree) {
                phaseThreeEnded = true;
            }

        }

        if (hitRemaining == 0 && timeLeft>0 && !timeOut) {
            Debug.Log("You win");
            timeOut = true;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            replayButton.gameObject.SetActive(false);
            confrontButton.gameObject.SetActive(false);
            HideOrShowDialogueTarget(false);

            dialogueText.text = string.Empty;

            timer.gameObject.SetActive(false);

            
            // dialogueText.text = "You Lie! According to the evidence I have. ";

            if (inPhaseOne) {
                dialogueText.text = "Contrary to your statement, the postman mentioned an unusual increase in your mail activity around the time of the party. Specifically, there was an anonymous letter sent to the victim a few days back. This contradicts your claim of not receiving or sending any mail that night.";
                phaseOneEnded = true;
            }

            if (inPhaseTwo) {
                dialogueText.text = "Your claim of ignorance regarding heart diseases and medications doesn't hold up. The librarian confirmed that you borrowed books specifically on rare heart medications. This contradicts your statement about not knowing anything about heart medications.";
                phaseTwoEnded = true;
            }

            if (inPhaseThree) {
                dialogueText.text = "You stated that you never entered the kitchen, but the housekeeper found items belonging to you in there. This directly contradicts your claim and suggests you were indeed in the kitchen at some point during the night.";
                phaseThreeEnded = true;
            }
        }

        

    }


    void StartTimer()
    {
        timer.gameObject.SetActive(true);
        if(GameManager.instance.isChiefBuffed) {
            timeLeft = 30.0f;
        } else if(GameManager.instance.isChiefNerfed) {
            timeLeft = 3.0f;
        } else {
            timeLeft = 20.0f;
        }
        timeOut = false;
    }

    void ReplaySentence()
    {
        ShowingImage.sprite = NPCimage;
        currentSentenceIndex = 0;
        dialogueText.text = string.Empty;
        if (inPhaseOne)
        {
            dialogueText.text = PhaseOneSentences[currentSentenceIndex];
        }
        if (inPhaseTwo)
        {
            dialogueText.text = PhaseTwoSentences[currentSentenceIndex];
        }
        if (inPhaseThree)
        {
            dialogueText.text = PhaseThreeSentences[currentSentenceIndex];
        }

        replayButton.gameObject.SetActive(false);
        confrontButton.gameObject.SetActive(false);
    }

    void ConfrontSentence()
    {
        replayButton.gameObject.SetActive(false);
        confrontButton.gameObject.SetActive(false);

        StartTimer();
        HideOrShowDialogueTarget(true);

        Cursor.SetCursor(newCursorTexture, Vector2.zero, CursorMode.Auto);

        ConfrontToEnemy();
    }

    void HideOrShowDialogueTarget(bool show)
    {
        foreach (var speech in speechTexts)
        {
            speech.transform.parent.gameObject.SetActive(show);
        }
    }

    void ConfrontToEnemy()
    {
        if(inPhaseOne) {
            if (GameManager.instance.positiveEvidenceOne)
            {
                hitRemaining = 1;
            }
            else if (GameManager.instance.mutralEvidenceOne)
            {
                hitRemaining = 5;
            }
            else if (GameManager.instance.negativeEvidenceOne)
            {
                hitRemaining = 10;
            }
            AddClickEventToTarget(FirstTargetIndex);
        }
        if(inPhaseTwo) {
            if (GameManager.instance.positiveEvidenceTwo)
            {
                hitRemaining = 1;
            }
            else if (GameManager.instance.mutralEvidenceTwo)
            {
                hitRemaining = 5;
            }
            else if (GameManager.instance.negativeEvidenceTwo)
            {
                hitRemaining = 10;
            }
            AddClickEventToTarget(SecondTargetIndex);
        }
        if(inPhaseThree) {
            if (GameManager.instance.positiveEvidenceThree)
            {
                hitRemaining = 1;
            }
            else if (GameManager.instance.mutralEvidenceThree)
            {
                hitRemaining = 5;
            }
            else if (GameManager.instance.negativeEvidenceThree)
            {
                hitRemaining = 10;
            }
            AddClickEventToTarget(ThirdTargetIndex);
        }
    }


    void AddClickEventToTarget(int index)
    {
        foreach (var speech in speechTexts)
        {
            speech.color = Color.black;
        }
        if(GameManager.instance.isDoctorBuffed) {
            speechTexts[index].color = Color.red;
        }
        if(GameManager.instance.isDoctorNerfed) {
            foreach (var speech in speechTexts)
            {
                speech.color = Color.gray;
            }
        }
        EventTrigger trigger = speechTexts[index].transform.parent.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry circleClick = new()
        {
            eventID = EventTriggerType.PointerClick
        };

        circleClick.callback.AddListener((data) => { HandleTargetClick(); });

        trigger.triggers.Add(circleClick);
    }

    public void HandleTargetClick()
    {
        Debug.Log("Clicked");
        this.hitRemaining--;

        dialogueText.text = string.Empty;
        dialogueText.text = "You hit the real sentence, you need " + hitRemaining + " hit(s) to crash him.";
    }

    public void HandlePanelClick()
    {
        if (inPhaseOne)
        {
            if (currentSentenceIndex < 4)
            {
                currentSentenceIndex++;
                dialogueText.text = PhaseOneSentences[currentSentenceIndex];
            }
            else
            {
                ShowMCDialogue();
            }
        }
        if (inPhaseTwo)
        {
            if (currentSentenceIndex < 4)
            {
                currentSentenceIndex++;
                dialogueText.text = PhaseTwoSentences[currentSentenceIndex];
            }
            else
            {
                ShowMCDialogue();

            }
        }
        if (inPhaseThree)
        {
            if (currentSentenceIndex < 4)
            {
                currentSentenceIndex++;
                dialogueText.text = PhaseThreeSentences[currentSentenceIndex];
            }
            else
            {
                ShowMCDialogue();

            }
        }

        if(phaseOneEnded) {
            ShowingImage.sprite = NPCimage;

            inPhaseOne = false;
            inPhaseTwo = true;
            currentSentenceIndex = 0;
            dialogueText.text = string.Empty;
            dialogueText.text = PhaseTwoSentences[currentSentenceIndex];
            phaseOneEnded = false;
            replayButton.gameObject.SetActive(false);
            confrontButton.gameObject.SetActive(false);
        }

        if(phaseTwoEnded) {
            ShowingImage.sprite = NPCimage;
            inPhaseTwo = false;
            inPhaseThree = true;
            currentSentenceIndex = 0;
            dialogueText.text = string.Empty;
            dialogueText.text = PhaseThreeSentences[currentSentenceIndex];
            phaseTwoEnded = false;
            replayButton.gameObject.SetActive(false);
            confrontButton.gameObject.SetActive(false);
        }

        if(phaseThreeEnded) {
            if(chanceRemaining == 0) {
                GameManager.instance.isWin = false;
                currentPanel.SetActive(false);
                SummaryPanel.SetActive(true);
            } else {
                GameManager.instance.isWin = true;
                currentPanel.SetActive(false);
                SummaryPanel.SetActive(true);
            }
        }
    }

    void ShowMCDialogue()
    {
        ShowingImage.sprite = MCimage;
        dialogueText.text = string.Empty;
        dialogueText.text = "One of these statements is not matching the evidence I have. What should I do now?";
        replayButton.gameObject.SetActive(true);
        confrontButton.gameObject.SetActive(true);
    }
}
