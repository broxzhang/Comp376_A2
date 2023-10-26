using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// [System.Serializable]
// public class DialogueData {
//     public List<DialogueEntry> Intro;
// }


public class Intro : MonoBehaviour
{
    public GameObject currentPanel; // 你可以在Inspector中拖放当前的Panel
    public GameObject npcPanel; // 你可以在Inspector中拖放NpcPanel
    // Start is called before the first frame update
    public TextMeshProUGUI textDisplay;
    public string[] sentences;

    private int index;

    public float typingSpeed;

    public int maxSentence = 5;

    public List<DialogueEntry> dialoguesList_Intro;
    public DialogueContainer dialogues;
    public DialogueEntry currentDialogue;

    private bool isTyping = false;

    void Start()
    {
        textDisplay.text = string.Empty;
        
        TextAsset textAsset = Resources.Load<TextAsset>("Dialogue");
        dialogues = JsonUtility.FromJson<DialogueContainer>(textAsset.text);

        this.dialoguesList_Intro = this.dialogues.dialogues.Intro;

        StartIntro();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                textDisplay.text = currentDialogue.text;
                isTyping = false;
                return;
            }

            if (currentDialogue.nextOrder > 0)
            {
                textDisplay.text = string.Empty;
                currentDialogue = GetDialogueByOrder(currentDialogue.nextOrder);
                StartCoroutine(TypeSentence(currentDialogue.text));
            }
            else if (currentDialogue.nextOrder == -1)
            {
                textDisplay.text = currentDialogue.text;
                currentDialogue = null;
            }
            
            if (currentDialogue == null)
            {
                currentPanel.SetActive(false);
                npcPanel.SetActive(true);
            }
        }
    }

    public void StartIntro()
    {
        index = 1;
        this.currentDialogue = GetDialogueByOrder(index);
        StartCoroutine(TypeSentence(currentDialogue.text));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;

        foreach (char letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        isTyping = false;
    }

    public DialogueEntry GetDialogueByOrder(int order)
    {
        foreach (DialogueEntry dialogue in dialoguesList_Intro)
        {
            if (dialogue.order == order)
            {
                return dialogue;
            }
        }
        return null;
    }
}
