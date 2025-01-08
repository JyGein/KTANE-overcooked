using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public abstract class Station {
    public abstract string[] Interact(string[] hands);
    public abstract void updoot();
    public abstract void startup();
    public float timer = 0;
    public string[] slot = new string[0];
    public int Image;
    public string Color;
    public Overcooked _module;
}