using UnityEngine;

[CreateAssetMenu(fileName = "ShopScriptableObject", menuName = "ScriptableObjects/ShopScriptableObject", order = 1)]
public class ShopScriptableObject : ScriptableObject
{
    public FoodScriptableObject[] food;
    public FurnitureScriptableObject[] furniture;
    public ToyScriptableObject[] toys;
    public ToolScriptableObject[] tools;
    public UpgradeScriptableObject[] upgrades;
}