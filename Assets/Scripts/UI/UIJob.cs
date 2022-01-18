using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIJob : MonoBehaviour, IScreen
{
    public Game game;
    VisualElement root;
    VisualElement job;

    public void Show()
    {
        UI.Show(job);
    }

    public void Hide()
    {
        UI.Hide(job);
    }

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        job = root.Q<VisualElement>("Job");

        Hide();
    }

    void Update()
    {
        if (game.view == View.Job)
        {
        }
    }

    void UpdateStats()
    {
        // energy.value = game.energy;
        // hunger.value = game.hunger;
        // love.value = game.love;
    }
}
