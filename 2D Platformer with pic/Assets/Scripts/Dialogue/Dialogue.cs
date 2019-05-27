using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    public bool invert = false;
    public DialogueTrigger nextTrigger = null;
    public Button nextButtonToTrigger = null;
    public string name;
    [TextArea(3, 10)]
    public string[] sentences;
    
}
