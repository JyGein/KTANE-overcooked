using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public static class DB {
    public static Texture[] ingredientImages;
    /*public static Place sushi0 = new Place(
            new Box[] { new Box(this, 0, "lettuce"), new Box(this, 1, "tomato"), new Box(this, 2, "cucumber"), new Box(this, 3, "fish"), new Box(this, 4, "shrimp"), new Box(this, 5, "rice"), new Box(this, 6, "seaweed") },
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Pot(this, 2), new Pot(this, 3), new Table(this, 4), new Table(this, 5) },
            new string[][] { new string[] { "cutFish" }, new string[] { "cutShrimp" }, new string[] { "seaweed", "cookedRice", "cutFish" }, new string[] { "seaweed", "cookedRice", "cutCucumber" }, new string[] { "seaweed", "cookedRice", "cutFish", "cutCucumber" }, new string[] { "cutFish" }, new string[] { "cutShrimp" }, new string[] { "seaweed", "cookedRice", "cutFish" }, new string[] { "seaweed", "cookedRice", "cutCucumber" }, new string[] { "seaweed", "cookedRice", "cutFish", "cutCucumber" }, new string[] { "cutLettuce" }, new string[] { "cutLettuce", "cutTomato" }, new string[] { "cutLettuce", "cutTomato", "cutCucumber" } },
            2.5f,
            Color.green);
    public static Place sushi1 = new Place(
            new Box[] { new Box(this, 0, "bun"), new Box(this, 1, "beef"), new Box(this, 2, "cucumber"), new Box(this, 3, "fish"), new Box(this, 4, "shrimp"), new Box(this, 5, "rice"), new Box(this, 6, "seaweed") }, 
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Pot(this, 2), new Pot(this, 3), new Table(this, 4), new Table(this, 5), new Pan(this, 6), new Pan(this, 7) }, 
            new string[][] { new string[] { "cutFish" }, new string[] { "cutShrimp" }, new string[] { "seaweed", "cookedRice", "cutFish" }, new string[] { "seaweed", "cookedRice", "cutCucumber" }, new string[] { "seaweed", "cookedRice", "cutFish", "cutCucumber" }, new string[] { "bun", "cookedBeef" }, }, 
            2.5f,
            Color.green);
    public static Place sky0 = new Place(
            new Box[] { new Box(this, 0, "lettuce"), new Box(this, 1, "tomato"), new Box(this, 2, "cucumber"), new Box(this, 3, "mushroom"), new Box(this, 4, "beef"), new Box(this, 5, "fish"), new Box(this, 6, "shrimp"), new Box(this, 7, "pasta") }, 
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Pan(this, 2), new Pan(this, 3), new Pot(this, 4), new Pot(this, 5), new Table(this, 6), new Table(this, 7) }, 
            new string[][] { new string[] { "cookedPasta", "cookedBeef" }, new string[] { "cookedPasta", "cookedMushroom" }, new string[] { "cookedPasta", "cookedTomato" }, new string[] { "cookedPasta", "cookedFish", "cookedShrimp" }, new string[] { "cookedPasta", "cookedBeef" }, new string[] { "cookedPasta", "cookedMushroom" }, new string[] { "cookedPasta", "cookedTomato" }, new string[] { "cookedPasta", "cookedFish", "cookedShrimp" }, new string[] { "cutLettuce" }, new string[] { "cutLettuce", "cutTomato" }, new string[] { "cutLettuce", "cutTomato", "cutCucumber" } }, 
            2.5f,
            Color.cyan);
    public static Place sky1 = new Place(
            new Box[] { new Box(this, 0, "bun"), new Box(this, 1, "cheese"), new Box(this, 2, "lettuce"), new Box(this, 3, "mushroom"), new Box(this, 4, "beef"), new Box(this, 5, "fish"), new Box(this, 6, "shrimp"), new Box(this, 7, "pasta"), new Box(this, 8, "tomato") }, 
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Pan(this, 2), new Pan(this, 3), new Pot(this, 4), new Pot(this, 5), new Table(this, 6), new Table(this, 7) }, 
            new string[][] { new string[] { "cookedPasta", "cookedBeef" }, new string[] { "cookedPasta", "cookedMushroom" }, new string[] { "cookedPasta", "cookedTomato" }, new string[] { "cookedPasta", "cookedFish", "cookedShrimp" }, new string[] { "bun", "cookedBeef" }, new string[] { "bun", "cookedBeef", "cutCheese" }, new string[] { "bun", "cookedBeef", "cutLettuce", "cutCheese" }, new string[] { "bun", "cookedBeef", "cutLettuce", "cutTomato" } }, 
            3.5f,
            Color.cyan);
    public static Place sky2 = new Place(
            new Box[] { new Box(this, 0, "bun"), new Box(this, 1, "cheese"), new Box(this, 2, "lettuce"), new Box(this, 3, "beef"), new Box(this, 4, "tomato")},
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Pan(this, 2), new Pan(this, 3), new Table(this, 4), new Table(this, 5) }, 
            new string[][] { new string[] { "bun", "cookedBeef" }, new string[] { "bun", "cookedBeef", "cutCheese" }, new string[] { "bun", "cookedBeef", "cutLettuce", "cutCheese" }, new string[] { "bun", "cookedBeef", "cutLettuce", "cutTomato" } }, 
            2.5f,
            Color.cyan);
    public static Place rapids0 = new Place(
            new Box[] { new Box(this, 0, "potato"), new Box(this, 1, "chicken") }, 
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Fryer(this, 2), new Fryer(this, 3), new Table(this, 4), new Table(this, 5) }, 
            new string[][] { new string[] { "friedPotato" }, new string[] { "friedChicken" }, new string[] { "friedPotato", "friedChicken" } }, 
            2f,
            Color.blue);
    public static Place mines0 = new Place(
            new Box[] { new Box(this, 0, "tortilla"), new Box(this, 1, "chicken"), new Box(this, 2, "beef"), new Box(this, 3, "mushroom"), new Box(this, 4, "rice") },
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Pan(this, 2), new Pan(this, 3), new Pot(this, 4), new Pot(this, 5), new Table(this, 6), new Table(this, 7) },
            new string[][] { new string[] { "cookedRice", "tortilla", "cookedMushroom" }, new string[] { "cookedRice", "tortilla", "cookedBeef" }, new string[] { "cookedRice", "tortilla", "cookedChicken" } },
            2.5f,
            Color.yellow);
    public static Place mines1 = new Place(
            new Box[] { new Box(this, 0, "bun"), new Box(this, 1, "cheese"), new Box(this, 2, "beef") },
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Pan(this, 2), new Pan(this, 3), new Table(this, 4), new Table(this, 5) },
            new string[][] { new string[] { "bun", "cookedBeef" }, new string[] { "bun", "cookedBeef", "cutCheese" } },
            2f,
            Color.yellow);
    public static Place magic0 = new Place(
            new Box[] { new Box(this, 0, "dough"), new Box(this, 1, "cheese"), new Box(this, 2, "tomato") },
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Cutting(this, 2), new Oven(this, 3), new Oven(this, 4), new Table(this, 5), new Table(this, 6) },
            new string[][] { new string[] { "bakedDough", "bakedCheese", "bakedTomato" } },
            2.5f,
            Color.magenta);
    public static Place magic1 = new Place(
            new Box[] { new Box(this, 0, "dough"), new Box(this, 1, "cheese"), new Box(this, 2, "tomato"), new Box(this, 3, "pepperoni") },
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Cutting(this, 2), new Oven(this, 3), new Oven(this, 4), new Table(this, 5), new Table(this, 6) },
            new string[][] { new string[] { "bakedDough", "bakedCheese", "bakedTomato" }, new string[] { "bakedDough", "bakedCheese", "bakedTomato", "bakedPepperoni" } },
            3f,
            Color.magenta);
    public static Place magic2 = new Place(
            new Box[] { new Box(this, 0, "flour"), new Box(this, 1, "egg"), new Box(this, 2, "chocolate") },
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Mixer(this, 2), new Mixer(this, 3), new PanP(this, 4), new PanP(this, 5), new Table(this, 6), new Table(this, 7) },
            new string[][] { new string[] { "cookedFlour", "cookedEgg" }, new string[] { "cookedFlour", "cookedEgg", "cookedChocolate" } },
            3f,
            Color.magenta);
    public static Place magic3 = new Place(
            new Box[] { new Box(this, 0, "flour"), new Box(this, 1, "egg"), new Box(this, 2, "chocolate"), new Box(this, 3, "strawberry"), new Box(this, 4, "blueberry") },
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Mixer(this, 2), new Mixer(this, 3), new PanP(this, 4), new PanP(this, 5), new Table(this, 6), new Table(this, 7) },
            new string[][] { new string[] { "cookedFlour", "cookedEgg" }, new string[] { "cookedFlour", "cookedEgg", "cookedChocolate" }, new string[] { "cookedFlour", "cookedEgg", "cookedStrawberry" }, new string[] { "cookedFlour", "cookedEgg", "cookedBlueberry" } },
            3.5f,
            Color.magenta);
    public static Place martian0 = new Place(
            new Box[] { new Box(this, 0, "flour"), new Box(this, 1, "egg"), new Box(this, 2, "honey"), new Box(this, 3, "chocolate"), new Box(this, 4, "carrot") },
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Mixer(this, 2), new Mixer(this, 3), new OvenC(this, 4), new OvenC(this, 5), new Table(this, 6), new Table(this, 7) },
            new string[][] { new string[] { "bakedFlour", "bakedEgg", "bakedHoney" }, new string[] { "bakedFlour", "bakedEgg", "bakedHoney", "bakedChocolate" }, new string[] { "bakedFlour", "bakedEgg", "bakedHoney", "bakedCarrot" } },
            4f,
            Color.red);
    public static Place kevel0 = new Place(
            new Box[] { new Box(this, 0, "flour"), new Box(this, 1, "fish"), new Box(this, 2, "carrot"), new Box(this, 3, "beef"), new Box(this, 4, "shrimp") },
            new Station[] { new Cutting(this, 0), new Cutting(this, 1), new Mixer(this, 2), new Mixer(this, 3), new Steamer(this, 4), new Steamer(this, 5), new Table(this, 6), new Table(this, 7) },
            new string[][] { new string[] { "steamedFish" }, new string[] { "steamedFlour", "steamedBeef" }, new string[] { "steamedFlour", "steamedCarrot" }, new string[] { "steamedFlour", "steamedShrimp" } },
            2.5f,
            Color.gray);
    public static Place[] places = new Place[] { sushi0, sushi1, sky0, sky1, sky2, rapids0, mines0, mines1, magic0, magic1, magic2, magic3, martian0, kevel0 };
    */
}
