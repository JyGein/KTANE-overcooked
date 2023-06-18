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
    public KMSelectable trash;
    public KMSelectable[] stations;
    public KMSelectable output;
    public KMSelectable[] ingredientBoxes;
    public Transform[] orderBackgrounds;
    public Texture[] orderImages;
    public Texture[] ingredientImages;
    public Transform[] orderDisplays;
    public Transform handsDisplay;
    public Material[] stationMaterials;

    static int ModuleIdCounter = 1;
    int ModuleId;
    bool ModuleSolved;
    bool ModuleSolved2;

    string[] hands = new string[0];
    private Place sushi;
    private Place[] places;
    public Place place;
    private string[][] orders;
    private int submitted = 0;
    private readonly string[][] allOrders = new string[][] { new string[] { "cutFish" }, new string[] { "cutShrimp" } };
    public readonly string[] allIngredients = new string[] { "fish", "shrimp", "cucumber", "rice", "seaweed", "cutFish", "cutShrimp", "cutCucumber", "cookedRice" };

    void Awake () {
        ModuleId = ModuleIdCounter++; 
        GetComponent<KMBombModule>().OnActivate += Activate;
        sushi = new Place(new Box[] { new Box(this, 0, "fish"), new Box(this, 1, "shrimp"), new Box(this, 2, "cucumber"), new Box(this, 3, "rice"), new Box(this, 4, "seaweed") }, new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Table(this, 2), new Table(this, 3) }, new string[][] { new string[] { "cutFish" }, new string[] { "cutShrimp" } }, 7);
        /*
        foreach (KMSelectable object in keypad) {
            object.OnInteract += delegate () { keypadPress(object); return false; };
        }shapuwu
        */

         //button.OnInteract += delegate () { buttonPress(); return false; };

    }

    void OnDestroy () { //Shit you need to do when the bomb ends
        
    }

    void Activate () { //Shit that should happen when the bomb arrives (factory)/Lights turn on

    }

    void Start () { //Shit
        places = new Place[] { sushi }; 
        place = places[0];
        hands = new string[0];
        orders = new string[place.OrderAmount][];
        for(int i=0; i<place.OrderAmount; i++) {
            orders[i] = place.Orders[Rnd.Range(0, place.Orders.Length)];
        }
        for(int i=0; i<3; i++) {
            int temp = -1;
            int count = 0;
            foreach(string[] order in allOrders) {
                bool check = false;
                if(order.Length != orders[i].Length) { check = true; }
                foreach(string food in orders[i]) {
                    if(Array.IndexOf(order, food) == -1) { check = true; }
                }
                if(!check) {
                    temp = count;
                }
                count++;
            }
            orderDisplays[i].GetComponent<MeshRenderer>().material.SetTexture("_MainTex", orderImages[temp]);
        }

        /*log(string.Join(" ", place.Ingredients));
        log(place.Stations[0].ToString());
        log(string.Join(" ", place.Orders));*/
        for(int i = 0; i < place.Ingredients.Length; i++) {
            int dummy = i;
            ingredientBoxes[i].transform.GetComponent<MeshRenderer>().enabled = true;
            //ingredientBoxes[i].transform.Find("ingredientText").transform.GetComponent<TextMesh>().text = place.Ingredients[i]._type;
            place.Ingredients[i].startup();
            ingredientBoxes[i].OnInteract += delegate () { ingredientPress(ingredientBoxes[dummy], place.Ingredients[dummy]); return false; };

        }
        trash.OnInteract += delegate () { if(ModuleSolved) { return false; } trash.AddInteractionPunch(); Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, trash.transform); hands = new string[0]; log("Selected Trash, Emptied Hands."); return false; };
        for(int i = 0; i < place.Stations.Length; i++) {
            int dummy = i;
            stations[i].transform.GetComponent<MeshRenderer>().enabled = true;
            stations[i].transform.Find("stationImage").transform.GetComponent<MeshRenderer>().enabled = true;
            place.Stations[i].startup();
            stations[i].OnInteract += delegate () { stationPress(stations[dummy], place.Stations[dummy]); return false; };
        }
        output.OnInteract += delegate () { if(ModuleSolved) { return false; } trash.AddInteractionPunch(); Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, trash.transform); outputCheck(); return false; };
        foreach(string[] i in orders) {
            foreach(string f in i) {
                log(f);
            }
        }
    }

    void outputCheck() {
        foreach(string food in hands) {
            if(Array.IndexOf(orders[submitted], food) == -1) {
                hands = new string[0];
                Strike("Incorrect!");
                return;
            }
        }
        hands = new string[0];
        submitted++;
        log($"Order#{submitted} Complete");
        if(submitted==place.OrderAmount) {
            Solve();
        }
        for(int i = 0+submitted; i < 3+submitted; i++) {
            try{
                int temp = -1;
                int count = 0;
                foreach(string[] order in allOrders) {
                    bool check = false;
                    if(order.Length != orders[i].Length) { check = true; }
                    foreach(string food in orders[i]) {
                        if(Array.IndexOf(order, food) == -1) { check = true; }
                    }
                    if(!check) {
                        temp = count;
                    }
                    count++;
                }
                orderDisplays[i-submitted].GetComponent<MeshRenderer>().material.SetTexture("_MainTex", orderImages[temp]);
            } catch {
                orderDisplays[i-submitted].GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    void ingredientPress(KMSelectable box, Box type) {   
        if(ModuleSolved) { return; }
        box.AddInteractionPunch();
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, box.transform);
        hands = type.Interact(hands);
    }

    void stationPress(KMSelectable station, Station type) { //Shit that happens when the station button gets pressed
        if(ModuleSolved) { return; }
        station.AddInteractionPunch();
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, station.transform);
        hands = type.Interact(hands);
    }

    void Update () { //Shit that happens at any point after initialization
        if(ModuleSolved2) { return; }
        //handText.text = string.Join(" ", hands);
        Transform foodIcon = handsDisplay.Find("FoodIcons").transform;
        MeshRenderer[] foodIcons = new MeshRenderer[] { foodIcon.Find("One").transform.Find("ingredientImage (0)").transform.GetComponent<MeshRenderer>(), foodIcon.Find("Two").transform.Find("ingredientImage (0)").transform.GetComponent<MeshRenderer>(), foodIcon.Find("Two").transform.Find("ingredientImage (1)").transform.GetComponent<MeshRenderer>(), foodIcon.Find("Three").transform.Find("ingredientImage (0)").transform.GetComponent<MeshRenderer>(), foodIcon.Find("Three").transform.Find("ingredientImage (1)").transform.GetComponent<MeshRenderer>(), foodIcon.Find("Three").transform.Find("ingredientImage (2)").transform.GetComponent<MeshRenderer>(), foodIcon.Find("Four").transform.Find("ingredientImage (0)").transform.GetComponent<MeshRenderer>(), foodIcon.Find("Four").transform.Find("ingredientImage (1)").transform.GetComponent<MeshRenderer>(), foodIcon.Find("Four").transform.Find("ingredientImage (2)").transform.GetComponent<MeshRenderer>(), foodIcon.Find("Four").transform.Find("ingredientImage (3)").transform.GetComponent<MeshRenderer>() };
        foreach(MeshRenderer i in foodIcons) {
            i.enabled = false;
        }
        if(hands.Length == 1) {
            for(int f = 0; f < 1; f++) {
                int temp = Array.IndexOf(allIngredients, hands[f]);
                foodIcons[f].material.SetTexture("_MainTex", ingredientImages[temp]);
                foodIcons[f].enabled = true;
            }
        }
        if(hands.Length == 2) {
            for(int f = 0; f < 2; f++) {
                int temp = Array.IndexOf(allIngredients, hands[f]);
                foodIcons[f + 1].material.SetTexture("_MainTex", ingredientImages[temp]);
                foodIcons[f + 1].enabled = true;
            }
        }
        if(hands.Length == 3) {
            for(int f = 0; f < 3; f++) {
                int temp = Array.IndexOf(allIngredients, hands[f]);
                foodIcons[f + 3].material.SetTexture("_MainTex", ingredientImages[temp]);
                foodIcons[f + 3].enabled = true;
            }
        }
        if(hands.Length == 4) {
            for(int f = 0; f < 4; f++) {
                int temp = Array.IndexOf(allIngredients, hands[f]);
                foodIcons[f + 6].material.SetTexture("_MainTex", ingredientImages[temp]);
                foodIcons[f + 6].enabled = true;
            }
        }
        for(int i = 0; i < place.Stations.Length; i++) {
            place.Stations[i].updoot();
        }
        if(ModuleSolved) { ModuleSolved2 = true; }
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
