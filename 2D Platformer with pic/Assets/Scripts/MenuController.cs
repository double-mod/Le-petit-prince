using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject menuOption;
    bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        menuOption.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            isPaused ^= true;
            Time.timeScale = isPaused ? 0f : 1f;
            menuOption.SetActive(isPaused);
        }
    }

    public void SetPause()
    {
        isPaused ^= true;
        Time.timeScale = isPaused ? 0f : 1f;
        menuOption.SetActive(isPaused);
    }
}
