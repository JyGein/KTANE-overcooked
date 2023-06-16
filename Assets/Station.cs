using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Station {
    public abstract string[] Interact(string[] hands);
    public abstract void updoot();
    public float timer = 0;
    public string[] slot = new string[0];
}