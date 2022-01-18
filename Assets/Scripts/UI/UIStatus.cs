using UnityEngine;
using UnityEngine.UIElements;

public class UIStatus : MonoBehaviour
{
    public Game game;

    VisualElement root;
    VisualElement energyStatus;
    VisualElement energy;
    VisualElement hungerStatus;
    VisualElement hunger;
    VisualElement loveStatus;
    VisualElement love;

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        energyStatus = root.Q<VisualElement>("EnergyStatus");
        energy = root.Q<VisualElement>("Energy");
        hungerStatus = root.Q<VisualElement>("HungerStatus");
        hunger = root.Q<VisualElement>("Hunger");
        loveStatus = root.Q<VisualElement>("LoveStatus");
        love = root.Q<VisualElement>("Love");
    }

    void Update()
    {
        energy.style.width = Length.Percent(game.stats.energy);
        hunger.style.width = Length.Percent(game.stats.hunger);
        love.style.width = Length.Percent(game.stats.love);
        energyStatus.tooltip = "Energy: " + Mathf.RoundToInt(game.stats.energy) + "%";
        hungerStatus.tooltip = "Hunger: " + Mathf.RoundToInt(game.stats.hunger) + "%";
        loveStatus.tooltip = "Love: " + Mathf.RoundToInt(game.stats.love) + "%";
    }
}