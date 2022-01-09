using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    public Game game;
    public Texture2D soundOn;
    public Texture2D soundOff;
    VisualElement root;
    Button homeBtn;
    Button shopBtn;
    Button jobsBtn;
    Button soundBtn;
    ProgressBar energy;
    ProgressBar hunger;
    ProgressBar affection;
    VisualElement home;
    VisualElement shop;
    VisualElement jobs;
    VisualElement location1;
    VisualElement location2;
    VisualElement location3;
    VisualElement location4;
    VisualElement location5;
    VisualElement[] locations;

    public void ShowJobLocation(int index, Vector3 position)
    {
        var screenPos = Camera.main.WorldToScreenPoint(position);
        locations[index].style.left = screenPos.x;
        locations[index].style.top = Screen.height - screenPos.y;
        locations[index].style.opacity = 1;
    }

    public void HideJobLocation(int index)
    {
        locations[index].style.opacity = 0;
    }

    void Awake()
    {
    }

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        // controls
        homeBtn = root.Q<Button>("HomeBtn");
        shopBtn = root.Q<Button>("ShopBtn");
        jobsBtn = root.Q<Button>("JobsBtn");
        soundBtn = root.Q<Button>("SoundBtn");

        // statuses
        energy = root.Q<ProgressBar>("Energy");
        hunger = root.Q<ProgressBar>("Hunger");
        affection = root.Q<ProgressBar>("Affection");

        // containers
        home = root.Q<VisualElement>("Home");
        shop = root.Q<VisualElement>("Shop");
        jobs = root.Q<VisualElement>("Jobs");
        HideContainers();

        location1 = root.Q<VisualElement>("Location1");
        location2 = root.Q<VisualElement>("Location2");
        location3 = root.Q<VisualElement>("Location3");
        location4 = root.Q<VisualElement>("Location4");
        location5 = root.Q<VisualElement>("Location5");
        locations = new VisualElement[] { location1, location2, location3, location4, location5 };

        // event handlers
        homeBtn.clicked += () => OnClickView(View.Home, home);
        shopBtn.clicked += () => OnClickView(View.Shop, shop);
        jobsBtn.clicked += () => OnClickView(View.Jobs, jobs);
        soundBtn.clicked += () =>
        {
            game.ToggleSound();
            soundBtn.style.backgroundImage = game.sound ? soundOn : soundOff;
        };
    }

    void Update()
    {
        energy.value = game.energy;
        hunger.value = game.hunger;
        affection.value = game.affection;
    }

    void HideContainers()
    {
        home.style.display = DisplayStyle.None;
        shop.style.display = DisplayStyle.None;
        jobs.style.display = DisplayStyle.None;
    }

    void OnClickView(View view, VisualElement ve)
    {
        HideContainers();
        ve.style.display = DisplayStyle.Flex;
        game.SetView(view);
    }
}
