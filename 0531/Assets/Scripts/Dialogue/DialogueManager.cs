using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue<string> sentences;
    private Dialogue dialogue;
    

    [SerializeField] private Transform background;
    [SerializeField] private Transform avatar;
    [SerializeField] private Button continueButton;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);


        background.localScale= new Vector3(dialogue.invert ? -1f : 1f, 1, 1);
        avatar.localPosition = new Vector3 (Mathf.Abs(avatar.localPosition.x) * (dialogue.invert ? 1f : -1f), avatar.localPosition.y, 0f);
        continueButton.gameObject.SetActive(true);
        continueButton.Select();

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayerNextSentence();
        this.dialogue = dialogue;
    }

    public void DisplayerNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        continueButton.gameObject.SetActive(false);
        animator.SetBool("IsOpen", false);
        if (dialogue.nextTrigger != null)
        {
            StopAllCoroutines();
            StartCoroutine(StartNextDialogue());
        }
        else
        {
            if (dialogue.nextButtonToTrigger != null)
            {
                dialogue.nextButtonToTrigger.onClick.Invoke();
            }
        }
    }

    IEnumerator StartNextDialogue()
    {
        yield return new WaitForSeconds(0.15f);
        dialogue.nextTrigger.TriggerDialogue();
    }
}
