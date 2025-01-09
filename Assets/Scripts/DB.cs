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
    public static Place sushi0 = new Place(
            new Ingredient[] { new Lettuce(), new Tomato(), new Cucumber(), new Fish(), new Prawn(), new Rice(), new Seaweed() },
            new Station[] { new Cutting(0), new Cutting(1), new Pot(2), new Pot( 3), new Table(4), new Table(5) },
            new string[][] { new string[] { "cutFish" }, new string[] { "cutPrawn" }, new string[] { "seaweed", "cookedRice", "cutFish" }, new string[] { "seaweed", "cookedRice", "cutCucumber" }, new string[] { "seaweed", "cookedRice", "cutFish", "cutCucumber" }, new string[] { "cutFish" }, new string[] { "cutPrawn" }, new string[] { "seaweed", "cookedRice", "cutFish" }, new string[] { "seaweed", "cookedRice", "cutCucumber" }, new string[] { "seaweed", "cookedRice", "cutFish", "cutCucumber" }, new string[] { "cutLettuce" }, new string[] { "cutLettuce", "cutTomato" }, new string[] { "cutLettuce", "cutTomato", "cutCucumber" } },
            2.5f,
            Color.green);
    public static Place sushi1 = new Place(
            new Ingredient[] { new Bun(), new Meat(), new Lettuce(), new Tomato(), new Cucumber(), new Fish(), new Prawn(), new Rice(), new Seaweed() }, 
            new Station[] { new Cutting(0), new Cutting(1), new Pot(2), new Pot(3), new Table(4), new Table(5), new Pan(6), new Pan(7) }, 
            new string[][] { new string[] { "cutFish" }, new string[] { "cutPrawn" }, new string[] { "seaweed", "cookedRice", "cutFish" }, new string[] { "seaweed", "cookedRice", "cutCucumber" }, new string[] { "seaweed", "cookedRice", "cutFish", "cutCucumber" }, new string[] { "bun", "cookedMeat" }, }, 
            2.5f,
            Color.green);
    public static Place sky0 = new Place(
            new Ingredient[] { new Lettuce(), new Tomato(), new Cucumber(), new Mushroom(), new Meat(), new Fish(), new Prawn(), new Pasta() }, 
            new Station[] { new Cutting(0), new Cutting(1), new Pan(2), new Pan(3), new Pot(4), new Pot(5), new Table(6), new Table(7) }, 
            new string[][] { new string[] { "cookedPasta", "cookedMeat" }, new string[] { "cookedPasta", "cookedMushroom" }, new string[] { "cookedPasta", "cookedTomato" }, new string[] { "cookedPasta", "cookedFish", "cookedPrawn" }, new string[] { "cookedPasta", "cookedMeat" }, new string[] { "cookedPasta", "cookedMushroom" }, new string[] { "cookedPasta", "cookedTomato" }, new string[] { "cookedPasta", "cookedFish", "cookedPrawn" }, new string[] { "cutLettuce" }, new string[] { "cutLettuce", "cutTomato" }, new string[] { "cutLettuce", "cutTomato", "cutCucumber" } }, 
            2.5f,
            Color.cyan);
    public static Place sky1 = new Place(
            new Ingredient[] { new Bun(), new Cheese(), new Lettuce(), new Mushroom(), new Meat(), new Fish(), new Prawn(), new Pasta(), new Tomato() }, 
            new Station[] { new Cutting(0), new Cutting(1), new Pan(2), new Pan(3), new Pot(4), new Pot(5), new Table(6), new Table(7) }, 
            new string[][] { new string[] { "cookedPasta", "cookedMeat" }, new string[] { "cookedPasta", "cookedMushroom" }, new string[] { "cookedPasta", "cookedTomato" }, new string[] { "cookedPasta", "cookedFish", "cookedPrawn" }, new string[] { "bun", "cookedMeat" }, new string[] { "bun", "cookedMeat", "cutCheese" }, new string[] { "bun", "cookedMeat", "cutLettuce", "cutCheese" }, new string[] { "bun", "cookedMeat", "cutLettuce", "cutTomato" } }, 
            3.5f,
            Color.cyan);
    public static Place sky2 = new Place(
            new Ingredient[] { new Bun(), new Cheese(), new Lettuce(), new Meat(), new Tomato() },
            new Station[] { new Cutting(0), new Cutting(1), new Pan(2), new Pan(3), new Table(4), new Table(5) }, 
            new string[][] { new string[] { "bun", "cookedMeat" }, new string[] { "bun", "cookedMeat", "cutCheese" }, new string[] { "bun", "cookedMeat", "cutLettuce", "cutCheese" }, new string[] { "bun", "cookedMeat", "cutLettuce", "cutTomato" } }, 
            2.5f,
            Color.cyan);
    public static Place rapids0 = new Place(
            new Ingredient[] { new Potato(), new Chicken() }, 
            new Station[] { new Cutting(0), new Cutting(1), new Fryer(2), new Fryer(3), new Table(4), new Table(5) }, 
            new string[][] { new string[] { "friedPotato" }, new string[] { "friedChicken" }, new string[] { "friedPotato", "friedChicken" } }, 
            2f,
            Color.blue);
    public static Place mines0 = new Place(
            new Ingredient[] { new Tortilla(), new Chicken(), new Meat(), new Mushroom(), new Rice() },
            new Station[] { new Cutting(0), new Cutting(1), new Pan(2), new Pan(3), new Pot(4), new Pot(5), new Table(6), new Table(7) },
            new string[][] { new string[] { "cookedRice", "tortilla", "cookedMushroom" }, new string[] { "cookedRice", "tortilla", "cookedMeat" }, new string[] { "cookedRice", "tortilla", "cookedChicken" } },
            2.5f,
            Color.yellow);
    public static Place mines1 = new Place(
            new Ingredient[] { new Bun(), new Cheese(), new Meat() },
            new Station[] { new Cutting(0), new Cutting(1), new Pan(2), new Pan(3), new Table(4), new Table(5) },
            new string[][] { new string[] { "bun", "cookedMeat" }, new string[] { "bun", "cookedMeat", "cutCheese" } },
            2f,
            Color.yellow);
    public static Place magic0 = new Place(
            new Ingredient[] { new Dough(), new Cheese(), new Tomato() },
            new Station[] { new Cutting(0), new Cutting(1), new Cutting(2), new Oven(3), new Oven(4), new Table(5), new Table(6) },
            new string[][] { new string[] { "bakedDough", "bakedCheese", "bakedTomato" } },
            2.5f,
            Color.magenta);
    public static Place magic1 = new Place(
            new Ingredient[] { new Dough(), new Cheese(), new Tomato(), new Pepperoni() },
            new Station[] { new Cutting(0), new Cutting(1), new Cutting(2), new Oven(3), new Oven(4), new Table(5), new Table(6) },
            new string[][] { new string[] { "bakedDough", "bakedCheese", "bakedTomato" }, new string[] { "bakedDough", "bakedCheese", "bakedTomato", "bakedPepperoni" } },
            3f,
            Color.magenta);
    public static Place magic2 = new Place(
            new Ingredient[] { new Flour(), new Egg(), new Chocolate() },
            new Station[] { new Cutting(0), new Cutting(1), new Mixer(2), new Mixer(3), new PanP(4), new PanP(5), new Table(6), new Table(7) },
            new string[][] { new string[] { "cookedFlour", "cookedEgg" }, new string[] { "cookedFlour", "cookedEgg", "cookedChocolate" } },
            3f,
            Color.magenta);
    public static Place magic3 = new Place(
            new Ingredient[] { new Flour(), new Egg(), new Chocolate(), new Strawberry(), new Blueberry() },
            new Station[] { new Cutting(0), new Cutting(1), new Mixer(2), new Mixer(3), new PanP(4), new PanP(5), new Table(6), new Table(7) },
            new string[][] { new string[] { "cookedFlour", "cookedEgg" }, new string[] { "cookedFlour", "cookedEgg", "cookedChocolate" }, new string[] { "cookedFlour", "cookedEgg", "cookedStrawberry" }, new string[] { "cookedFlour", "cookedEgg", "cookedBlueberry" } },
            3.5f,
            Color.magenta);
    public static Place martian0 = new Place(
            new Ingredient[] { new Flour(), new Egg(), new Honey(), new Chocolate(), new Carrot() },
            new Station[] { new Cutting(0), new Cutting(1), new Mixer(2), new Mixer(3), new OvenC(4), new OvenC(5), new Table(6), new Table(7) },
            new string[][] { new string[] { "bakedFlour", "bakedEgg", "bakedHoney" }, new string[] { "bakedFlour", "bakedEgg", "bakedHoney", "bakedChocolate" }, new string[] { "bakedFlour", "bakedEgg", "bakedHoney", "bakedCarrot" } },
            4f,
            Color.red);
    public static Place kevel0 = new Place(
            new Ingredient[] { new Flour(), new Fish(), new Carrot(), new Meat(), new Prawn() },
            new Station[] { new Cutting(0), new Cutting(1), new Mixer(2), new Mixer(3), new Steamer(4), new Steamer(5), new Table(6), new Table(7) },
            new string[][] { new string[] { "steamedFish" }, new string[] { "steamedFlour", "steamedMeat" }, new string[] { "steamedFlour", "steamedCarrot" }, new string[] { "steamedFlour", "steamedPrawn" } },
            2.5f,
            Color.gray);
    public static Place[] places = new Place[] { sushi0, sushi1, sky0, sky1, sky2, rapids0, mines0, mines1, magic0, magic1, magic2, magic3, martian0, kevel0 };
}
