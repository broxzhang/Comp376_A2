using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Intro : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI textDisplay;
    public string[] sentences;

    private int index;

    public float typingSpeed;

    public int maxSentence = 5;

    void Start()
    {
        textDisplay.text = string.Empty;
        StartIntro();
    }

    // Update is called once per frame
    void Update()
    {
        if (textDisplay.text == sentences[index])
        {
            NextSentence();
        }
    }

    public void StartIntro()
    {
        index = 0;
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            Debug.Log("NextSentence");
            index++;
            textDisplay.text = string.Empty;
            StartCoroutine(TypeSentence());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
