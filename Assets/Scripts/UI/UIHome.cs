using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHome : MonoBehaviour, IScreen
{
    public Game game;
    public ShopScriptableObject shopSO;
    public bool isMouseOverUI = false;

    VisualElement root;
    VisualElement home;
    VisualElement activeItem;
    Button foodBtn;
    Button toysBtn;
    VisualElement food;
    VisualElement toys;
    List<VisualElement> foodBtns = new List<VisualElement>();
    List<VisualElement> toyBtns = new List<VisualElement>();
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

        activeItem = root.Q<VisualElement>("ActiveItem");

        home = root.Q<VisualElement>("Home");

        food = home.Q<VisualElement>("Food");
        var foodItems = food.Q<ScrollView>("Items");
        foreach (var so in shopSO.food)
        {
            var btn = new VisualElement();
            btn.AddToClassList("food-button");
            btn.Add(CreateButtonIcon(so.texture));
            btn.Add(CreateButtonLabel());
            btn.RegisterCallback<MouseDownEvent>((ctx) =>
            {
                SetActiveFood(so);
            });
            foodItems.Add(btn);
            foodBtns.Add(btn);
        }

        toys = home.Q<VisualElement>("Toys");
        var toyItems = toys.Q<ScrollView>("Items");
        foreach (var so in shopSO.toys)
        {
            var btn = new VisualElement();
            btn.AddToClassList("toy-button");
            btn.Add(CreateButtonIcon(so.texture));
            btn.RegisterCallback<MouseDownEvent>((ctx) =>
            {
                SetActiveToy(so);
                UI.Hide(toys);
            });
            toyItems.Add(btn);
            toyBtns.Add(btn);
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

        foreach (var btn in home.Query<Button>().ToList())
        {
            btn.RegisterCallback<MouseEnterEvent>((ctx) =>
            {
                isMouseOverUI = true;
                foreach (var outliner in FindObjectsOfType<SpriteOutliner>())
                {
                    outliner.Hide();
                }
            });
            btn.RegisterCallback<MouseLeaveEvent>((ctx) => isMouseOverUI = false);
        }

        UI.Hide(food);
        UI.Hide(toys);
        Hide();
    }

    void Update()
    {
        if (game.view == View.Home)
        {
            UpdateQuantities();

            UpdateToys();

            UpdateMoving();

            UpdateActiveItem();

            if (Input.GetMouseButtonDown(1))
            {
                UI.Hide(food);
                UI.Hide(toys);
            }
        }
    }

    void UpdateMoving()
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
            var qty = game.inventory.QtyFood(food);

            if (qty > 0)
            {
                btn.style.display = DisplayStyle.Flex;
                btn.Q<Label>().text = qty.ToString();
            }
            else
            {
                btn.style.display = DisplayStyle.None;
            }
        }
    }

    void UpdateToys()
    {
        for (var i = 0; i < toyBtns.Count; i++)
        {
            var btn = toyBtns[i];
            var toy = shopSO.toys[i];
            btn.style.display = game.inventory.IsPurchased(toy) ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }

    void UpdateActiveItem()
    {
        if (activeFood != null || activeToy != null)
        {
            SetActiveItemPosition();

            if (Input.GetMouseButtonUp(0))
            {
                if (activeFood)
                {
                    game.home.Feed(activeFood);
                }
                else if (activeToy)
                {
                    game.home.Play(activeToy);
                }

                activeItem.style.display = DisplayStyle.None;
                activeFood = null;
                activeToy = null;
            }
        }
    }

    VisualElement CreateButtonIcon(Texture2D texture)
    {
        var icon = new VisualElement();
        icon.AddToClassList("frame");
        icon.AddToClassList("button-icon");
        icon.pickingMode = PickingMode.Ignore;
        icon.style.backgroundImage = texture;

        return icon;
    }

    Label CreateButtonLabel()
    {
        var label = new Label();
        label.AddToClassList("button-label");
        label.pickingMode = PickingMode.Ignore;

        return label;
    }

    void SetActiveItemPosition()
    {
        activeItem.style.left = Input.mousePosition.x;
        activeItem.style.top = Screen.height - Input.mousePosition.y;
    }

    void SetActiveFood(FoodScriptableObject so)
    {
        activeFood = so;
        SetActiveItemPosition();
        activeItem.style.backgroundImage = so.texture;
        activeItem.style.display = DisplayStyle.Flex;
    }

    void SetActiveToy(ToyScriptableObject so)
    {
        activeToy = so;
        SetActiveItemPosition();
        activeItem.style.backgroundImage = so.texture;
        activeItem.style.display = DisplayStyle.Flex;
    }
}
