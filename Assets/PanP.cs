﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class PanP : Station {
    public PanP(Overcooked module, int number) { _module = module; _number = number; }
    private Overcooked _module;
    private int _number;
    string[] uncooked = { "mixedFlour", "mixedEgg", "mixedChocolate", "mixedBlueberry", "mixedStrawberry" };
    string[] cooked = { "cookedFlour", "cookedEgg", "cookedChocolate", "cookedBlueberry", "cookedStrawberry" };
    public new string[] slot = new string[0];
    public new int Image = 3;
    public new float timer = 0;
    private int burning = 0;
    public override void startup() {
        updateText();
        _module.stations[_number].transform.Find("stationImage").transform.GetComponent<MeshRenderer>().material = _module.stationMaterials[Image];
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
    /*
        foreach(string i in hands) {
            if(!unmixed.Contains(i)) {
                _module.log($"{i} can't go in the oven.");
                return hands;
            }
        }
    */
    public override string[] Interact(string[] hands) {
        string[] temp = new string[slot.Length + hands.Length];
        string temp2 = "";
        if(slot.Length == 0 && hands.Length == 0) {
            _module.log("Holding nothing and nothing in the Pan.");
            return hands;
        }
        if(slot.Length + hands.Length > 4) { _module.log($"No space in the pan."); return hands; }
        if(hands.Length > 0 && slot.Length > 0) {
            _module.log($"Holding somthing and somthing in the Pan.");
            return hands;
        }
        if(hands.Length == 0) {
            foreach(string i in slot) {
                temp2 += i;
            }
            _module.log($"Took {temp2} out of the pan.");
            temp = slot;
            slot = new string[0];
            updateText();
            timer = 0;
            burning = 0;
            return temp;
        }
        foreach(string i in hands) {
            if(!uncooked.Contains(i)) {
                _module.log($"{i} can't go in the pan.");
                return hands;
            }
        }
        foreach(string i in hands) {
            temp2 += i;
        }
        _module.log($"Put {temp2} in the pan.");
        timer = 0;
        burning = 0;
        slot = slot.Concat(hands).ToArray();
        updateText();
        return new string[0];
    }
    public override void updoot() {
        string temp2;
        if(slot.Length > 0) {
            timer += Time.deltaTime;
        }
        if(timer >= 15 && burning == 0) {
            temp2 = "";
            foreach(string i in slot) {
                temp2 += i;
            }
            _module.log($"{temp2} is done cooking");
            for(int i = 0; i < slot.Length; i++) {
                try {
                    slot[i] = cooked[Array.IndexOf(uncooked, slot[i])];
                } catch {}
            }
            updateText();
            burning = 1;
            _module.Beep(_module.stations[_number]);
        }
        if(timer >= (15 + burning)) {
            burning++;
            _module.Beep(_module.stations[_number]);
        }
        if((timer >= 20 && !_module.TPStrikeTimer) || (timer >= 30 && _module.TPStrikeTimer)) {
            burning = 100;
            temp2 = "";
            foreach(string i in slot) {
                temp2 += i;
            }
            _module.Strike($"{temp2} burned in the Pan.");
        }
        MeshRenderer currentMesh = _module.stations[_number].transform.Find("progressBar").transform.GetComponent<MeshRenderer>();
        if(timer > 0 && timer < 15) {
            currentMesh.enabled = true;
            _module.stations[_number].transform.Find("progressBar").transform.GetComponent<MeshRenderer>().material.SetTextureOffset("_MainTex", new Vector2(0, timer / 30));
        } else { currentMesh.enabled = false; }
    }
}