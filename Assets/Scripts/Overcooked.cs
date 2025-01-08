using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;
using System.Runtime.InteropServices;

public class Overcooked : MonoBehaviour {

    public KMBombInfo Bomb;
    public KMAudio Audio;
    static int ModuleIdCounter = 1;
    int ModuleId;

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
    public GameObject textTemplate;

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
    private readonly string[][] allOrders = new string[][] { new string[] { "cutFish" }, new string[] { "cutShrimp" }, new string[] { "seaweed", "cookedRice", "cutFish" }, new string[] { "seaweed", "cookedRice", "cutCucumber" }, new string[] { "seaweed", "cookedRice", "cutFish", "cutCucumber" }, new string[] { "cookedPasta", "cookedBeef" }, new string[] { "cookedPasta", "cookedMushroom" }, new string[] { "cookedPasta", "cookedTomato" }, new string[] { "cookedPasta", "cookedFish", "cookedShrimp" }, new string[] { "cutLettuce" }, new string[] { "cutLettuce", "cutTomato" }, new string[] { "cutLettuce", "cutTomato", "cutCucumber" }, new string[] { "friedPotato" }, new string[] { "friedChicken" }, new string[] { "friedPotato", "friedChicken" }, new string[] { "cookedRice", "tortilla", "cookedMushroom" }, new string[] { "cookedRice", "tortilla", "cookedBeef" }, new string[] { "cookedRice", "tortilla", "cookedChicken" }, new string[] { "bun", "cookedBeef" }, new string[] { "bun", "cookedBeef", "cutCheese" }, new string[] { "bun", "cookedBeef", "cutLettuce", "cutCheese" }, new string[] { "bun", "cookedBeef", "cutLettuce", "cutTomato" }, new string[] { "bakedDough", "bakedCheese", "bakedTomato" }, new string[] { "bakedDough", "bakedCheese", "bakedTomato", "bakedPepperoni" }, new string[] { "bakedDough", "bakedCheese", "bakedTomato", "bakedChicken" }, new string[] { "bakedDough", "bakedCheese", "bakedTomato", "bakedOlive" }, new string[] { "cookedFlour", "cookedEgg" }, new string[] { "cookedFlour", "cookedEgg", "cookedChocolate" }, new string[] { "cookedFlour", "cookedEgg", "cookedStrawberry" }, new string[] { "cookedFlour", "cookedEgg", "cookedBlueberry" }, new string[] { "bakedFlour", "bakedEgg", "bakedHoney" }, new string[] { "bakedFlour", "bakedEgg", "bakedHoney", "bakedChocolate" }, new string[] { "bakedFlour", "bakedEgg", "bakedHoney", "bakedCarrot" }, new string[] { "steamedFish" }, new string[] { "steamedFlour", "steamedBeef" }, new string[] { "steamedFlour", "steamedCarrot" }, new string[] { "steamedFlour", "steamedShrimp" } };
    public readonly string[] allIngredients = new string[] { "fish", "shrimp", "cucumber", "rice", "seaweed", "cutFish", "cutShrimp", "cutCucumber", "cookedRice", "pasta", "mushroom", "beef", "tomato", "cutTomato", "cutBeef", "cutMushroom", "cookedFish", "cookedShrimp", "cookedMushroom", "cookedBeef", "cookedTomato", "cookedPasta", "lettuce", "cutLettuce", "tortilla", "potato", "chicken", "cutPotato", "cutChicken", "friedPotato", "friedChicken", "bakedChicken", "bun", "cheese", "cutCheese", "dough", "cutDough", "bakedDough", "pepperoni", "cutPepperoni", "bakedCheese", "bakedPepperoni", "flour", "egg", "mixedFlour", "mixedEgg", "chocolate", "blueberry", "strawberry", "cutChocolate", "cutBlueberry", "cutStrawberry", "mixedChocolate", "mixedBlueberry", "mixedStrawberry", "cookedFlour", "cookedEgg", "cookedChocolate", "cookedBlueberry", "cookedStrawberry", "honey", "carrot", "cutHoney", "cutCarrot", "mixedHoney", "mixedCarrot", "bakedHoney", "bakedCarrot", "mixedBeef", "mixedShrimp", "steamedFish", "steamedShrimp", "steamedBeef", "bakedTomato", "bakedFlour", "steamedFlour", "bakedEgg", "bakedChocolate", "cookedChicken ", "steamedCarrot" };
    private readonly string beep = "beep-22 (mp3cut.net)";
    public float minutesTime;
    public readonly string[] uncombinable = new string[] { "bakedDough", "fish", "shrimp", "cucumber", "rice", "pasta", "mushroom", "beef", "tomato", "cutBeef", "cutMushroom", "lettuce", "potato", "chicken", "cutPotato", "cutChicken", "cheese", "dough", "pepperoni", "flour", "egg", "mixedFlour", "mixedEgg", "chocolate", "blueberry", "strawberry", "mixedChocolate", "mixedBlueberry", "mixedStrawberry", "cookedFlour", "cookedEgg", "cookedChocolate", "cookedBlueberry", "cookedStrawberry", "honey", "carrot", "mixedHoney", "mixedCarrot", "cookedHoney", "bakedCarrot", "mixedBeef", "mixedShrimp", "steamedFish", "steamedShrimp", "steamedBeef", "steamedCarrot", "bakedTomato", "bakedFlour", "bakedChocolate", "bakedEgg" };
    public bool TPStrikeTimer;
    public GameObject[] colorblindTexts = new GameObject[8];
    public bool colorblindtime;

    void Awake () {
        ModuleId = ModuleIdCounter++; 
        colorblindtime = true;
        for(int i=0; i<8; i++) {
            colorblindTexts[i] = Instantiate(textTemplate);
            colorblindTexts[i].transform.parent = stations[i].transform;
            colorblindTexts[i].transform.localPosition = new Vector3(0, 0.2f, 0.67f);
        }
        /*
        foreach (KMSelectable object in keypad) {
            object.OnInteract += delegate () { keypadPress(object); return false; };
        }shapuwu
        */

         //button.OnInteract += delegate () { buttonPress(); return false; };

    }

    void Start () { //Shit
        DB.ingredientImages = ingredientImages;
        submitted = 0;
        minutesTime = Bomb.GetTime() / 60;
        float calcTime = minutesTime / 2 * 7 >= 45 ? 45 : minutesTime / 2 * 7;
        sushi0 = new Place(
            new Box[] { new Box(this, 0, "lettuce"), new Box(this, 1, "tomato"), new Box(this, 2, "cucumber"), new Box(this, 3, "fish"), new Box(this, 4, "shrimp"), new Box(this, 5, "rice"), new Box(this, 6, "seaweed") },
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Pot(this, 2), new Pot(this, 3), new Table(this, 4), new Table(this, 5) }, 
            new string[][] { new string[] { "cutFish" }, new string[] { "cutShrimp" }, new string[] { "seaweed", "cookedRice", "cutFish" }, new string[] { "seaweed", "cookedRice", "cutCucumber" }, new string[] { "seaweed", "cookedRice", "cutFish", "cutCucumber" }, new string[] { "cutFish" }, new string[] { "cutShrimp" }, new string[] { "seaweed", "cookedRice", "cutFish" }, new string[] { "seaweed", "cookedRice", "cutCucumber" }, new string[] { "seaweed", "cookedRice", "cutFish", "cutCucumber" }, new string[] { "cutLettuce" }, new string[] { "cutLettuce", "cutTomato" }, new string[] { "cutLettuce", "cutTomato", "cutCucumber" } }, 
            2.5f,
            Color.green);
        sushi1 = new Place(
            new Box[] { new Box(this, 0, "bun"), new Box(this, 1, "beef"), new Box(this, 2, "cucumber"), new Box(this, 3, "fish"), new Box(this, 4, "shrimp"), new Box(this, 5, "rice"), new Box(this, 6, "seaweed") }, 
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Pot(this, 2), new Pot(this, 3), new Table(this, 4), new Table(this, 5), new Pan(this, 6), new Pan(this, 7) }, 
            new string[][] { new string[] { "cutFish" }, new string[] { "cutShrimp" }, new string[] { "seaweed", "cookedRice", "cutFish" }, new string[] { "seaweed", "cookedRice", "cutCucumber" }, new string[] { "seaweed", "cookedRice", "cutFish", "cutCucumber" }, new string[] { "bun", "cookedBeef" }, }, 
            2.5f,
            Color.green);
        sky0 = new Place(
            new Box[] { new Box(this, 0, "lettuce"), new Box(this, 1, "tomato"), new Box(this, 2, "cucumber"), new Box(this, 3, "mushroom"), new Box(this, 4, "beef"), new Box(this, 5, "fish"), new Box(this, 6, "shrimp"), new Box(this, 7, "pasta") }, 
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Pan(this, 2), new Pan(this, 3), new Pot(this, 4), new Pot(this, 5), new Table(this, 6), new Table(this, 7) }, 
            new string[][] { new string[] { "cookedPasta", "cookedBeef" }, new string[] { "cookedPasta", "cookedMushroom" }, new string[] { "cookedPasta", "cookedTomato" }, new string[] { "cookedPasta", "cookedFish", "cookedShrimp" }, new string[] { "cookedPasta", "cookedBeef" }, new string[] { "cookedPasta", "cookedMushroom" }, new string[] { "cookedPasta", "cookedTomato" }, new string[] { "cookedPasta", "cookedFish", "cookedShrimp" }, new string[] { "cutLettuce" }, new string[] { "cutLettuce", "cutTomato" }, new string[] { "cutLettuce", "cutTomato", "cutCucumber" } }, 
            2.5f,
            Color.cyan);
        sky1 = new Place(
            new Box[] { new Box(this, 0, "bun"), new Box(this, 1, "cheese"), new Box(this, 2, "lettuce"), new Box(this, 3, "mushroom"), new Box(this, 4, "beef"), new Box(this, 5, "fish"), new Box(this, 6, "shrimp"), new Box(this, 7, "pasta"), new Box(this, 8, "tomato") }, 
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Pan(this, 2), new Pan(this, 3), new Pot(this, 4), new Pot(this, 5), new Table(this, 6), new Table(this, 7) }, 
            new string[][] { new string[] { "cookedPasta", "cookedBeef" }, new string[] { "cookedPasta", "cookedMushroom" }, new string[] { "cookedPasta", "cookedTomato" }, new string[] { "cookedPasta", "cookedFish", "cookedShrimp" }, new string[] { "bun", "cookedBeef" }, new string[] { "bun", "cookedBeef", "cutCheese" }, new string[] { "bun", "cookedBeef", "cutLettuce", "cutCheese" }, new string[] { "bun", "cookedBeef", "cutLettuce", "cutTomato" } }, 
            3.5f,
            Color.cyan);
        sky2 = new Place(
            new Box[] { new Box(this, 0, "bun"), new Box(this, 1, "cheese"), new Box(this, 2, "lettuce"), new Box(this, 3, "beef"), new Box(this, 4, "tomato")},
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Pan(this, 2), new Pan(this, 3), new Table(this, 4), new Table(this, 5) }, 
            new string[][] { new string[] { "bun", "cookedBeef" }, new string[] { "bun", "cookedBeef", "cutCheese" }, new string[] { "bun", "cookedBeef", "cutLettuce", "cutCheese" }, new string[] { "bun", "cookedBeef", "cutLettuce", "cutTomato" } }, 
            2.5f,
            Color.cyan);
        rapids0 = new Place(
            new Box[] { new Box(this, 0, "potato"), new Box(this, 1, "chicken") }, 
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Fryer(this, 2), new Fryer(this, 3), new Table(this, 4), new Table(this, 5) }, 
            new string[][] { new string[] { "friedPotato" }, new string[] { "friedChicken" }, new string[] { "friedPotato", "friedChicken" } }, 
            2f,
            Color.blue);
        mines0 = new Place(
            new Box[] { new Box(this, 0, "tortilla"), new Box(this, 1, "chicken"), new Box(this, 2, "beef"), new Box(this, 3, "mushroom"), new Box(this, 4, "rice") },
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Pan(this, 2), new Pan(this, 3), new Pot(this, 4), new Pot(this, 5), new Table(this, 6), new Table(this, 7) },
            new string[][] { new string[] { "cookedRice", "tortilla", "cookedMushroom" }, new string[] { "cookedRice", "tortilla", "cookedBeef" }, new string[] { "cookedRice", "tortilla", "cookedChicken" } },
            2.5f,
            Color.yellow);
        mines1 = new Place(
            new Box[] { new Box(this, 0, "bun"), new Box(this, 1, "cheese"), new Box(this, 2, "beef") },
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Pan(this, 2), new Pan(this, 3), new Table(this, 4), new Table(this, 5) },
            new string[][] { new string[] { "bun", "cookedBeef" }, new string[] { "bun", "cookedBeef", "cutCheese" } },
            2f,
            Color.yellow);
        magic0 = new Place(
            new Box[] { new Box(this, 0, "dough"), new Box(this, 1, "cheese"), new Box(this, 2, "tomato") },
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Cutting(this, 2), new Oven(this, 3), new Oven(this, 4), new Table(this, 5), new Table(this, 6) },
            new string[][] { new string[] { "bakedDough", "bakedCheese", "bakedTomato" } },
            2.5f,
            Color.magenta);
        magic1 = new Place(
            new Box[] { new Box(this, 0, "dough"), new Box(this, 1, "cheese"), new Box(this, 2, "tomato"), new Box(this, 3, "pepperoni") },
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Cutting(this, 2), new Oven(this, 3), new Oven(this, 4), new Table(this, 5), new Table(this, 6) },
            new string[][] { new string[] { "bakedDough", "bakedCheese", "bakedTomato" }, new string[] { "bakedDough", "bakedCheese", "bakedTomato", "bakedPepperoni" } },
            3f,
            Color.magenta);
        magic2 = new Place(
            new Box[] { new Box(this, 0, "flour"), new Box(this, 1, "egg"), new Box(this, 2, "chocolate") },
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Mixer(this, 2), new Mixer(this, 3), new PanP(this, 4), new PanP(this, 5), new Table(this, 6), new Table(this, 7) },
            new string[][] { new string[] { "cookedFlour", "cookedEgg" }, new string[] { "cookedFlour", "cookedEgg", "cookedChocolate" } },
            3f,
            Color.magenta);
        magic3 = new Place(
            new Box[] { new Box(this, 0, "flour"), new Box(this, 1, "egg"), new Box(this, 2, "chocolate"), new Box(this, 3, "strawberry"), new Box(this, 4, "blueberry") },
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Mixer(this, 2), new Mixer(this, 3), new PanP(this, 4), new PanP(this, 5), new Table(this, 6), new Table(this, 7) },
            new string[][] { new string[] { "cookedFlour", "cookedEgg" }, new string[] { "cookedFlour", "cookedEgg", "cookedChocolate" }, new string[] { "cookedFlour", "cookedEgg", "cookedStrawberry" }, new string[] { "cookedFlour", "cookedEgg", "cookedBlueberry" } },
            3.5f,
            Color.magenta);
        martian0 = new Place(
            new Box[] { new Box(this, 0, "flour"), new Box(this, 1, "egg"), new Box(this, 2, "honey"), new Box(this, 3, "chocolate"), new Box(this, 4, "carrot") },
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Mixer(this, 2), new Mixer(this, 3), new OvenC(this, 4), new OvenC(this, 5), new Table(this, 6), new Table(this, 7) },
            new string[][] { new string[] { "bakedFlour", "bakedEgg", "bakedHoney" }, new string[] { "bakedFlour", "bakedEgg", "bakedHoney", "bakedChocolate" }, new string[] { "bakedFlour", "bakedEgg", "bakedHoney", "bakedCarrot" } },
            4f,
            Color.red);
        kevel0 = new Place(
            new Box[] { new Box(this, 0, "flour"), new Box(this, 1, "fish"), new Box(this, 2, "carrot"), new Box(this, 3, "beef"), new Box(this, 4, "shrimp") },
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Mixer(this, 2), new Mixer(this, 3), new Steamer(this, 4), new Steamer(this, 5), new Table(this, 6), new Table(this, 7) },
            new string[][] { new string[] { "steamedFish" }, new string[] { "steamedFlour", "steamedBeef" }, new string[] { "steamedFlour", "steamedCarrot" }, new string[] { "steamedFlour", "steamedShrimp" } },
            2.5f,
            Color.gray);
        places = new Place[] { sushi0, sushi1, sky0, sky1, sky2, rapids0, mines0, mines1, magic0, magic1, magic2, magic3, martian0, kevel0 };
        int randPlace = Rnd.Range(0, places.Length);
        place = places[randPlace];
        moduleMaterial.GetComponent<MeshRenderer>().material.SetColor("_Color", place.color);
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
        log($"The orders are: {orders.arrayArrayToString()}.");
        /*log(string.Join(" ", place.Ingredients));
        log(place.Stations[0].ToString());
        log(string.Join(" ", place.Orders));*/
        for(int i = 0; i < 9; i++) {
            ingredientBoxes[i].gameObject.SetActive(false);
            ingredientBoxes[i].OnInteract = delegate () { return false; };
            ingredientBoxes[i].transform.GetComponent<MeshRenderer>().enabled = true;
        }
        for(int i = 0; i < place.Ingredients.Length; i++) {
            int dummy = i;
            ingredientBoxes[i].gameObject.SetActive(true);
            place.Ingredients[i].startup();
            ingredientBoxes[i].OnInteract = delegate () { ingredientPress(ingredientBoxes[dummy], place.Ingredients[dummy]); return false; };
        }
        trash.OnInteract = delegate () { if(ModuleSolved) { return false; } trash.AddInteractionPunch(); Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, trash.transform); hands = new string[0]; log("Selected Trash, Emptied Hands."); return false; };
        for(int i = 0; i < 8; i++) {
            stations[i].gameObject.SetActive(false);
            stations[i].OnInteract = delegate () { return false; };
            stations[i].transform.GetComponent<MeshRenderer>().enabled = true;
        }
        for(int i = 0; i < place.Stations.Length; i++) {
            int dummy = i;
            stations[i].gameObject.SetActive(true);
            place.Stations[i].startup();
            stations[i].transform.Find("stationImage").transform.GetComponent<MeshRenderer>().enabled = true;
            stations[i].OnInteract = delegate () { stationPress(stations[dummy], place.Stations[dummy]); return false; };
        }
        output.OnInteract = delegate () { if(ModuleSolved) { return false; } trash.AddInteractionPunch(); Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, output.transform); outputCheck(); return false; };
    }
    
    void outputCheck() {
        bool[] checki = new bool[orders[submitted].Length];
        foreach(string food in hands) {
            string[] validFoods = orders[submitted].Where((o, index) => o == food && checki[index] == false).ToArray();
            if(!validFoods.Any()) {
                Strike($"Incorrect! Submitted {hands.arrayToString()} when supposed to submit {orders[submitted].arrayToString()}");
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
                Strike($"Incorrect! Submitted {hands.arrayToString()} when supposed to submit {orders[submitted].arrayToString()}");
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
        string output = message.Split(' ').Select((word) => allIngredients.Contains(word.TrimEnd('.', ',')) ? word.CapitalizeFirstLetter() : word).Join(" ");
        Debug.Log($"[Overcooked #{ModuleId}] {output}");
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
    private readonly string TwitchHelpMessage = @"Use !{0} i#/s#/o/t to press an ingredient box/station/output/trash. Ingredients and stations are numbered in reading order. Commands can be chained like !1 i3 s1 s8 t s6 o. It takes 15 seconds rather than 5 to burn in TP.";
#pragma warning restore 414

    KMSelectable[] ProcessTwitchCommand (string Command) {
        TPStrikeTimer = true;
        string LowerCommand = Command.ToLower().Trim();
        if(LowerCommand == "colorblind") { colorblindtime = true; foreach(Station i in place.Stations) { i.startup(); } return new KMSelectable[0]; }
        Match m = Regex.Match(LowerCommand, @"^((i[1-9]|s[1-8]|t|o)\s?\b)+$");
        if(!m.Success) { return null; }
        List<KMSelectable> press = new List<KMSelectable>();
        foreach(string i in m.Value.Split(' ')) {
            if(i[0] == 't') {
                press.Add(trash);
            }
            if(i[0] == 'o') {
                press.Add(output);
            }
            if(i[0] == 's') {
                press.Add(stations[i[1]-'1']);
            }
            if(i[0] == 'i') {
                press.Add(ingredientBoxes[i[1]-'1']);
            }
        }
        return press.ToArray();
    }

    KMSelectable[] TwitchHandleForcedSolve () {
        return null;
    }
}
