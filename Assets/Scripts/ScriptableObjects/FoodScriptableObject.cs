using UnityEngine;

public enum Food
{
    Cupcake,
    Fries,
    Hamburger,
    Hotdog,
    Juice,
    Milk,
    Milkshake,
    Pizza,
    Soda,
    Steak,
    Taco,
    Water,
}

[CreateAssetMenu(fileName = "FoodScriptableObject", menuName = "ScriptableObjects/FoodScriptableObject", order = 2)]
public class FoodScriptableObject : ScriptableObject
{
    public Food type;
    public Texture2D texture;
    public int price;
    public int energy;
    public int hunger;
    public int love;
}
