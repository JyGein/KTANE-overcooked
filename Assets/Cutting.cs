using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class Cutting : Station {
    public Cutting(Overcooked module, int number) { _module = module; _number = number; }
    private Overcooked _module;
    private int _number;
    readonly string[] uncut = { "fish", "shrimp", "cucumber", "mushroom", "tomato", "beef", "lettuce", "potato", "chicken", "cheese", "dough", "pepperoni", "chocolate", "blueberry", "strawberry", "honey", "carrot" };
    readonly string[] cut = { "cutFish", "cutShrimp", "cutCucumber", "cutMushroom", "cutTomato", "cutBeef", "cutLettuce", "cutPotato", "cutChicken", "cutCheese", "cutDough", "cutPepperoni", "cutChocolate", "cutBlueberry", "cutStrawberry", "cutHoney", "cutCarrot" };
    public new string[] slot = new string[0];
    public new int Image = 1;
    public new float timer = 0;
    public override void startup() {
        updateText();
        _module.stations[_number].transform.Find("stationImage").transform.GetComponent<MeshRenderer>().material = _module.stationMaterials[Image];
    }
    public void updateText() {
        //_module.stations[_number].transform.Find("stationText").transform.GetComponent<TextMesh>().text = string.Join(" ", slot);
        Transform foodIcon = _module.stations[_number].transform.Find("FoodIcons").transform;
        MeshRenderer[] foodIcons = new MeshRenderer[] { foodIcon.Find("One").transform.Find("ingredientImage (0)").transform.GetComponent<MeshRenderer>(), foodIcon.Find("Two").transform.Find("ingredientImage (0)").transform.GetComponent<MeshRenderer>(), foodIcon.Find("Two").transform.Find("ingredientImage (1)").transform.GetComponent<MeshRenderer>(), foodIcon.Find("Three").transform.Find("ingredientImage (0)").transform.GetComponent<MeshRenderer>(), foodIcon.Find("Three").transform.Find("ingredientImage (1)").transform.GetComponent<MeshRenderer>(), foodIcon.Find("Three").transform.Find("ingredientImage (2)").transform.GetComponent<MeshRenderer>(), foodIcon.Find("Four").transform.Find("ingredientImage (0)").transform.GetComponent<MeshRenderer>(), foodIcon.Find("Four").transform.Find("ingredientImage (1)").transform.GetComponent<MeshRenderer>(), foodIcon.Find("Four").transform.Find("ingredientImage (2)").transform.GetComponent<MeshRenderer>(), foodIcon.Find("Four").transform.Find("ingredientImage (3)").transform.GetComponent<MeshRenderer>() };
        if(slot.Length == 0) {
            foreach(MeshRenderer i in foodIcons) {
                i.enabled = false;
            }
        }
        if(slot.Length == 1) {
            for(int f = 0; f < 1; f++) {
                int temp = Array.IndexOf(_module.allIngredients, slot[f]);
                foodIcons[f].material.SetTexture("_MainTex", _module.ingredientImages[temp]);
                foodIcons[f].enabled = true;
            }
        }
    }
    public override string[] Interact(string[] hands) {
        string[] temp;
        if(slot.Length == 0 && hands.Length == 1) {
            if(Array.IndexOf(uncut, hands[0]) != -1) {
                slot = hands;
                updateText();
                //_module.log(string.Format("Put {0} onto the cutter.", hands));
                _module.log($"Put {hands[0]} onto the cutter.");
                return new string[0];
            }
            _module.log("Invalid Cutter Item.");
            return hands;
        }
        if(slot.Length == 0) {
            if(hands.Length == 0) {
                _module.log("Holding nothing and nothing on the cutting board");
                return hands;
            }
            _module.log("Can only put one item on the cutter.");
            return hands;
        }
        if(hands.Length + slot.Length > 4) {
            _module.log("Hands are full");
            return hands;
        }
        if(hands.Length == 0) {
            _module.log($"Took {slot[0]} off the cutter.");
            temp = new string[hands.Length + 1];
            for(int i = 0; i < hands.Length; i++) {
                temp[i] = hands[i];
            }
            temp[hands.Length] = slot[0];
            slot = new string[0];
            updateText();
            timer = 0;
            return temp;
        }
        foreach(string i in hands.Concat(slot).ToArray()) {
            if(_module.uncombinable.Contains(i)) {
                _module.log($"{i} is uncombinable");
                return hands;
            }
        }
        _module.log($"Took {slot[0]} off the cutter.");
        temp = new string[hands.Length + 1];
        for(int i = 0; i < hands.Length; i++) {
            temp[i] = hands[i];
        }
        temp[hands.Length] = slot[0];
        slot = new string[0];
        updateText();
        timer = 0;
        return temp;
    }
    public override void updoot () {
        try {
            if(Array.IndexOf(uncut, slot[0]) != -1) {
                timer += Time.deltaTime;
            }
        } catch { }
        if(timer >= 15) {
            _module.log($"{slot} is done cutting");
            slot[0] = cut[Array.IndexOf(uncut, slot[0])];
            updateText();
            timer = 0;
        }
        MeshRenderer currentMesh = _module.stations[_number].transform.Find("progressBar").transform.GetComponent<MeshRenderer>();
        if(timer > 0) {
            currentMesh.enabled = true;
            _module.stations[_number].transform.Find("progressBar").transform.GetComponent<MeshRenderer>().material.SetTextureOffset("_MainTex", new Vector2(0, timer/30));
        } else { currentMesh.enabled = false; }
    }
}