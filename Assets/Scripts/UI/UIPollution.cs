using UnityEngine;
using UnityEngine.UIElements;

public class UIPollution : MonoBehaviour
{
    public Game game;
    VisualElement root;
    VisualElement pollution;
    VisualElement fill;

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        pollution = root.Q<VisualElement>("Pollution");
        fill = root.Q<VisualElement>("PollutionFill");
    }

    void Update()
    {
        pollution.tooltip = "Global Pollution: " + Mathf.RoundToInt(game.pollutionRatio * 100) + "%";
        fill.style.backgroundColor = Color.Lerp(Color.white, Color.red, game.pollutionRatio);
        fill.style.width = Length.Percent(game.pollutionRatio * 100);
    }
}
