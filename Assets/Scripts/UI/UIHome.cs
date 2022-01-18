using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHome : MonoBehaviour, IScreen
{
    public Game game;
    public ShopScriptableObject shopSO;

    VisualElement root;
    VisualElement home;
    VisualElement activeItem;
    Button foodBtn;
    Button toysBtn;
    VisualElement food;
    VisualElement toys;
    List<Button> foodBtns = new List<Button>();
    List<Button> toyBtns = new List<Button>();
    FoodScriptableObject activeFood;
    ToyScriptableObject activeToy;
    bool prevActive = false;

    public void Show()
    {
        UI.Show(home);
    }

    public void Hide()
    {
        UI.Hide(home);
    }

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        home = root.Q<VisualElement>("Home");

        activeItem = home.Q<VisualElement>("ActiveItem");

        food = home.Q<VisualElement>("Food");
        var foodItems = food.Q<ScrollView>("Items");
        foreach (var food in shopSO.food)
        {
            var btn = new Button();
            btn.AddToClassList("food-button");
            btn.style.backgroundImage = food.texture;
            btn.style.unityBackgroundScaleMode = ScaleMode.ScaleToFit;
            btn.RegisterCallback<MouseDownEvent>((ctx) =>
            {
                activeFood = food;
            });
            foodItems.Add(btn);
            foodBtns.Add(btn);
        }

        toys = home.Q<VisualElement>("Toys");
        var toyItems = toys.Q<ScrollView>("Items");
        foreach (var toy in shopSO.toys)
        {
            var btn = new Button();
            btn.AddToClassList("toy-button");
            btn.style.backgroundImage = toy.texture;
            btn.style.unityBackgroundScaleMode = ScaleMode.ScaleToFit;
            btn.RegisterCallback<MouseDownEvent>((ctx) =>
            {
                activeToy = toy;
            });
            toyItems.Insert(0, btn);
            toyBtns.Insert(0, btn);
        }

        foodBtn = home.Q<Button>("FoodBtn");
        foodBtn.clicked += () =>
        {
            UI.Hide(toys);
            UI.Toggle(food);
        };

        toysBtn = home.Q<Button>("ToysBtn");
        toysBtn.clicked += () =>
        {
            UI.Hide(food);
            UI.Toggle(toys);
        };

        UI.Hide(food);
        UI.Hide(toys);
        Hide();
    }

    void Update()
    {
        if (game.view == View.Home)
        {
            UpdateQuantities();

            UpdateButtonVisibility();

            UpdateActiveItem();

            if (Input.GetMouseButtonDown(1))
            {
                UI.Hide(food);
                UI.Hide(toys);
            }

            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log(activeToy);
            }
        }
    }

    void UpdateButtonVisibility()
    {
        var currentActive = game.home.raccoonSide.activeSelf;

        if (prevActive != currentActive)
        {
            if (currentActive)
            {
                home.AddToClassList("moving");
            }
            else
            {
                home.RemoveFromClassList("moving");
            }

            prevActive = currentActive;
        }
    }

    void UpdateQuantities()
    {
        for (var i = 0; i < foodBtns.Count; i++)
        {
            var btn = foodBtns[i];
            var food = shopSO.food[i];
            var qty = game.inventory.Qty(food);

            btn.style.display = qty == 0 ? DisplayStyle.None : DisplayStyle.Flex;
        }
    }

    void UpdateActiveItem()
    {

    }
}
