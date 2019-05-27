using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    [SerializeField] Text message;
    [SerializeField] Animator animator;

    private DiaMessage _message;

    public void StartMessage(DiaMessage message)
    {

        animator.SetBool("IsOpen", true);
        
        _message = message;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(message.sentences));
    }


    IEnumerator TypeSentence(string sentence)
    {
        message.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            message.text += letter;
            yield return new WaitForSeconds(_message.displaySpeed);
        }
        yield return new WaitForSeconds(_message.displayTime);
        animator.SetBool("IsOpen", false);
    }
}
