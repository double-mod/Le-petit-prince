using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Openning : MonoBehaviour
{
    string state = "wait";
    float timer = 0f;
    float waitTime = 1.0f;

    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] Button conversationButton;
    [SerializeField] Button NextSceneButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case "wait":
                timer += Time.deltaTime;
                if (timer >= waitTime)
                {
                    conversationButton.onClick.Invoke();
                    timer = 0f;
                    state = "state2";
                }
                break;
        }
    }
}
