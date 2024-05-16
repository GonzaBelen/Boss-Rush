using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class Dialogue : MonoBehaviour
{
    private BrotherController brotherController;
    private GameObject player;
    private PlayerController playerController;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject movementBarrier;
    [SerializeField] private GameObject nextDialogue;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private float typingTime = 0.05f;
    private bool isPlayerInRange;
    private bool didDialogueStart = false;
    private int lineIndex;
    
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        brotherController = GetComponentInParent<BrotherController>();
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetButtonDown("Fire1"))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            } else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialogueMark.SetActive(false);
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        playerController.SetInputEnabled(false);
        StartCoroutine(ShowLine());
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            dialogueMark.SetActive(true);
        }    
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueMark.SetActive(false);
        }    
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        } else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            dialogueMark.SetActive(true);
            playerController.SetInputEnabled(true);
            DestroyBarrier();
        }
    }

    public void DestroyBarrier()
    {
        movementBarrier.SetActive(false);
        gameObject.SetActive(false);
        ActiveNextDialogue();
    }

    private void ActiveNextDialogue()
    {
        nextDialogue.SetActive(true);
    }
}
