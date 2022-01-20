using UnityEngine;

public enum Toy
{
    Beachball,
    CatTree,
    Chewtoy,
    LaserPointer,
}

[CreateAssetMenu(fileName = "ToyScriptableObject", menuName = "ScriptableObjects/ToyScriptableObject", order = 1)]
public class ToyScriptableObject : ScriptableObject
{
    public Toy type;
    public Texture2D texture;
    public int price;
    public int energy;
    public int hunger;
    public int love;
}
