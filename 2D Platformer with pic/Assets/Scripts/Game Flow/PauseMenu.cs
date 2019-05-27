using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Button nextButton;
    [SerializeField] Button dammyButton;

    private void OnEnable()
    {
        SelectButton();
    }

    public void SelectButton()
    {
        dammyButton.Select();
        nextButton.Select();
    }
}
