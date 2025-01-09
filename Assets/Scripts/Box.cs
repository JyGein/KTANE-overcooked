using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Box {
    public Box(Overcooked module, int number, Ingredient type) { _module = module; _number = number; _type = type; }
    private Overcooked _module;
    private int _number;
    public Ingredient _type;
    public void startup () {
        _module.ingredientBoxes[_number].transform.Find("ingredientImage").transform.GetComponent<MeshRenderer>().enabled = true;
        int temp = -1;
        temp = Array.IndexOf(_module.allIngredients, _type);
        _module.ingredientBoxes[_number].transform.Find("ingredientImage").transform.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", _module.ingredientImages[temp]);
    }
    public Ingredient[] Interact(Ingredient[] hands) {
        if(hands.Length != 0) {
            _module.log($"Hands are full");
            return hands;
        }
        _module.log($"Picked up {_type.ToString()}");
        return new Ingredient[] { Util.DeepCopy(_type) };
    }
}
