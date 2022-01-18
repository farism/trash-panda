using UnityEngine;

public enum Tool
{
    Bulldozer,
    Grabber,
    Hand,
    LeafBlower,
    Spike,
}

[CreateAssetMenu(fileName = "ToolScriptableObject", menuName = "ScriptableObjects/ToolScriptableObject", order = 1)]
public class ToolScriptableObject : ScriptableObject
{
    public Tool type;
    public Texture2D texture;
    public int price;
    public int energy;
    public int hunger;
    public int love;
}
