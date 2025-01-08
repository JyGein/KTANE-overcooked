using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public abstract class Ingredient {
    public abstract Spr GetImage();
    public bool cut;
    public virtual bool IsCuttable() => false;
    public bool mixed;
    public virtual bool IsMixable() => false;
    public bool cooked;
    public virtual bool IsCookable() => false;
    public bool baked;
    public virtual bool IsBakeable() => false;
    public bool steamed;
    public virtual bool IsSteamable() => false;
    public bool fried;
    public virtual bool IsFryable() => false;
    public bool boiled;
    public virtual bool IsBoilable() => false;
    public bool prepared { get { return cooked || baked || steamed || fried || boiled; } }
}

public class Flour : Ingredient {
    public override Spr GetImage() {
        if(steamed) return Spr.Texture2D_steamedFlour;
        if(baked) return Spr.Texture2D_bakedFlour;
        if(cooked) return Spr.Texture2D_cookedFlour;
        if(mixed) return Spr.Texture2D_mixedFlour;
        return Spr.Texture2D_Flour_Icon;
    }
    public override bool IsMixable() => !mixed && !prepared;
    public override bool IsCookable() => mixed && !prepared;
    public override bool IsBakeable() => mixed && !prepared;
    public override bool IsSteamable() => mixed && !prepared;
}

public class Seaweed : Ingredient {
    public override Spr GetImage() {
        return Spr.Texture2D_Seaweed_Icon;
    }
}

public class Fish : Ingredient
{
    public override Spr GetImage()
    {
        if (steamed) return Spr.Texture2D_steamedFish;
        if (cooked) return Spr.Texture2D_cookedFish;
        if (cut) return Spr.Texture2D_cutFish;
        return Spr.Texture2D_Fish_Icon;
    }
    public override bool IsCuttable() => !cut && !prepared;
    public override bool IsCookable() => cut && !prepared;
    public override bool IsSteamable() => cut && !prepared;
}

public class Prawn : Ingredient
{
    public override Spr GetImage()
    {
        if (steamed) return Spr.Texture2D_steamedPrawn;
        if (cooked) return Spr.Texture2D_cookedPrawn;
        if (cut) return Spr.Texture2D_cutPrawn;
        if (mixed) return Spr.Texture2D_mixedPrawn;
        return Spr.Texture2D_prawn_Icon;
    }
    public override bool IsCuttable() => !cut && !mixed && !prepared;
    public override bool IsMixable() => !cut && !mixed && !prepared;
    public override bool IsCookable() => cut && !mixed && !prepared;
    public override bool IsSteamable() => !cut && mixed && !prepared;
}

public class Rice : Ingredient
{
    public override Spr GetImage()
    {
        if (boiled) return Spr.Texture2D_cookedRice;
        return Spr.Texture2D_Rice_Icon;
    }
    public override bool IsBoilable() => !prepared;
}

public class Pasta : Ingredient
{
    public override Spr GetImage()
    {
        if (boiled) return Spr.Texture2D_cookedPasta;
        return Spr.Texture2D_pasta_Icon;
    }
    public override bool IsBoilable() => !prepared;
}

public class Mushroom : Ingredient
{
    public override Spr GetImage()
    {
        if (cooked) return Spr.Texture2D_cookedMushroom;
        if (cut) return Spr.Texture2D_cutMushroom;
        return Spr.Texture2D_Mushroom_Icon;
    }
    public override bool IsCookable() => cut && !prepared;
    public override bool IsCuttable() => !cut && !prepared;
}

public class Meat : Ingredient
{
    public override Spr GetImage()
    {
        if (steamed) return Spr.Texture2D_steamedMeat;
        if (cooked) return Spr.Texture2D_cookedMeat;
        if (cut) return Spr.Texture2D_cutMeat;
        if (mixed) return Spr.Texture2D_mixedMeat;
        return Spr.Texture2D_Meat_Icon;
    }
    public override bool IsCuttable() => !cut && !mixed && !prepared;
    public override bool IsMixable() => !cut && !mixed && !prepared;
    public override bool IsCookable() => cut && !mixed && !prepared;
    public override bool IsSteamable() => !cut && mixed && !prepared;
}

public class Tomato : Ingredient
{
    public override Spr GetImage()
    {
        if (baked) return Spr.Texture2D_bakedTomato;
        if (cooked) return Spr.Texture2D_cookedTomato;
        if (cut) return Spr.Texture2D_cutTomato;
        return Spr.Texture2D_Tomato_Icon;
    }
    public override bool IsCookable() => cut && !prepared;
    public override bool IsBakeable() => cut && !prepared;
    public override bool IsCuttable() => !cut && !prepared;
}

public class Lettuce : Ingredient
{
    public override Spr GetImage()
    {
        if (cut) return Spr.Texture2D_cutLettuce;
        return Spr.Texture2D_Lettuce_Icon;
    }
    public override bool IsCuttable() => !cut;
}

public class Tortilla : Ingredient
{
    public override Spr GetImage()
    {
        return Spr.Texture2D_Tortilla_Icon;
    }
}

public class Potato : Ingredient
{
    public override Spr GetImage()
    {
        if (fried) return Spr.Texture2D_friedPotato;
        if (cut) return Spr.Texture2D_cutPotato;
        return Spr.Texture2D_Potato_Icon;
    }
    public override bool IsFryable() => cut && !prepared;
    public override bool IsCuttable() => !cut && !prepared;
}

public class Chicken : Ingredient
{
    public override Spr GetImage()
    {
        if (baked) return Spr.Texture2D_bakedChicken;
        if (cooked) return Spr.Texture2D_cookedChicken;
        if (fried) return Spr.Texture2D_friedChicken;
        if (cut) return Spr.Texture2D_cutChicken;
        return Spr.Texture2D_Chicken_Icon;
    }
    public override bool IsFryable() => cut && !prepared;
    public override bool IsBakeable() => cut && !prepared;
    public override bool IsCookable() => cut && !prepared;
    public override bool IsCuttable() => !cut && !prepared;
}

public class Bun : Ingredient
{
    public override Spr GetImage()
    {
        return Spr.Texture2D_Bun_Icon;
    }
}

public class Cheese : Ingredient
{
    public override Spr GetImage()
    {
        if (baked) return Spr.Texture2D_bakedCheese;
        if (cut) return Spr.Texture2D_cutCheese;
        return Spr.Texture2D_Cheese_Icon;
    }
    public override bool IsBakeable() => cut && !prepared;
    public override bool IsCuttable() => !cut && !prepared;
}

public class Dough : Ingredient
{
    public override Spr GetImage()
    {
        if (baked) return Spr.Texture2D_bakedDough;
        if (cut) return Spr.Texture2D_cutDough;
        return Spr.Texture2D_Dough_Icon;
    }
    public override bool IsBakeable() => cut && !prepared;
    public override bool IsCuttable() => !cut && !prepared;
}

public class Pepperoni : Ingredient
{
    public override Spr GetImage()
    {
        if (baked) return Spr.Texture2D_bakedPepperoni;
        if (cut) return Spr.Texture2D_cutPepperoni;
        return Spr.Texture2D_Pepperoni_Icon;
    }
    public override bool IsBakeable() => cut && !prepared;
    public override bool IsCuttable() => !cut && !prepared;
}

public class Egg : Ingredient
{
    public override Spr GetImage()
    {
        if (baked) return Spr.Texture2D_bakedEgg;
        if (cooked) return Spr.Texture2D_cookedEgg;
        if (mixed) return Spr.Texture2D_mixedEgg;
        return Spr.Texture2D_egg_Icon;
    }
    public override bool IsBakeable() => mixed && !prepared;
    public override bool IsCookable() => mixed && !prepared;
    public override bool IsMixable() => !mixed && !prepared;
}

public class Chocolate : Ingredient
{
    public override Spr GetImage()
    {
        if (baked) return Spr.Texture2D_bakedChocolate;
        if (cooked) return Spr.Texture2D_cookedChocolate;
        if (mixed) return Spr.Texture2D_mixedChocolate;
        if (cut) return Spr.Texture2D_cutChocolate;
        return Spr.Texture2D_chocolate_Icon;
    }
    public override bool IsBakeable() => mixed && !prepared;
    public override bool IsCookable() => mixed && !prepared;
    public override bool IsMixable() => cut && !mixed && !prepared;
    public override bool IsCuttable() => !cut && !mixed && !prepared;
}

public class Blueberry : Ingredient
{
    public override Spr GetImage()
    {
        if (cooked) return Spr.Texture2D_cookedBlueberry;
        if (mixed) return Spr.Texture2D_mixedBlueberry;
        if (cut) return Spr.Texture2D_cutBlueberry;
        return Spr.Texture2D_Blueberry_Icon;
    }
    public override bool IsCookable() => mixed && !prepared;
    public override bool IsMixable() => cut && !mixed && !prepared;
    public override bool IsCuttable() => !cut && !mixed && !prepared;
}

public class Strawberry : Ingredient
{
    public override Spr GetImage()
    {
        if (cooked) return Spr.Texture2D_cookedStrawberry;
        if (mixed) return Spr.Texture2D_mixedStrawberry;
        if (cut) return Spr.Texture2D_cutStrawberry;
        return Spr.Texture2D_Strawberry_Icon;
    }
    public override bool IsCookable() => mixed && !prepared;
    public override bool IsMixable() => cut && !mixed && !prepared;
    public override bool IsCuttable() => !cut && !mixed && !prepared;
}

public class Honey : Ingredient
{
    public override Spr GetImage()
    {
        if (baked) return Spr.Texture2D_bakedHoney;
        if (mixed) return Spr.Texture2D_mixedHoney;
        if (cut) return Spr.Texture2D_cutHoney;
        return Spr.Texture2D_honeycomb_Icon;
    }
    public override bool IsBakeable() => mixed && !prepared;
    public override bool IsMixable() => cut && !mixed && !prepared;
    public override bool IsCuttable() => !cut && !mixed && !prepared;
}

public class Carrot : Ingredient
{
    public override Spr GetImage()
    {
        if (baked) return Spr.Texture2D_bakedHoney;
        if (steamed) return Spr.Texture2D_steamedCarrot;
        if (mixed) return Spr.Texture2D_mixedHoney;
        if (cut) return Spr.Texture2D_cutHoney;
        return Spr.Texture2D_Carrot_Icon;
    }
    public override bool IsBakeable() => mixed && !prepared;
    public override bool IsMixable() => cut && !mixed && !prepared;
    public override bool IsCuttable() => !cut && !mixed && !prepared;
}