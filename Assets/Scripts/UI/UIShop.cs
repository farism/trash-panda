using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;

public class UIShop : MonoBehaviour, IScreen
{
    VisualTreeAsset shopItem;

    public Game game;
    public ShopScriptableObject shopSO;

    VisualElement root;
    VisualElement shop;
    VisualElement shopTabs;
    VisualElement activeCategory;
    Button foodBtn;
    Button toysBtn;
    Button furnitureBtn;
    Button toolsBtn;
    Button upgradesBtn;
    VisualElement food;
    VisualElement toys;
    VisualElement furniture;
    VisualElement tools;
    VisualElement upgrades;

    public void Show()
    {
        UI.Show(shop);
    }

    public void Hide()
    {
        UI.Hide(shop);
    }

    void Start()
    {
        shopItem = Resources.Load<VisualTreeAsset>("Views/ShopItem");

        root = GetComponent<UIDocument>().rootVisualElement;

        shop = root.Q<VisualElement>("Shop");

        shopTabs = root.Q<VisualElement>("ShopTabs");

        food = shop.Q<VisualElement>("Food");
        var foodItems = food.Q<ScrollView>("Items");
        foreach (var so in shopSO.food)
        {
            var ve = shopItem.Instantiate();
            ItemIcon(ve, so.texture, so.type.ToString());

            ve.Q<Label>(className: "shop-item-name").text = so.type.ToString();
            ve.Q<Label>(className: "shop-item-energy").text = so.energy.ToString();
            ve.Q<Label>(className: "shop-item-hunger").text = so.hunger.ToString();
            ve.Q<Label>(className: "shop-item-love").text = so.love.ToString();
            ve.Q<Label>(className: "shop-item-purchase-price").text = so.price.ToString();
            ve.Q<Button>(className: "shop-item-purchase").clicked += () => game.inventory.Purchase(so);
            foodItems.Add(ve);
        }

        toys = shop.Q<VisualElement>("Toys");
        var toyItems = toys.Q<ScrollView>("Items");
        foreach (var so in shopSO.toys)
        {
            var ve = shopItem.Instantiate();
            ItemIcon(ve, so.texture, so.type.ToString());
            TogglePurchased(so, ve);

            ve.Q<VisualElement>(className: "shop-item-icon").style.backgroundImage = so.texture;
            ve.Q<Label>(className: "shop-item-name").text = so.type.ToString();
            ve.Q<Label>(className: "shop-item-energy").text = so.energy.ToString();
            ve.Q<Label>(className: "shop-item-hunger").text = so.hunger.ToString();
            ve.Q<Label>(className: "shop-item-love").text = so.love.ToString();
            ve.Q<Label>(className: "shop-item-purchase-price").text = so.price.ToString();
            ve.Q<Button>(className: "shop-item-purchase").clicked += () =>
            {
                game.inventory.Purchase(so);
                TogglePurchased(so, ve);
            };
            toyItems.Add(ve);
        }

        furniture = shop.Q<VisualElement>("Furniture");
        var furnitureItems = furniture.Q<ScrollView>("Items");
        foreach (var so in shopSO.furniture)
        {
            var ve = shopItem.Instantiate();
            ItemIcon(ve, so.texture, so.type.ToString());
            TogglePurchased(so, ve);

            ve.Q<Label>(className: "shop-item-energy").text = so.energy.ToString();
            ve.Q<Label>(className: "shop-item-name").text = so.type.ToString();
            ve.Q<Label>(className: "shop-item-purchase-price").text = so.price.ToString();
            ve.Q<Button>(className: "shop-item-purchase").clicked += () =>
            {
                game.inventory.Purchase(so);
                game.home.UpdateFurniture();
                TogglePurchased(so, ve);
            };
            furnitureItems.Add(ve);
        }

        tools = shop.Q<VisualElement>("Tools");
        var toolItems = tools.Q<ScrollView>("Items");
        foreach (var so in shopSO.tools)
        {
            var ve = shopItem.Instantiate();
            ItemIcon(ve, so.texture, so.type.ToString());
            TogglePurchased(so, ve);

            ve.Q<Label>(className: "shop-item-energy").text = so.energy.ToString();
            ve.Q<Label>(className: "shop-item-hunger").text = so.hunger.ToString();
            ve.Q<Label>(className: "shop-item-love").text = so.love.ToString();
            ve.Q<Label>(className: "shop-item-name").text = so.type.ToString();
            ve.Q<Label>(className: "shop-item-purchase-price").text = so.price.ToString();
            ve.Q<Button>(className: "shop-item-purchase").clicked += () =>
            {
                game.inventory.Purchase(so);
                TogglePurchased(so, ve);
            };
            toolItems.Add(ve);
        }

        upgrades = shop.Q<VisualElement>("Upgrades");
        var upgradeItems = upgrades.Q<ScrollView>("Items");
        foreach (var so in shopSO.upgrades)
        {
            var ve = shopItem.Instantiate();
            ItemIcon(ve, so.texture, so.type.ToString());
            TogglePurchased(so, ve);

            ve.Q<VisualElement>(className: "shop-item-icon").style.backgroundImage = so.texture;
            ve.Q<Label>(className: "shop-item-name").text = so.type.ToString();
            ve.Q<Label>(className: "shop-item-purchase-price").text = so.price.ToString();
            ve.Q<Button>(className: "shop-item-purchase").clicked += () =>
            {
                game.inventory.Purchase(so);
                TogglePurchased(so, ve);
            };
            upgradeItems.Add(ve);
        }

        foodBtn = shopTabs.Q<Button>("FoodBtn");
        foodBtn.clicked += () => ShowCategory(foodBtn, food);
        toysBtn = shopTabs.Q<Button>("ToysBtn");
        toysBtn.clicked += () => ShowCategory(toysBtn, toys);
        furnitureBtn = shopTabs.Q<Button>("FurnitureBtn");
        furnitureBtn.clicked += () => ShowCategory(furnitureBtn, furniture);
        toolsBtn = shopTabs.Q<Button>("ToolsBtn");
        toolsBtn.clicked += () => ShowCategory(toolsBtn, tools);
        upgradesBtn = shopTabs.Q<Button>("UpgradesBtn");
        upgradesBtn.clicked += () => ShowCategory(upgradesBtn, upgrades);

        UI.Hide(toys);
        UI.Hide(furniture);
        UI.Hide(tools);
        UI.Hide(upgrades);
        ShowCategory(foodBtn, food);
        Hide();
    }

    void Update()
    {
        if (game.view == View.Shop)
        {
        }
    }

    void ItemIcon(VisualElement ve, Texture2D texture, string tooltip)
    {
        var icon = ve.Q<VisualElement>(className: "shop-item-icon");
        icon.style.backgroundImage = texture;
        // icon.tooltip = String.Join(" ", Regex.Split(tooltip, @"(?<!^)(?=[A-Z])"));
        // icon.AddManipulator(new UITooltip(game));
    }

    void ShowCategory(VisualElement btn, VisualElement ve)
    {
        foodBtn.RemoveFromClassList("active");
        toysBtn.RemoveFromClassList("active");
        furnitureBtn.RemoveFromClassList("active");
        toolsBtn.RemoveFromClassList("active");
        upgradesBtn.RemoveFromClassList("active");
        btn.AddToClassList("active");

        if (activeCategory != null)
        {
            UI.Hide(activeCategory);
        }

        activeCategory = ve;

        UI.Show(activeCategory);
    }

    void TogglePurchased(ToyScriptableObject so, VisualElement ve)
    {
        if (game.inventory.IsPurchased(so))
        {
            ve.AddToClassList("purchased");
        }
    }

    void TogglePurchased(FurnitureScriptableObject so, VisualElement ve)
    {
        if (game.inventory.IsPurchased(so))
        {
            ve.AddToClassList("purchased");
        }
    }

    void TogglePurchased(ToolScriptableObject so, VisualElement ve)
    {
        if (game.inventory.IsPurchased(so))
        {
            ve.AddToClassList("purchased");
        }
    }

    void TogglePurchased(UpgradeScriptableObject so, VisualElement ve)
    {
        if (game.inventory.IsPurchased(so))
        {
            ve.AddToClassList("purchased");
        }
    }
}
