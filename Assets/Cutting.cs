using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cutting : Station {
    readonly string[] uncut = { "fish", "shrimp" };
    readonly string[] cut = { "cutFish", "cutShrimp" };
    public new string[] slot = new string[1];
    public new float timer = 0;
    public Cutting(Overcooked module) { _module = module; }
    private Overcooked _module;
    public override string[] Interact(string[] hands) {
        if(slot.Length == 1 && slot[0] == null) {
            if(Array.IndexOf(uncut, hands[0]) != -1) {
                slot = hands;
                //_module.log(string.Format("Put {0} onto the cutter.", hands));
                _module.log($"Put {hands[0]} onto the cutter.");
                return new string[1];
            }
            _module.log("Invalid Cutter Item.");
            return hands;
        }
        if(hands.Length == 1 && hands[0] == null) {
            _module.log($"Took {slot[0]} off the cutter.");
            string[] temp = slot;
            slot = new string[1];
            return temp;
        }
        _module.log("Hands Full, can't take from cutter");
        return hands;
    }
    public override void updoot () {
        if(Array.IndexOf(uncut, slot[0]) != -1) {
            timer += Time.deltaTime;
        }
        if(timer >= 15) {
            _module.log($"{slot} is done cutting");
            slot[0] = cut[Array.IndexOf(uncut, slot[0])];
            timer = 0;
        }
    }
}