using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class State{

    public string name;
    public bool active;
    public State(string name)
    {
        this.name = name;
    }

}
