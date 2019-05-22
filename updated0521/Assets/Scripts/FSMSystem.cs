using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMSystem : MonoBehaviour
{
    [SerializeField] string stateName;

    public delegate void StateUpdateFunc();
    private StateUpdateFunc state;
    private StateUpdateFunc stateNext;
    private Dictionary<string, StateUpdateFunc> stateUpdateDic;

    private bool stateNew = true;
    private float stateTimer = 0;


    // Start is called before the first frame update
    void Start()
    {
        stateUpdateDic = new Dictionary<string, StateUpdateFunc>();
    }

    public float StateTimer
    {
        get { return stateTimer; }
        set { }
    }

    public string StateName
    {
        get { return stateName; }
        set { }
    }

    public bool StateNew
    {
        get { return stateNew; }
        set { }
    }

    public void StateCreate(string name, StateUpdateFunc stateFunc)
    {
        stateUpdateDic.Add(name, stateFunc);
        if (state == null)
        {
            StateInit(name);
        }
    }

    public void StateInit(string name)
    {
        if (stateUpdateDic.ContainsKey(name))
        {
            state = stateUpdateDic[name];
            stateName = name;
        }
        else
        {
            Debug.LogError("Cant's Set InitState, state " + name + " is not exist");
        }
    }

    public void StateExecute()
    {
        state();
    }

    public void StateUpdate()
    {
        if (state != stateNext && stateNext != null)
        {
            state = stateNext;
            stateTimer = 0f;
            stateNew = true;
        }
        else
        {
            stateTimer+= Time.deltaTime;
            stateNew = false;
        }
    }

    public void StateSwitch(string name)
    {
        if (stateUpdateDic.ContainsKey(name))
        {
            stateNext = stateUpdateDic[name];
            stateName = name;
        }
        else
        {
            Debug.LogError("StateSwitch Error!");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void LateUpdate()
    {
    }
}
