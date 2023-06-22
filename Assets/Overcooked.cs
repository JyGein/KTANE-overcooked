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
    public Transform moduleMaterial;

    static int ModuleIdCounter = 1;
    int ModuleId;
    bool ModuleSolved;
    bool ModuleSolved2;

    string[] hands = new string[0];
    private Place sushi0;
    private Place sushi1;
    private Place sky0;
    private Place sky1;
    private Place sky2;
    private Place rapids0;
    private Place mines0;
    private Place mines1;
    private Place magic0;
    private Place magic1;
    private Place magic2;
    private Place magic3;
    private Place martian0;
    private Place kevel0;
    private Place[] places;
    public Place place;
    private string[][] orders;
    private int submitted = 0;
    private readonly string[][] allOrders = new string[][] { new string[] { "cutFish" }, new string[] { "cutShrimp" }, new string[] { "seaweed", "cookedRice", "cutFish" }, new string[] { "seaweed", "cookedRice", "cutCucumber" }, new string[] { "seaweed", "cookedRice", "cutFish", "cutCucumber" }, new string[] { "cookedPasta", "cookedBeef" }, new string[] { "cookedPasta", "cookedMushroom" }, new string[] { "cookedPasta", "cookedTomato" }, new string[] { "cookedPasta", "cookedFish", "cookedShrimp" }, new string[] { "cutLettuce" }, new string[] { "cutLettuce", "cutTomato" }, new string[] { "cutLettuce", "cutTomato", "cutCucumber" }, new string[] { "friedPotato" }, new string[] { "friedChicken" }, new string[] { "friedPotato", "friedChicken" }, new string[] { "cookedRice", "tortilla", "cookedMushroom" }, new string[] { "cookedRice", "tortilla", "cookedBeef" }, new string[] { "cookedRice", "tortilla", "cookedChicken" }, new string[] { "bun", "cookedBeef" }, new string[] { "bun", "cookedBeef", "cutCheese" }, new string[] { "bun", "cookedBeef", "cutLettuce", "cutCheese" }, new string[] { "bun", "cookedBeef", "cutLettuce", "cutTomato" }, new string[] { "cookedDough", "cookedCheese", "cookedTomato" }, new string[] { "cookedDough", "cookedCheese", "cookedTomato", "cookedPepperoni" }, new string[] { "cookedDough", "cookedCheese", "cookedTomato", "cookedChicken" }, new string[] { "cookedDough", "cookedCheese", "cookedTomato", "cookedOlive" }, new string[] { "cookedFlour", "cookedEgg" }, new string[] { "cookedFlour", "cookedEgg", "cookedChocolate" }, new string[] { "cookedFlour", "cookedEgg", "cookedStrawberry" }, new string[] { "cookedFlour", "cookedEgg", "cookedBlueberry" }, new string[] { "cookedFlour", "cookedEgg", "cookedHoney" }, new string[] { "cookedFlour", "cookedEgg", "cookedHoney", "cookedChocolate" }, new string[] { "cookedFlour", "cookedEgg", "cookedHoney", "cookedCarrot" }, new string[] { "cookedFish" }, new string[] { "cookedFlour", "cookedBeef" }, new string[] { "cookedFlour", "cookedCarrot" }, new string[] { "cookedFlour", "cookedShrimp" } };
    public readonly string[] allIngredients = new string[] { "fish", "shrimp", "cucumber", "rice", "seaweed", "cutFish", "cutShrimp", "cutCucumber", "cookedRice", "pasta", "mushroom", "beef", "tomato", "cutTomato", "cutBeef", "cutMushroom", "cookedFish", "cookedShrimp", "cookedMushroom", "cookedBeef", "cookedTomato", "cookedPasta", "lettuce", "cutLettuce", "tortilla", "potato", "chicken", "cutPotato", "cutChicken", "friedPotato", "friedChicken", "cookedChicken", "bun", "cheese", "cutCheese", "dough", "cutDough", "cookedDough", "pepperoni", "cutPepperoni", "cookedCheese", "cookedPepperoni", "flour", "egg", "mixedFlour", "mixedEgg", "chocolate", "blueberry", "strawberry", "cutChocolate", "cutBlueberry", "cutStrawberry", "mixedChocolate", "mixedBlueberry", "mixedStrawberry", "cookedFlour", "cookedEgg", "cookedChocolate", "cookedBlueberry", "cookedStrawberry", "honey", "carrot", "cutHoney", "cutCarrot", "mixedHoney", "mixedCarrot", "cookedHoney", "cookedCarrot", "mixedBeef", "mixedShrimp" };
    private string beep = "overcooked-beep-(1) (mp3cut.net)(1)";
    public float minutesTime;
    public readonly string[] uncombinable = new string[] { "cookedDough", "fish", "shrimp", "cucumber", "rice", "pasta", "mushroom", "beef", "tomato", "cutBeef", "cutMushroom", "lettuce", "potato", "chicken", "cutPotato", "cutChicken", "cheese", "dough", "pepperoni", "flour", "egg", "mixedFlour", "mixedEgg", "chocolate", "blueberry", "strawberry", "mixedChocolate", "mixedBlueberry", "mixedStrawberry", "cookedFlour", "cookedEgg", "cookedChocolate", "cookedBlueberry", "cookedStrawberry", "honey", "carrot", "mixedHoney", "mixedCarrot", "cookedHoney", "cookedCarrot", "mixedBeef", "mixedShrimp" };
    public bool TPStrikeTimer;

    void Awake () {
        ModuleId = ModuleIdCounter++; 
        GetComponent<KMBombModule>().OnActivate += Activate;
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
        submitted = 0;
        minutesTime = Bomb.GetTime() / 60;
        float calcTime = minutesTime / 2 * 7 >= 45 ? 45 : minutesTime / 2 * 7;
        sushi0 = new Place(
            new Box[] { new Box(this, 0, "lettuce"), new Box(this, 1, "tomato"), new Box(this, 2, "cucumber"), new Box(this, 3, "fish"), new Box(this, 4, "shrimp"), new Box(this, 5, "rice"), new Box(this, 6, "seaweed") },
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Pot(this, 2), new Pot(this, 3), new Table(this, 4), new Table(this, 5) }, 
            new string[][] { new string[] { "cutFish" }, new string[] { "cutShrimp" }, new string[] { "seaweed", "cookedRice", "cutFish" }, new string[] { "seaweed", "cookedRice", "cutCucumber" }, new string[] { "seaweed", "cookedRice", "cutFish", "cutCucumber" }, new string[] { "cutFish" }, new string[] { "cutShrimp" }, new string[] { "seaweed", "cookedRice", "cutFish" }, new string[] { "seaweed", "cookedRice", "cutCucumber" }, new string[] { "seaweed", "cookedRice", "cutFish", "cutCucumber" }, new string[] { "cutLettuce" }, new string[] { "cutLettuce", "cutTomato" }, new string[] { "cutLettuce", "cutTomato", "cutCucumber" } }, 
            2.5f);
        sushi1 = new Place(
            new Box[] { new Box(this, 0, "bun"), new Box(this, 1, "beef"), new Box(this, 2, "cucumber"), new Box(this, 3, "fish"), new Box(this, 4, "shrimp"), new Box(this, 5, "rice"), new Box(this, 6, "seaweed") }, 
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Pot(this, 2), new Pot(this, 3), new Table(this, 4), new Table(this, 5), new Pan(this, 6), new Pan(this, 7) }, 
            new string[][] { new string[] { "cutFish" }, new string[] { "cutShrimp" }, new string[] { "seaweed", "cookedRice", "cutFish" }, new string[] { "seaweed", "cookedRice", "cutCucumber" }, new string[] { "seaweed", "cookedRice", "cutFish", "cutCucumber" }, new string[] { "bun", "cookedBeef" }, }, 
            2.5f);
        sky0 = new Place(
            new Box[] { new Box(this, 0, "lettuce"), new Box(this, 1, "tomato"), new Box(this, 2, "cucumber"), new Box(this, 3, "mushroom"), new Box(this, 4, "beef"), new Box(this, 5, "fish"), new Box(this, 6, "shrimp"), new Box(this, 7, "pasta") }, 
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Pan(this, 2), new Pan(this, 3), new Pot(this, 4), new Pot(this, 5), new Table(this, 6), new Table(this, 7) }, 
            new string[][] { new string[] { "cookedPasta", "cookedBeef" }, new string[] { "cookedPasta", "cookedMushroom" }, new string[] { "cookedPasta", "cookedTomato" }, new string[] { "cookedPasta", "cookedFish", "cookedShrimp" }, new string[] { "cookedPasta", "cookedBeef" }, new string[] { "cookedPasta", "cookedMushroom" }, new string[] { "cookedPasta", "cookedTomato" }, new string[] { "cookedPasta", "cookedFish", "cookedShrimp" }, new string[] { "cutLettuce" }, new string[] { "cutLettuce", "cutTomato" }, new string[] { "cutLettuce", "cutTomato", "cutCucumber" } }, 
            2.5f);
        sky1 = new Place(
            new Box[] { new Box(this, 0, "bun"), new Box(this, 1, "cheese"), new Box(this, 2, "lettuce"), new Box(this, 3, "mushroom"), new Box(this, 4, "beef"), new Box(this, 5, "fish"), new Box(this, 6, "shrimp"), new Box(this, 7, "pasta"), new Box(this, 8, "tomato") }, 
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Pan(this, 2), new Pan(this, 3), new Pot(this, 4), new Pot(this, 5), new Table(this, 6), new Table(this, 7) }, 
            new string[][] { new string[] { "cookedPasta", "cookedBeef" }, new string[] { "cookedPasta", "cookedMushroom" }, new string[] { "cookedPasta", "cookedTomato" }, new string[] { "cookedPasta", "cookedFish", "cookedShrimp" }, new string[] { "bun", "cookedBeef" }, new string[] { "bun", "cookedBeef", "cutCheese" }, new string[] { "bun", "cookedBeef", "cutLettuce", "cutCheese" }, new string[] { "bun", "cookedBeef", "cutLettuce", "cutTomato" } }, 
            3.5f);
        sky2 = new Place(
            new Box[] { new Box(this, 0, "bun"), new Box(this, 1, "cheese"), new Box(this, 2, "lettuce"), new Box(this, 3, "beef"), new Box(this, 4, "tomato")},
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Pan(this, 2), new Pan(this, 3), new Table(this, 4), new Table(this, 5) }, 
            new string[][] { new string[] { "bun", "cookedBeef" }, new string[] { "bun", "cookedBeef", "cutCheese" }, new string[] { "bun", "cookedBeef", "cutLettuce", "cutCheese" }, new string[] { "bun", "cookedBeef", "cutLettuce", "cutTomato" } }, 
            2.5f);
        rapids0 = new Place(
            new Box[] { new Box(this, 0, "potato"), new Box(this, 1, "chicken") }, 
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Fryer(this, 2), new Fryer(this, 3), new Table(this, 4), new Table(this, 5) }, 
            new string[][] { new string[] { "friedPotato" }, new string[] { "friedChicken" }, new string[] { "friedPotato", "friedChicken" } }, 
            2f);
        mines0 = new Place(
            new Box[] { new Box(this, 0, "tortilla"), new Box(this, 1, "chicken"), new Box(this, 2, "beef"), new Box(this, 3, "mushroom"), new Box(this, 4, "rice") },
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Pan(this, 2), new Pan(this, 3), new Pot(this, 4), new Pot(this, 5), new Table(this, 6), new Table(this, 7) },
            new string[][] { new string[] { "cookedRice", "tortilla", "cookedMushroom" }, new string[] { "cookedRice", "tortilla", "cookedBeef" }, new string[] { "cookedRice", "tortilla", "cookedChicken" } },
            2.5f);
        mines1 = new Place(
            new Box[] { new Box(this, 0, "bun"), new Box(this, 1, "cheese"), new Box(this, 2, "beef") },
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Pan(this, 2), new Pan(this, 3), new Table(this, 4), new Table(this, 5) },
            new string[][] { new string[] { "bun", "cookedBeef" }, new string[] { "bun", "cookedBeef", "cutCheese" } },
            2f);
        magic0 = new Place(
            new Box[] { new Box(this, 0, "dough"), new Box(this, 1, "cheese"), new Box(this, 2, "tomato") },
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Cutting(this, 2), new Oven(this, 3), new Oven(this, 4), new Table(this, 5), new Table(this, 6) },
            new string[][] { new string[] { "cookedDough", "cookedCheese", "cookedTomato" } },
            2.5f);
        magic1 = new Place(
            new Box[] { new Box(this, 0, "dough"), new Box(this, 1, "cheese"), new Box(this, 2, "tomato"), new Box(this, 3, "pepperoni") },
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Cutting(this, 2), new Oven(this, 3), new Oven(this, 4), new Table(this, 5), new Table(this, 6) },
            new string[][] { new string[] { "cookedDough", "cookedCheese", "cookedTomato" }, new string[] { "cookedDough", "cookedCheese", "cookedTomato", "cookedPepperoni" } },
            3f);
        magic2 = new Place(
            new Box[] { new Box(this, 0, "flour"), new Box(this, 1, "egg"), new Box(this, 2, "chocolate") },
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Mixer(this, 2), new Mixer(this, 3), new PanP(this, 4), new PanP(this, 5), new Table(this, 6), new Table(this, 7) },
            new string[][] { new string[] { "cookedFlour", "cookedEgg" }, new string[] { "cookedFlour", "cookedEgg", "cookedChocolate" } },
            3f);
        magic3 = new Place(
            new Box[] { new Box(this, 0, "flour"), new Box(this, 1, "egg"), new Box(this, 2, "chocolate"), new Box(this, 3, "strawberry"), new Box(this, 4, "blueberry") },
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Mixer(this, 2), new Mixer(this, 3), new PanP(this, 4), new PanP(this, 5), new Table(this, 6), new Table(this, 7) },
            new string[][] { new string[] { "cookedFlour", "cookedEgg" }, new string[] { "cookedFlour", "cookedEgg", "cookedChocolate" }, new string[] { "cookedFlour", "cookedEgg", "cookedStrawberry" }, new string[] { "cookedFlour", "cookedEgg", "cookedBlueberry" } },
            3.5f);
        martian0 = new Place(
            new Box[] { new Box(this, 0, "flour"), new Box(this, 1, "egg"), new Box(this, 2, "honey"), new Box(this, 3, "chocolate"), new Box(this, 4, "carrot") },
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Mixer(this, 2), new Mixer(this, 3), new OvenC(this, 4), new OvenC(this, 5), new Table(this, 6), new Table(this, 7) },
            new string[][] { new string[] { "cookedFlour", "cookedEgg", "cookedHoney" }, new string[] { "cookedFlour", "cookedEgg", "cookedHoney", "cookedChocolate" }, new string[] { "cookedFlour", "cookedEgg", "cookedHoney", "cookedCarrot" } },
            4f);
        kevel0 = new Place(
            new Box[] { new Box(this, 0, "flour"), new Box(this, 1, "fish"), new Box(this, 2, "carrot"), new Box(this, 3, "beef"), new Box(this, 4, "shrimp") },
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Mixer(this, 2), new Mixer(this, 3), new Steamer(this, 4), new Steamer(this, 5), new Table(this, 6), new Table(this, 7) },
            new string[][] { new string[] { "cookedFish" }, new string[] { "cookedFlour", "cookedBeef" }, new string[] { "cookedFlour", "cookedCarrot" }, new string[] { "cookedFlour", "cookedShrimp" } },
            2.5f);
        Color[] colors = { Color.green, Color.green, Color.cyan, Color.cyan, Color.cyan, Color.blue, Color.yellow, Color.yellow, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.red, Color.gray };
        places = new Place[] { sushi0, sushi1, sky0, sky1, sky2, rapids0, mines0, mines1, magic0, magic1, magic2, magic3, martian0, kevel0 };
        int randPlace = Rnd.Range(0, places.Length);
        place = places[randPlace];
        moduleMaterial.GetComponent<MeshRenderer>().material.SetColor("_Color", colors[randPlace]);
        hands = new string[0];
        orders = new string[Mathf.FloorToInt(calcTime/place.OrderAmount) < 3 ? 3 : Mathf.FloorToInt(calcTime / place.OrderAmount)][];
        for(int i=0; i<(Mathf.FloorToInt(calcTime / place.OrderAmount) < 3 ? 3 : Mathf.FloorToInt(calcTime / place.OrderAmount)); i++) {
            orders[i] = place.Orders[Rnd.Range(0, place.Orders.Length)];
        }
        for(int i=0; i<3; i++) {
            orderDisplays[i - submitted].GetComponent<MeshRenderer>().enabled = true;
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
        log($"The orders are: {arrayArrayToString(orders)}.");
        /*log(string.Join(" ", place.Ingredients));
        log(place.Stations[0].ToString());
        log(string.Join(" ", place.Orders));*/
        for(int i = 0; i < 9; i++) {
            try {
                int dummy = i;
                ingredientBoxes[i].gameObject.SetActive(true);
                ingredientBoxes[i].transform.GetComponent<MeshRenderer>().enabled = true;
                place.Ingredients[i].startup();
                ingredientBoxes[i].OnInteract = delegate () { ingredientPress(ingredientBoxes[dummy], place.Ingredients[dummy]); return false; };
            } catch {
                ingredientBoxes[i].gameObject.SetActive(false);
                ingredientBoxes[i].OnInteract = delegate () { return false; };
            }
        }
        trash.OnInteract += delegate () { if(ModuleSolved) { return false; } trash.AddInteractionPunch(); Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, trash.transform); hands = new string[0]; log("Selected Trash, Emptied Hands."); return false; };
        for(int i = 0; i < 8; i++) {
            try {
                int dummy = i;
                stations[i].gameObject.SetActive(true);
                stations[i].transform.GetComponent<MeshRenderer>().enabled = true;
                stations[i].transform.Find("stationImage").transform.GetComponent<MeshRenderer>().enabled = true;
                place.Stations[i].startup();
                stations[i].OnInteract = delegate () { stationPress(stations[dummy], place.Stations[dummy]); return false; };
            } catch {
                stations[i].gameObject.SetActive(false);
                stations[i].OnInteract = delegate () { return false; };
            }
        }
        output.OnInteract = delegate () { if(ModuleSolved) { return false; } trash.AddInteractionPunch(); Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, output.transform); outputCheck(); return false; };

    }
    
    void outputCheck() {
        bool[] checki = new bool[orders[submitted].Length];
        foreach(string food in hands) {
            string[] validFoods = orders[submitted].Where((o, index) => o == food && checki[index] == false).ToArray();
            if(!validFoods.Any()) {
                Strike($"Incorrect! Submitted {arrayToString(hands)} when supposed to submit {arrayToString(orders[submitted])}");
                hands = new string[0];
                return;
            }
            if(checki[Array.IndexOf(orders[submitted], food)] == true) {
                checki[Array.IndexOf(orders[submitted], food, Array.IndexOf(orders[submitted], food))] = true;
            } else {
                checki[Array.IndexOf(orders[submitted], food)] = true;
            }
        }
        foreach(bool i in checki) {
            if(!i) {
                Strike($"Incorrect! Submitted {arrayToString(hands)} when supposed to submit {arrayToString(orders[submitted])}");
                hands = new string[0];
                return;
            }
        }
        hands = new string[0];
        submitted++;
        log($"Order#{submitted} Complete");
        if(submitted==orders.Length) {
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

    public int findInArrayArray(string[][] uncooked, string[] slot) {
        int temp2 = -1;
        int count = 0;
        foreach(string[] order in uncooked) {
            bool check = false;
            if(order.Length != slot.Length) { check = true; }
            foreach(string food in slot) {
                if(Array.IndexOf(order, food) == -1) { check = true; }
            }
            if(!check) {
                temp2 = count;
            }
            count++;
        }
        return temp2;
    }

    public string arrayToString(string[] f) {
        string temp = "";
        foreach(string i in f) {
            temp += i;
        }
        return temp;
    }
    
    public string arrayArrayToString(string[][] f) {
        string temp = "";
        foreach(string[] i in f) {
            temp += " ";
            temp += arrayToString(i);
        }
        return temp;
    }

    public void Beep(KMSelectable station) {
        Audio.PlaySoundAtTransform(beep, station.transform);
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
        Debug.LogFormat("[Overcooked #{0}] Good Job! Module Solved.", ModuleId);
        GetComponent<KMBombModule>().HandlePass();
        ModuleSolved = true;
    }

    public void Strike (string reason) {
        Debug.LogFormat("[Overcooked #{0}] {1} Strike Issued.", ModuleId, reason);
        GetComponent<KMBombModule>().HandleStrike();
        Start();
    }

#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use !{0} to do something.";
#pragma warning restore 414

    KMSelectable[] ProcessTwitchCommand (string Command) {
        TPStrikeTimer = true;
        return null;
    }

    KMSelectable[] TwitchHandleForcedSolve () {
        return null;
    }
}
