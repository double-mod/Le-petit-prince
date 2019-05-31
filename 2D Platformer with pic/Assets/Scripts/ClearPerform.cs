using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearPerform : MonoBehaviour
{
    [SerializeField] Camera performCamera;
    [SerializeField] Transform clockPos;
    [SerializeField] Transform titlePos;
    [SerializeField] GameObject motoClock;

    Animator animator;
    Vector3 pos;

    public void SwitchCamera()
    {
        performCamera.transform.position = Camera.main.transform.position;
        performCamera.enabled = true;
        Camera.main.enabled = false;
        StartCoroutine(CameraTrans(clockPos));
    }

    IEnumerator CameraTrans(Transform target)
    {
        for (; performCamera.transform.position.magnitude - target.position.magnitude < 0.1f;)
        {
            pos = Vector3.MoveTowards(performCamera.transform.position, target.position, 12f * Time.deltaTime);
            pos.z = performCamera.transform.position.z;
            performCamera.transform.position = pos;
            yield return null;
            
        }
        animator.SetTrigger("Clock");
    }

    public void CameraToTitle()
    {
        StartCoroutine(CameraTrans(titlePos));
    }

    public void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        performCamera.transform.position = Camera.main.transform.position;
        motoClock.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
