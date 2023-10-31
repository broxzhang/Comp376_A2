using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class DialogueData
{
    public List<DialogueEntry> Intro;
    public List<DialogueEntry> Librarian;
    public List<DialogueEntry> Chief;
    public List<DialogueEntry> Postman;
    public List<DialogueEntry> Housekeeper;
    public List<DialogueEntry> Doctor;
}

[System.Serializable]
public class DialogueContainer
{
    public DialogueData dialogues;
}

[System.Serializable]
public class DialogueEntry
{
    public string text;

    public int order;

    public bool isOption;

    public string Option_One_Text;

    public int Option_One_nextOrder;

    public string Option_Two_Text;

    public int Option_Two_nextOrder;

    public int nextOrder;

    public float Option_WinRate;

}


public class Seleaction : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI winRateText;
    [SerializeField]
    private TextMeshProUGUI dialoguesText;

    [SerializeField]
    private Button optionOneButton;

    [SerializeField]
    private Button optionTwoButton;

    public GameObject currentPanel;
    public GameObject npcSelectPanel;

    float winRate = 0.0f;

    int NpcIndex = 0;

    public Image displayImage;
    public Sprite[] imageOptions;

    public List<DialogueEntry> currentDialogues;

    public DialogueEntry currentDialogue;

    public int index = 0;

    public bool showOption = false;

    public DialogueContainer dialogues;

    // Start is called before the first frame update
    void Start()
    {
        optionOneButton.onClick.AddListener(() => OnOptionOneButtonClick());
        optionTwoButton.onClick.AddListener(() => OnOptionTwoButtonClick());
    }

    void OnEnable()
    {
        optionOneButton.gameObject.SetActive(true);
        optionTwoButton.gameObject.SetActive(true);
        this.winRate = GameManager.instance.winrate * 100.0f;
        this.NpcIndex = GameManager.instance.NpcIndex;

        index = 0;

        TextAsset textAsset = Resources.Load<TextAsset>("Dialogue");
        dialogues = JsonUtility.FromJson<DialogueContainer>(textAsset.text);

        displayImage.sprite = imageOptions[NpcIndex - 1];




        GetDialogues(NpcIndex);

        currentDialogue = currentDialogues[index];

        UpdateWinRate(this.winRate);

        StartDialogues(currentDialogue.text);

    }

    // Update is called once per frame
    void Update()
    {
        if (!currentDialogue.isOption)
        {
            optionOneButton.gameObject.SetActive(false);
            optionTwoButton.gameObject.SetActive(false);
            if (Input.GetMouseButtonDown(0))
            {
                currentPanel.SetActive(false);
                npcSelectPanel.SetActive(true);
            }
        }

        if (currentDialogue.isOption)
        {
            optionOneButton.GetComponentInChildren<TextMeshProUGUI>().text = currentDialogue.Option_One_Text;
            optionTwoButton.GetComponentInChildren<TextMeshProUGUI>().text = currentDialogue.Option_Two_Text;
        }
    }

    void GetDialogues(int NpcIndex)
    {
        if (NpcIndex == 1)
        {
            this.currentDialogues = this.dialogues.dialogues.Librarian;
        }
        else if (NpcIndex == 2)
        {
            this.currentDialogues = this.dialogues.dialogues.Chief;
        }
        else if (NpcIndex == 3)
        {
            this.currentDialogues = this.dialogues.dialogues.Postman;
        }
        else if (NpcIndex == 4)
        {
            this.currentDialogues = this.dialogues.dialogues.Housekeeper;
        }
        else if (this.NpcIndex == 5)
        {
            this.currentDialogues = this.dialogues.dialogues.Doctor;
        }
    }

    public void StartDialogues(string text)
    {
        dialoguesText.text = string.Empty;
        dialoguesText.text = text;

    }

    public void UpdateWinRate(float winRate)
    {
        winRateText.text = "Wintate: " + winRate.ToString() + "%";
    }

    public DialogueEntry GetDialogueByOrder(int order)
    {
        foreach (DialogueEntry dialogue in currentDialogues)
        {
            if (dialogue.order == order)
            {
                return dialogue;
            }
        }
        return null;
    }

    void OnOptionOneButtonClick()
    {
        dialoguesText.text = string.Empty;
        if (currentDialogue.Option_One_nextOrder > 0)
        {
            currentDialogue = GetDialogueByOrder(currentDialogue.Option_One_nextOrder);
            dialoguesText.text = currentDialogue.text;

            Debug.Log(currentDialogue.order);

            if (currentDialogue.order >= 4)
            {
                GameManager.instance.winrate += currentDialogue.Option_WinRate;
                UpdateWinRate(GameManager.instance.winrate * 100.0f);
            }

            if (currentDialogue.order == 4)
            {
                setNpcToTalked(NpcIndex);
                if (NpcIndex == 1)
                {
                    GameManager.instance.positiveEvidenceOne = true;
                }
                if (NpcIndex == 2)
                {
                    GameManager.instance.isChiefBuffed = true;
                }
                if (NpcIndex == 3)
                {
                    GameManager.instance.positiveEvidenceTwo = true;
                }
                if (NpcIndex == 4)
                {
                    GameManager.instance.positiveEvidenceThree = true;
                }
                if (NpcIndex == 5)
                {
                    GameManager.instance.isDoctorBuffed = true;
                }
            }

            if (currentDialogue.order == 6)
            {
                setNpcToTalked(NpcIndex);
                if (NpcIndex == 1)
                {
                    GameManager.instance.mutralEvidenceOne = true;
                }
                if (NpcIndex == 3)
                {
                    GameManager.instance.mutralEvidenceTwo = true;
                }
                if (NpcIndex == 4)
                {
                    GameManager.instance.mutralEvidenceThree = true;
                }
            }
        }
    }
    void OnOptionTwoButtonClick()
    {
        dialoguesText.text = string.Empty;
        if (currentDialogue.Option_Two_nextOrder > 0)
        {
            currentDialogue = GetDialogueByOrder(currentDialogue.Option_Two_nextOrder);
            // StartCoroutine(TypeSentence(currentDialogue.text));
            dialoguesText.text = currentDialogue.text;

            if (currentDialogue.order >= 4)
            {
                GameManager.instance.winrate += currentDialogue.Option_WinRate;
                UpdateWinRate(GameManager.instance.winrate * 100.0f);
            }

            if (currentDialogue.order == 5)
            {
                setNpcToTalked(NpcIndex);
                if (NpcIndex == 1)
                {
                    GameManager.instance.mutralEvidenceOne = true;
                }
                if (NpcIndex == 3)
                {
                    GameManager.instance.mutralEvidenceTwo = true;
                }
                if (NpcIndex == 4)
                {
                    GameManager.instance.mutralEvidenceThree = true;
                }

            }

            if (currentDialogue.order == 7)
            {
                setNpcToTalked(NpcIndex);
                if (NpcIndex == 1)
                {
                    GameManager.instance.negativeEvidenceOne = true;
                }
                if (NpcIndex == 2)
                {
                    GameManager.instance.isChiefNerfed = true;
                }
                if (NpcIndex == 3)
                {
                    GameManager.instance.negativeEvidenceTwo = true;
                }
                if (NpcIndex == 4)
                {
                    GameManager.instance.negativeEvidenceThree = true;
                }
                if (NpcIndex == 5)
                {
                    GameManager.instance.isDoctorNerfed = true;
                }
            }
        }
    }



    void setNpcToTalked(int npcIndex)
    {
        if (npcIndex == 1)
        {
            GameManager.instance.old_man_talked = true;
        }
        if (npcIndex == 2)
        {
            GameManager.instance.plice_chief_talked = true;
        }
        if (npcIndex == 3)
        {
            GameManager.instance.mail_man_talked = true;
        }
        if (npcIndex == 4)
        {
            GameManager.instance.female_butler_talked = true;
        }
        if (npcIndex == 5)
        {
            GameManager.instance.young_doctor_talked = true;
        }

    }

}
