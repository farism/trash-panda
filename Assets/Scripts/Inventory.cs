using System.Collections.Generic;
using System;

public class Inventory
{
    public int currency { get; set; } = 100;

    public bool toolHand { get; private set; } = true;

    public int toolLeafblowerEnergy { get; private set; } = 0;

    public int toolBulldozerEnergy { get; private set; } = 0;

    public Dictionary<Food, int> food = new Dictionary<Food, int>();

    public Dictionary<Toy, bool> toys = new Dictionary<Toy, bool>();

    public Dictionary<Furniture, bool> furniture = new Dictionary<Furniture, bool>();

    public Dictionary<Tool, bool> tools = new Dictionary<Tool, bool>();

    public Dictionary<Upgrade, bool> upgrades = new Dictionary<Upgrade, bool>();

    public int Qty(FoodScriptableObject so)
    {
        return food.GetValueOrDefault(so.type, 0);
    }

    public void Purchase(FoodScriptableObject so)
    {
        if (currency < so.price)
        {
            return;
        }

        currency -= so.price;

        Add(this.food, so.type);
    }

    public void Consume(FoodScriptableObject so)
    {
        if (food.GetValueOrDefault(so.type, 0) > 0)
        {
            Subtract(food, so.type);
        }
    }

    void Add<T>(Dictionary<T, int> dic, T key) where T : Enum
    {
        var val = dic.GetValueOrDefault(key, 0);

        dic[key] = val + 1;
    }

    void Subtract<T>(Dictionary<T, int> dic, T key) where T : Enum
    {
        if (dic.ContainsKey(key))
        {
            dic[key] += -1;
        }
    }
}