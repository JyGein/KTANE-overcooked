using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Place {
    public Place(Box[] ingredients, Station[] stations, string[][] orders, int orderAmount) { Ingredients = ingredients; Stations = stations; Orders = orders; OrderAmount = orderAmount; }
    public Box[] Ingredients;
    public Station[] Stations;
    public string[][] Orders;
    public int OrderAmount;
}