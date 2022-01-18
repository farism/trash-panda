using UnityEngine;

public enum Furniture
{
    Beanbag,
    BigBed,
    Futon,
    SmallBed,
}

[CreateAssetMenu(fileName = "FurnitureScriptableObject", menuName = "ScriptableObjects/FurnitureScriptableObject", order = 1)]
public class FurnitureScriptableObject : ScriptableObject
{
    public Furniture type;
    public Texture2D texture;
    public int price;
    public int energy;
}
