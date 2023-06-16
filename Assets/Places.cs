using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Place {
    public Place(string[] ingredients, Station[] stations, string[] orders) { Ingredients = ingredients; Stations = stations; Orders = orders; }
    public string[] Ingredients;
    public Station[] Stations;
    public string[] Orders;
}