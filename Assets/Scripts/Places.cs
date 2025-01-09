using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HarmonyLib;

public struct Place {
    public Place(Ingredient[] ingredients, Station[] stations, string[][] orders, float orderAmount, Color _color) { 
        Ingredients = ingredients;
        Stations = stations;
        Orders = orders;
        OrderAmount = orderAmount;
        color = _color;
        Boxes = new Box[9]; 
    }
    public Ingredient[] Ingredients;
    public Box[] Boxes;
    public Station[] Stations;
    public string[][] Orders;
    public float OrderAmount;
    public Color color;
    public void Initiate(Overcooked module) {
        int count1 = 0;
        foreach(Ingredient ingredient in Ingredients)
        {
            Boxes[count1] = new Box(module, count1, ingredient);
            Boxes[count1].startup();
            count1++;
        }
        int count2 = 0;
        foreach(Station station in Stations)
        {
            Stations[count2]._module = module;
            Stations[count2].startup();
        }
    }
}