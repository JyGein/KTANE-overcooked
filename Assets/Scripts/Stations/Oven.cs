using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class Oven : Station {
    public Oven(Overcooked module, int number) { _module = module; _number = number; }
    private int _number;
    string[] uncooked = { "cutDough", "cutCheese", "cutTomato", "cutPepperoni", "cutChicken", "cutOlive" };
    string[] cooked = { "bakedDough", "bakedCheese", "bakedTomato", "bakedPepperoni", "bakedChicken", "bakedOlive" };
    public new string[] slot = new string[0];
    public new int Image = 5;
    public new string Color = "C";
    public new float timer = 0;
    private int burning = 0;
    public override void startup() {
        updateText();
        _module.stations[_number].transform.Find("stationImage").transform.GetComponent<MeshRenderer>().material = _module.stationMaterials[Image];
        if(_module.colorblindtime) {
            _module.colorblindTexts[_number].transform.GetComponent<TextMesh>().text = Color;
        }
        /*uncooked = new string[] { "rice" };
        cooked = new string[] { "cookedRice" };
        slot = new string[0];
        Image = 2;
        timer = 0;
        burning = 0;*/
    }
    public void updateText() {
        //_module.stations[_number].transform.Find("stationText").transform.GetComponent<TextMesh>().text = string.Join(" ", slot);
        Transform foodIcon = _module.stations[_number].transform.Find("FoodIcons").transform;
        MeshRenderer[] foodIcons = new MeshRenderer[] { foodIcon.Find("One").transform.Find("ingredientImage (0)").transform.GetComponent<MeshRenderer>(), foodIcon.Find("Two").transform.Find("ingredientImage (0)").transform.GetComponent<MeshRenderer>(), foodIcon.Find("Two").transform.Find("ingredientImage (1)").transform.GetComponent<MeshRenderer>(), foodIcon.Find("Three").transform.Find("ingredientImage (0)").transform.GetComponent<MeshRenderer>(), foodIcon.Find("Three").transform.Find("ingredientImage (1)").transform.GetComponent<MeshRenderer>(), foodIcon.Find("Three").transform.Find("ingredientImage (2)").transform.GetComponent<MeshRenderer>(), foodIcon.Find("Four").transform.Find("ingredientImage (0)").transform.GetComponent<MeshRenderer>(), foodIcon.Find("Four").transform.Find("ingredientImage (1)").transform.GetComponent<MeshRenderer>(), foodIcon.Find("Four").transform.Find("ingredientImage (2)").transform.GetComponent<MeshRenderer>(), foodIcon.Find("Four").transform.Find("ingredientImage (3)").transform.GetComponent<MeshRenderer>() };
        foreach(MeshRenderer i in foodIcons) {
                i.enabled = false;
        }
        if(slot.Length == 1) {
            for(int f = 0; f < 1; f++) {
                int temp = Array.IndexOf(_module.allIngredients, slot[f]);
                foodIcons[f].material.SetTexture("_MainTex", _module.ingredientImages[temp]);
                foodIcons[f].enabled = true;
            }
        }
        if(slot.Length == 2) {
            for(int f = 0; f < 2; f++) {
                int temp = Array.IndexOf(_module.allIngredients, slot[f]);
                foodIcons[f + 1].material.SetTexture("_MainTex", _module.ingredientImages[temp]);
                foodIcons[f + 1].enabled = true;
            }
        }
        if(slot.Length == 3) {
            for(int f = 0; f < 3; f++) {
                int temp = Array.IndexOf(_module.allIngredients, slot[f]);
                foodIcons[f + 3].material.SetTexture("_MainTex", _module.ingredientImages[temp]);
                foodIcons[f + 3].enabled = true;
            }
        }
        if(slot.Length == 4) {
            for(int f = 0; f < 4; f++) {
                int temp = Array.IndexOf(_module.allIngredients, slot[f]);
                foodIcons[f + 6].material.SetTexture("_MainTex", _module.ingredientImages[temp]);
                foodIcons[f + 6].enabled = true;
            }
        }
    }
    public override string[] Interact(string[] hands) {
        string[] temp = new string[slot.Length + hands.Length];
        if(slot.Length == 0 && hands.Length == 0) {
            _module.log("Holding nothing and nothing in the oven.");
            return hands;
        }
        if(slot.Length + hands.Length > 4) { _module.log($"No space in the oven."); return hands; }
        if(slot.Length == 0) {
            if(!hands.Contains("cutDough")) {
                _module.log("You can't cook a pizza without dough.");
                return hands;
            }
            foreach(string i in hands) {
                if(!uncooked.Contains(i)) {
                    _module.log($"{i} can't go in the oven.");
                    return hands;
                }
            }
            _module.log($"Put {hands.arrayToString()} in the oven.");
            timer = 0;
            burning = 0;
            slot = hands;
            updateText();
            return new string[0];
        }
        if(hands.Length == 0) {
            _module.log($"Took {slot.arrayToString()} out of the oven.");
            temp = slot;
            slot = new string[0];
            updateText();
            timer = 0;
            burning = 0;
            return temp;
        }
        if(slot.Contains("bakedDough")) {
            _module.log("Cannot combine bakedDough.");
            return hands;
            //cannot cum
            //projectile dysfunction???
        }
        foreach(string i in hands) {
            if(!uncooked.Contains(i)) {
                _module.log($"{i} can't go in the oven.");
                return hands;
            }
        }
        _module.log($"Put {hands.arrayToString()} in the oven.");
        timer = 0;
        burning = 0;
        slot = slot.Concat(hands).ToArray();
        updateText();
        return new string[0];
    }
    public override void updoot() {
        try {
            if(Array.IndexOf(uncooked, slot[0]) != -1) {
                timer += Time.deltaTime;
            }
        } catch { }
        try {
            if(Array.IndexOf(cooked, slot[0]) != -1) {
                timer += Time.deltaTime;
            }
        } catch { }
        if(timer >= 15 && burning == 0) {
            _module.log($"{slot.arrayToString()} is done baking");
            for(int i=0; i<slot.Length; i++) {
                slot[i] = cooked[Array.IndexOf(uncooked, slot[i])];
            }
            updateText();
            burning = 1;
            _module.Beep(_module.stations[_number]);
        }
        if(timer >= (15 + burning)) {
            burning++;
            _module.Beep(_module.stations[_number]);
        }
        if(timer >= 20 && !_module.TPStrikeTimer) {
            burning = 100;
            _module.Strike($"{slot.arrayToString()} burned in Oven.");
        }
        if(timer >= 30 && _module.TPStrikeTimer) {
            burning = 100;
            _module.Strike($"{slot.arrayToString()} burned in Oven.");
        }
        MeshRenderer currentMesh = _module.stations[_number].transform.Find("progressBar").transform.GetComponent<MeshRenderer>();
        if(timer > 0 && timer < 15) {
            currentMesh.enabled = true;
            _module.stations[_number].transform.Find("progressBar").transform.GetComponent<MeshRenderer>().material.SetTextureOffset("_MainTex", new Vector2(0, timer / 30));
        } else { currentMesh.enabled = false; }
    }
}
