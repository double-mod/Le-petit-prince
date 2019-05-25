using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelector : MonoBehaviour
{
    [SerializeField] Button[] stageButton;

    [SerializeField] int focusStage = 0;
    [SerializeField] int maxStage;
    
    // Start is called before the first frame update
    void Start()
    {
        maxStage = stageButton.Length;
    }
    public void StageGoRight()
    {
        focusStage++;
        focusStage %= maxStage;
        SelectFocusStageButton();
    }

    public void StageGoLeft()
    {
        focusStage--;
        if (focusStage < 0)
        {
            focusStage += 3;
        }
        SelectFocusStageButton();
    }

    public void  SelectFocusStageButton()
    {
        
        stageButton[focusStage].Select();
    }
    
}
