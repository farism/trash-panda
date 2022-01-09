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
