﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    
    int activeFlowCnt = 0;
    [SerializeField] GameObject[] flows;

    [SerializeField] Animator fadePanel;


    public void ActiveNextFlow()
    {
        fadePanel.SetTrigger("Fade In");

        StopAllCoroutines();
        StartCoroutine(SetNext());
    }

    IEnumerator SetNext()
    {
        yield return new WaitForSeconds(0.5f);
        flows[activeFlowCnt].SetActive(false);
        activeFlowCnt++;
        flows[activeFlowCnt].SetActive(true);
        yield return new WaitForSeconds(0.2f);
        fadePanel.SetTrigger("Fade Out");
    }

    public void BackToTitleScene()
    {
        SceneManager.LoadScene("Title");
    }
}
