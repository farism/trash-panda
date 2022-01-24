using System.Collections.Generic;
using System;

[Serializable]
public class Inventory
{
  public int currency = 100;

  public int foodCupcake = 0;
  public int foodFries = 0;
  public int foodHamburger = 0;
  public int foodHotdog = 0;
  public int foodJuice = 0;
  public int foodMilk = 0;
  public int foodMilkshake = 0;
  public int foodPizza = 0;
  public int foodSoda = 0;
  public int foodSteak = 0;
  public int foodTaco = 0;
  public int foodWater = 0;

  public bool toyChewtoy = true;
  public bool toyCatTree = false;
  public bool toyBeachball = false;
  public bool toyLaserPointer = false;

  public bool furnitureSmallBed = true;
  public bool furnitureBeanbag = false;
  public bool furnitureFuton = false;
  public bool furnitureBigBed = false;

  public bool toolHand = true;
  public bool toolSpike = false;
  public bool toolGrabber = false;
  public bool toolLeafBlower = false;
  public bool toolBulldozer = false;

  public int toolLeafblowerEnergy = 100;
  public int toolBulldozerEnergy = 100;

  public Inventory()
  {
    foodWater = 5;
  }

  public void Unlock()
  {
    currency = 10000000;
    foodCupcake = 100;
    foodFries = 100;
    foodHamburger = 100;
    foodHotdog = 100;
    foodJuice = 100;
    foodMilk = 100;
    foodMilkshake = 100;
    foodPizza = 100;
    foodSoda = 100;
    foodSteak = 100;
    foodTaco = 100;
    foodWater = 100;
    toyChewtoy = true;
    toyCatTree = true;
    toyBeachball = true;
    toyLaserPointer = true;
    furnitureSmallBed = true;
    furnitureBeanbag = true;
    furnitureFuton = true;
    furnitureBigBed = true;
    toolHand = true;
    toolSpike = true;
    toolGrabber = true;
    toolLeafBlower = true;
    toolBulldozer = true;
  }

  public void AddCurrency(int amount)
  {
    currency += amount;
  }

  public int QtyFood(FoodScriptableObject so)
  {
    switch (so.type)
    {
      case Food.Cupcake:
        return foodCupcake;
      case Food.Fries:
        return foodFries;
      case Food.Hamburger:
        return foodHamburger;
      case Food.Hotdog:
        return foodHotdog;
      case Food.Juice:
        return foodJuice;
      case Food.Milk:
        return foodMilk;
      case Food.Milkshake:
        return foodMilkshake;
      case Food.Pizza:
        return foodPizza;
      case Food.Soda:
        return foodSoda;
      case Food.Steak:
        return foodSteak;
      case Food.Taco:
        return foodTaco;
      case Food.Water:
        return foodWater;
    }
    return 0;
  }

  public void ConsumeFood(FoodScriptableObject so)
  {
    if (so.type == Food.Cupcake)
    {
      foodCupcake--;
    }
    else if (so.type == Food.Fries)
    {
      foodFries--;
    }
    else if (so.type == Food.Hamburger)
    {
      foodHamburger--;
    }
    else if (so.type == Food.Hotdog)
    {
      foodHotdog--;
    }
    else if (so.type == Food.Juice)
    {
      foodJuice--;
    }
    else if (so.type == Food.Milk)
    {
      foodMilk--;
    }
    else if (so.type == Food.Milkshake)
    {
      foodMilkshake--;
    }
    else if (so.type == Food.Pizza)
    {
      foodPizza--;
    }
    else if (so.type == Food.Soda)
    {
      foodSoda--;
    }
    else if (so.type == Food.Steak)
    {
      foodSteak--;
    }
    else if (so.type == Food.Taco)
    {
      foodTaco--;
    }
    else if (so.type == Food.Water)
    {
      foodWater--;
    }
  }

  public void Purchase(FoodScriptableObject so)
  {
    if (currency >= so.price)
    {
      currency -= so.price;

      if (so.type == Food.Cupcake)
      {
        foodCupcake++;
      }
      else if (so.type == Food.Cupcake)
      {
        foodCupcake++;
      }
      else if (so.type == Food.Fries)
      {
        foodFries++;
      }
      else if (so.type == Food.Hamburger)
      {
        foodHamburger++;
      }
      else if (so.type == Food.Hotdog)
      {
        foodHotdog++;
      }
      else if (so.type == Food.Hotdog)
      {
        foodHotdog++;
      }
      else if (so.type == Food.Juice)
      {
        foodJuice++;
      }
      else if (so.type == Food.Milk)
      {
        foodMilk++;
      }
      else if (so.type == Food.Milkshake)
      {
        foodMilkshake++;
      }
      else if (so.type == Food.Pizza)
      {
        foodPizza++;
      }
      else if (so.type == Food.Soda)
      {
        foodSoda++;
      }
      else if (so.type == Food.Steak)
      {
        foodSteak++;
      }
      else if (so.type == Food.Taco)
      {
        foodTaco++;
      }
      else if (so.type == Food.Water)
      {
        foodWater++;
      }
    }
  }

  public void Purchase(ToyScriptableObject so)
  {
    if (currency >= so.price)
    {
      currency -= so.price;

      if (so.type == Toy.Chewtoy)
      {
        toyChewtoy = true;
      }
      else if (so.type == Toy.CatTree)
      {
        toyCatTree = true;
      }
      else if (so.type == Toy.Beachball)
      {
        toyBeachball = true;
      }
      else if (so.type == Toy.LaserPointer)
      {
        toyLaserPointer = true;
      }
    }
  }

  public bool IsPurchased(ToyScriptableObject so)
  {
    switch (so.type)
    {
      case Toy.Chewtoy:
        return toyChewtoy;
      case Toy.CatTree:
        return toyCatTree;
      case Toy.Beachball:
        return toyBeachball;
      case Toy.LaserPointer:
        return toyLaserPointer;
    }
    return false;
  }

  public void Purchase(FurnitureScriptableObject so)
  {
    if (currency >= so.price)
    {
      currency -= so.price;

      if (so.type == Furniture.SmallBed)
      {
        furnitureSmallBed = true;
      }
      else if (so.type == Furniture.Beanbag)
      {
        furnitureBeanbag = true;
      }
      else if (so.type == Furniture.Futon)
      {
        furnitureFuton = true;
      }
      else if (so.type == Furniture.BigBed)
      {
        furnitureBigBed = true;
      }
    }
  }

  public bool IsPurchased(FurnitureScriptableObject so)
  {
    switch (so.type)
    {
      case Furniture.SmallBed:
        return furnitureSmallBed;
      case Furniture.Beanbag:
        return furnitureBeanbag;
      case Furniture.Futon:
        return furnitureFuton;
      case Furniture.BigBed:
        return furnitureBigBed;
    }
    return false;
  }

  public void Purchase(ToolScriptableObject so)
  {
    if (currency > so.price)
    {
      currency -= so.price;

      if (so.type == Tool.Spike)
      {
        toolSpike = true;
      }
      else if (so.type == Tool.Grabber)
      {
        toolGrabber = true;
      }
      else if (so.type == Tool.LeafBlower)
      {
        toolLeafBlower = true;
      }
      else if (so.type == Tool.Bulldozer)
      {
        toolBulldozer = true;
      }
    }

  }

  public bool IsPurchased(ToolScriptableObject so)
  {
    switch (so.type)
    {
      case Tool.Hand:
        return toolHand;
      case Tool.Spike:
        return toolSpike;
      case Tool.Grabber:
        return toolGrabber;
      case Tool.LeafBlower:
        return toolLeafBlower;
      case Tool.Bulldozer:
        return toolBulldozer;
    }
    return false;
  }

  public void Purchase(UpgradeScriptableObject so)
  {
    if (currency > so.price)
    {
      currency -= so.price;
    }
  }

  public bool IsPurchased(UpgradeScriptableObject so)
  {
    return false;
  }
}