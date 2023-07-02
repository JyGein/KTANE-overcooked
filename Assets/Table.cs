using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class Table : Station {
    public Table(Overcooked module, int number) { _module = module; _number = number; }
    private Overcooked _module;
    private int _number;
    public new string[] slot = new string[0];
    public new int Image = 0;
    public new string Color = "N";
    public override void startup() {
        updateText();
        _module.stations[_number].transform.Find("stationImage").transform.GetComponent<MeshRenderer>().material = _module.stationMaterials[Image];
    }
    public override string[] Interact(string[] hands) {
        string[] temp = new string[slot.Length + hands.Length];
        string[] temp2 = new string[4];
        if(slot.Length == 0 && hands.Length == 0) {
            _module.log("Holding nothing and nothing on the counter");
            return hands;
        }
        if(slot.Length + hands.Length > 4) { _module.log($"No space on the Counter."); return hands; }
        if(hands.Length == 0) {
            for(int i = 0; i < 4; i++) {
                try {
                    temp2[i] = slot[i];
                }
                catch {
                    temp2[i] = "";
                }
            }
            _module.log($"Took {temp2[0] + temp2[1] + temp2[2] + temp2[3]} off the counter.");
            temp = slot;
            slot = new string[0];
            updateText();
            timer = 0;
            return temp;
        }
        if(slot.Length != 0) { 
            foreach(string i in hands.Concat(slot).ToArray()) {
                if(_module.uncombinable.Contains(i)) {
                    _module.log($"{i} is uncombinable");
                    return hands;
                }
            }
        }
        for(int i=0; i<4; i++) {
            try {
                temp2[i] = hands[i];
            } catch {
                temp2[i] = "";
            }
        }
        _module.log($"Put {temp2[0] + temp2[1] + temp2[2] + temp2[3]} on the counter.");
        for(int i = 0; i < slot.Length; i++) {
            temp[i] = slot[i];
        }
        for(int i = 0; i < hands.Length; i++) {
            temp[i + slot.Length] = hands[i];
        }
        slot = temp;
        updateText();
        return new string[0];
    }
    public override void updoot() {
        MeshRenderer currentMesh = _module.stations[_number].transform.Find("progressBar").transform.GetComponent<MeshRenderer>();
        if(timer > 0 && timer < 15) {
            currentMesh.enabled = true;
            _module.stations[_number].transform.Find("progressBar").transform.GetComponent<MeshRenderer>().material.SetTextureOffset("_MainTex", new Vector2(0, timer / 30));
        } else { currentMesh.enabled = false; }
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
                foodIcons[f+1].material.SetTexture("_MainTex", _module.ingredientImages[temp]);
                foodIcons[f+1].enabled = true;
            }
        }
        if(slot.Length == 3) {
            for(int f = 0; f < 3; f++) {
                int temp = Array.IndexOf(_module.allIngredients, slot[f]);
                foodIcons[f+3].material.SetTexture("_MainTex", _module.ingredientImages[temp]);
                foodIcons[f+3].enabled = true;
            }
        }
        if(slot.Length == 4) {
            for(int f = 0; f < 4; f++) {
                int temp = Array.IndexOf(_module.allIngredients, slot[f]);
                foodIcons[f+6].material.SetTexture("_MainTex", _module.ingredientImages[temp]);
                foodIcons[f+6].enabled = true;
            }
        }
    }
}
