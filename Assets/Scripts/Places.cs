using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct Place {
    public Place(Box[] ingredients, Station[] stations, string[][] orders, float orderAmount, Color _color) { Ingredients = ingredients; Stations = stations; Orders = orders; OrderAmount = orderAmount; color = _color; }
    public Box[] Ingredients;
    //public Ingredient[] Ingredients;
    public Station[] Stations;
    public string[][] Orders;
    public float OrderAmount;
    public Color color;
    public void Initiate(Overcooked module) {
        
    }
}