using UnityEngine;

public enum Upgrade
{
    Bulldozer1,
    Bulldozer2,
    Grabber1,
    Grabber2,
    Leafblower1,
    Leafblower2,
    Spike1,
    Spike2,
}

[CreateAssetMenu(fileName = "UpgradeScriptableObject", menuName = "ScriptableObjects/UpgradeScriptableObject", order = 1)]
public class UpgradeScriptableObject : ScriptableObject
{
    public Upgrade type;
    public Texture2D texture;
    public int price;
}
