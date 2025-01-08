using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class Pan : Station {
    public Pan(Overcooked module, int number) { _module = module; _number = number; }
    private int _number;
    string[] uncooked = { "cutBeef", "cutMushroom", "cutTomato", "cutFish", "cutShrimp", "cutChicken" };
    string[] cooked = { "cookedBeef", "cookedMushroom", "cookedTomato", "cookedFish", "cookedShrimp", "cookedChicken" };
    public new string[] slot = new string[0];
    public new int Image = 3;
    public new string Color = "P";
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
        if(slot.Length == 0 && hands.Length == 1) {
            if(Array.IndexOf(uncooked, hands[0]) != -1) {
                slot = hands;
                updateText();
                //_module.log(string.Format("Put {0} onto the cutter.", hands));
                _module.log($"Put {hands[0]} into the pan.");
                return new string[0];
            }
            _module.log("Invalid Pan Item.");
            return hands;
        }
        if(slot.Length == 0) {
            if(hands.Length == 0) {
                _module.log("Holding nothing and nothing in the pan");
                return hands;
            }
            _module.log("Can only put one item in the pan.");
            return hands;
        }
        if(hands.Length + slot.Length > 4) {
            _module.log("Hands are full");
            return hands;
        }
        if(hands.Length != 0) {
            foreach(string i in hands.Concat(slot).ToArray()) {
                if(_module.uncombinable.Contains(i)) {
                    _module.log($"{i} is uncombinable");
                    return hands;
                }
            }
        }
        _module.log($"Took {slot[0]} out of the pan.");
        string[] temp = new string[hands.Length + 1];
        for(int i = 0; i < hands.Length; i++) {
            temp[i] = hands[i];
        }
        temp[hands.Length] = slot[0];
        slot = new string[0];
        updateText();
        timer = 0;
        burning = 0;
        return temp;
    }
    public override void updoot() {
        try {
            if(Array.IndexOf(uncooked, slot[0]) != -1) {
                timer += Time.deltaTime;
            }
        }
        catch { }
        try {
            if(Array.IndexOf(cooked, slot[0]) != -1) {
                timer += Time.deltaTime;
            }
        }
        catch { }
        if(timer >= 15 && burning == 0) {
            _module.log($"{slot} is done cooking");
            slot[0] = cooked[Array.IndexOf(uncooked, slot[0])];
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
            _module.Strike($"{slot[0]} burned in pan.");
        }
        if(timer >= 30 && _module.TPStrikeTimer) {
            burning = 100;
            _module.Strike($"{slot[0]} burned in pan.");
        }
        MeshRenderer currentMesh = _module.stations[_number].transform.Find("progressBar").transform.GetComponent<MeshRenderer>();
        if(timer > 0 && timer < 15) {
            currentMesh.enabled = true;
            _module.stations[_number].transform.Find("progressBar").transform.GetComponent<MeshRenderer>().material.SetTextureOffset("_MainTex", new Vector2(0, timer / 30));
        } else { currentMesh.enabled = false; }
    }
}
