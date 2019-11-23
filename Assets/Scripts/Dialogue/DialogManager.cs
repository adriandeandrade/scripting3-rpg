using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    #region Singleton
    private static DialogManager instance;
    public static DialogManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DialogManager>();
            }
            return instance;
        }
    }

    #endregion

    // Public Variables
    [SerializeField] private TextMeshProUGUI dialogueText;

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

    public void StartDialog(Dialog dialogue)
    {
        //animator.SetBool("IsOpen", true);
        hasSentence = true;

        sentences.Clear();

        foreach (string sentence in dialogue.instructions)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void StartEndDialogue()
    {
        StartCoroutine(EndDialogue());
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 1)
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
        string sentence = sentences.Dequeue();
        StartTypingSentence(sentence);

        yield return new WaitForSeconds(3f);

        //animator.SetBool("IsOpen", false);
    }
}

