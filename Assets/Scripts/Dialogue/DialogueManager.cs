using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    #region Singleton
    private static DialogueManager instance;
    public static DialogueManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DialogueManager>();
            }
            return instance;
        }
    }

    #endregion

    // Public Variables
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI continueButtonText;
    [SerializeField] private Button continueButton;

    // Private Variables
    private Queue<string> sentences;
    private bool hasSentence;
    private int sentencesCount;

    // Components
    [SerializeField] private Animator animator;

    private void Awake()
    {
        sentences = new Queue<string>();
    }

    private void Update()
    {
        sentencesCount = sentences.Count;
    }

    private void StartTypingSentence(string sentence)
    {
        StartCoroutine(TypeSentence(sentence));
    }

    public void SetDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        continueButtonText.SetText("CONTINUE");
        continueButton.interactable = true;
        hasSentence = true;

        sentences.Clear();

        foreach (string sentence in dialogue.instructions)
        {
            sentences.Enqueue(sentence);
        }

        //DisplayNextSentence();
    }

    public void StartEndDialogue()
    {
        StartCoroutine(EndDialogue());
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 1)
        {
            continueButtonText.SetText("CLOSE");
        }

        if (sentences.Count == 0)
        {
            StartEndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StartTypingSentence(sentence);
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.SetText("");
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    IEnumerator EndDialogue()
    {
        continueButton.interactable = false;
        //yield return new WaitForSeconds(0.5f);

        animator.SetBool("IsOpen", false);

        yield break;
    }

    // Animation event from dialogue panel.
    public void OnOpen()
    {
        Debug.Log("Dialogue box finished opening.");
        DisplayNextSentence();
    }

    // Animation event from dialogue panel.
    public void OnClose()
    {

    }
}

