using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class Overcooked : MonoBehaviour {

    public KMBombInfo Bomb;
    public KMAudio Audio;
    public TextMesh handText;
    public KMSelectable trash;
    public KMSelectable[] stations;

    static int ModuleIdCounter = 1;
    int ModuleId;
    bool ModuleSolved;

    string[] hands = new string[0];
    private Place sushi;
    private Place[] places;
    private Place place;

    void Awake () {
        ModuleId = ModuleIdCounter++; 
        GetComponent<KMBombModule>().OnActivate += Activate;
        sushi = new Place(new string[] { "fish", "shrimp" }, new Station[] { new Cutting(this), new Cutting(this) }, new string[] { "cutFish", "cutShrimp" });
        /*
        foreach (KMSelectable object in keypad) {
            object.OnInteract += delegate () { keypadPress(object); return false; };
        }
        */

        //button.OnInteract += delegate () { buttonPress(); return false; };

    }

    void OnDestroy () { //Shit you need to do when the bomb ends
        
    }

    void Activate () { //Shit that should happen when the bomb arrives (factory)/Lights turn on

    }

    void Start () { //Shit
        places = new Place[] { sushi };
        place = places[Rnd.Range(0, places.Length)];
        hands = new string[] { "fish" };
        /*log(string.Join(" ", place.Ingredients));
        log(place.Stations[0].ToString());
        log(string.Join(" ", place.Orders));*/
        trash.OnInteract += delegate () { trash.AddInteractionPunch(); Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, trash.transform); hands = new string[0]; log("Selected Trash, Emptied Hands."); return false; };
        for(int i=0; i<place.Stations.Length; i++) {
            int dummy = i;
            stations[i].transform.GetComponent<MeshRenderer>().enabled = true;
            stations[i].OnInteract += delegate () { stationPress(stations[dummy], place.Stations[dummy]); return false; };
        }
    }

    void stationPress(KMSelectable station, Station type) { //Shit that happens when the station button gets pressed
        station.AddInteractionPunch();
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, station.transform);
        hands = type.Interact(hands);
    }

    void Update () { //Shit that happens at any point after initialization
        handText.text = string.Join(" ", hands);
        for(int i = 0; i < place.Stations.Length; i++) {    
            place.Stations[i].updoot();
            stations[i].transform.Find("stationText").transform.GetComponent<TextMesh>().text = string.Join(" ", place.Stations[i].slot); //place.Stations[i].slot = { "fish" }
            log(stations[i].transform.Find("stationText").transform.GetComponent<TextMesh>().text);
            MeshRenderer currentMesh = stations[i].transform.Find("progressBar").transform.GetComponent<MeshRenderer>();
            if(place.Stations[i].timer >= 0) {
                currentMesh.enabled = true;
                stations[i].transform.Find("progressBar").transform.GetComponent<MeshRenderer>().material.SetTextureOffset("_MainTex", new Vector2(place.Stations[i].timer/30, 0));
            } else { currentMesh.enabled = false; }
        }
    }

    public void log(string message) {
        Debug.Log($"[Overcooked #{ModuleId}] {message}");
    }

    void Solve () {
        GetComponent<KMBombModule>().HandlePass();
        Debug.LogFormat("[Overcooked #{0}] Good Job! Module Solved.", ModuleId);
        ModuleSolved = true;
    }

    public void Strike (string reason) {
        GetComponent<KMBombModule>().HandleStrike();
        Debug.LogFormat("[Overcooked #{0}] {1} Strike Issued.", ModuleId, reason);
    }

#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use !{0} to do something.";
#pragma warning restore 414

    KMSelectable[] ProcessTwitchCommand (string Command) {
        return null;
    }

    KMSelectable[] TwitchHandleForcedSolve () {
        return null;
    }
}
