using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingConv : MonoBehaviour
{
    [SerializeField] Button theButton;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitToConv());
    }
    IEnumerator waitToConv()
    {
        yield return new WaitForSeconds(2.0f);
        theButton.onClick.Invoke();
    }
}
