using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTarget : MonoBehaviour
{
    public float speed;

    public Vector2 moveDirection;

    private RectTransform rectTransform;

    [SerializeField]
    private RectTransform parentRectTransform;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(250f, 500f);
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = GameObject.Find("HitPanel").GetComponent<RectTransform>();
        
        moveDirection = Random.insideUnitCircle.normalized;
    }

    // Update is called once per frame
    private void Update()
    {
        rectTransform.anchoredPosition += moveDirection * speed * Time.deltaTime;

        // Check if out of bounds
        if ((rectTransform.anchoredPosition.x + rectTransform.sizeDelta.x / 2) > parentRectTransform.rect.width / 2)
        {
            moveDirection.x = -Mathf.Abs(moveDirection.x) * Random.Range(0.8f, 1.2f);
            rectTransform.anchoredPosition = new Vector2(parentRectTransform.rect.width / 2 - rectTransform.sizeDelta.x / 2, rectTransform.anchoredPosition.y);
        }
        else if ((rectTransform.anchoredPosition.x - rectTransform.sizeDelta.x / 2) < -parentRectTransform.rect.width / 2)
        {
            moveDirection.x = Mathf.Abs(moveDirection.x) * Random.Range(0.8f, 1.2f);
            rectTransform.anchoredPosition = new Vector2(-parentRectTransform.rect.width / 2 + rectTransform.sizeDelta.x / 2, rectTransform.anchoredPosition.y);
        }

        if ((rectTransform.anchoredPosition.y + rectTransform.sizeDelta.y / 2) > parentRectTransform.rect.height / 2)
        {
            moveDirection.y = -Mathf.Abs(moveDirection.y) * Random.Range(0.8f, 1.2f);
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, parentRectTransform.rect.height / 2 - rectTransform.sizeDelta.y / 2);
        }
        else if ((rectTransform.anchoredPosition.y - rectTransform.sizeDelta.y / 2) < -parentRectTransform.rect.height / 2)
        {
            moveDirection.y = Mathf.Abs(moveDirection.y) * Random.Range(0.8f, 1.2f);
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -parentRectTransform.rect.height / 2 + rectTransform.sizeDelta.y / 2);
        }

    }
}
