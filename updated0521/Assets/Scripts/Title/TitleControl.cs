using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TitleControl : MonoBehaviour
{
    Animator animator;
    [SerializeField] GameObject follower;
    [SerializeField] GameObject follower2;
    [SerializeField] GameObject follower3;

    [SerializeField] Button[] nextSelectButton;
    [SerializeField] Button slefButton;

    private void Start()
    {
        slefButton = GetComponent<Button>();
    }
    

    void FadeMeOut()
    {
        animator.SetTrigger("fadeMeOut");
    }

    void SetSelfOff()
    {
        gameObject.SetActive(false);
    }

    void SetSelfOn()
    {
        gameObject.SetActive(true);
    }

    void SetFollowerOn()
    {
        follower.SetActive(true);
    }

    void SetFollower2On()
    {
        follower2.SetActive(true);
    }
    void SetFollower3On()
    {
        follower3.SetActive(true);
    }
    void SetNextSelect()
    {
        nextSelectButton[0].Select();
    }

    void SetNextSelectByNum(int nextButtonNum)
    {
        nextSelectButton[nextButtonNum].Select();
    }
    void SelectSlef()
    {
        slefButton.Select();
    }
}
