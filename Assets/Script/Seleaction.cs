using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class DialogueData {
    public List<DialogueEntry> Intro;
    public List<DialogueEntry> Npc1;
    public List<DialogueEntry> Npc2;
    public List<DialogueEntry> Npc3;
    public List<DialogueEntry> Npc4;
    public List<DialogueEntry> Npc5;
    // ... 其他NPC列表 ...
}

[System.Serializable]
public class DialogueContainer {
    public DialogueData dialogues;
}

[System.Serializable]
public class DialogueEntry {
    public string text;
    public int order;
    public bool isOption;

    public string Option_One_Text;

    public string Option_Two_Text;

    public int nextOrder;
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

    float winRate = 0.0f;

    int NpcIndex = 0;

    public List<DialogueEntry> dialoguesList_Npc1;
    public List<DialogueEntry> dialoguesList_Npc2;
    public List<DialogueEntry> dialoguesList_Npc3;
    public List<DialogueEntry> dialoguesList_Npc4;
    public List<DialogueEntry> dialoguesList_Npc5;




    public List<DialogueEntry> currentDialogues;

    public DialogueEntry currentDialogue;

    public int index = 0;

    public bool isEnd = false;

    public bool showOption = false;

    public DialogueContainer dialogues;
 
    // Start is called before the first frame update
    void Start()
    {
        this.winRate = GameManager.instance.winrate * 100.0f;
        this.NpcIndex = GameManager.instance.NpcIndex;

        TextAsset textAsset = Resources.Load<TextAsset>("Dialogue");
        dialogues = JsonUtility.FromJson<DialogueContainer>(textAsset.text);
        // Debug.Log(textAsset.text);
        // if (dialogues != null && dialogues.dialogues.Npc1 != null) {
        //     Debug.Log("Number of dialogues for Npc1: " + dialogues.dialogues.Npc1.Count);
        // } else {
        //     Debug.Log("Dialogues not loaded or parsed correctly.");
        // }


        GetDialogues(NpcIndex);

        currentDialogue = currentDialogues[index];

        UpdateWinRate(this.winRate);

        StartDialogues(currentDialogue.text);
    }

    // Update is called once per frame
    void Update()
    {
        if(!currentDialogue.isOption) {
            optionOneButton.gameObject.SetActive(false);
        }
    }

    void GetDialogues( int NpcIndex )
    {   
        if (NpcIndex == 0)
        {
            this.currentDialogues = this.dialogues.dialogues.Npc1;
        }
        else if (NpcIndex == 1)
        {
            this.currentDialogues = this.dialogues.dialogues.Npc2;
        }
        else if (NpcIndex == 2)
        {
            this.currentDialogues = this.dialogues.dialogues.Npc3;
        }
        else if (NpcIndex == 3)
        {
            this.currentDialogues = this.dialogues.dialogues.Npc4;
        }
        else if (this.NpcIndex == 4)
        {
            this.currentDialogues = this.dialogues.dialogues.Npc5;
        }
    }

    public void StartDialogues(string text)
    {
        dialoguesText.text = string.Empty;
        StartCoroutine(TypeSentence(text));
    }

    public void UpdateWinRate(float winRate)
    {
        winRateText.text = "Wintate: " + winRate.ToString() + "%";
    }

    IEnumerator TypeSentence(string sentence)
    {
        foreach (char letter in sentence.ToCharArray())
        {
            dialoguesText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
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

    public void NextSentence()
    {
        index++;
        dialoguesText.text = string.Empty;
        StartCoroutine(TypeSentence(currentDialogues[index].text));
    }
}
