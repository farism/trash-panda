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
        foreach (var food in shopSO.food)
        {
            var ve = shopItem.Instantiate();

            var icon = ve.Q<VisualElement>(className: "shop-item-icon");
            icon.style.backgroundImage = food.texture;
            icon.tooltip = food.type.ToString();
            icon.AddManipulator(new UITooltip(game));

            ve.Q<Label>(className: "shop-item-name").text = food.type.ToString();
            ve.Q<Label>(className: "shop-item-energy").text = food.energy.ToString();
            ve.Q<Label>(className: "shop-item-hunger").text = food.hunger.ToString();
            ve.Q<Label>(className: "shop-item-love").text = food.love.ToString();
            ve.Q<Label>(className: "shop-item-purchase-price").text = food.price.ToString();
            ve.Q<Button>(className: "shop-item-purchase").clicked += () =>
            {
                Debug.Log("Purchasing" + food.type.ToString());
                game.inventory.Purchase(food);
            };
            foodItems.Add(ve);
        }

        toys = shop.Q<VisualElement>("Toys");
        var toyItems = toys.Q<ScrollView>("Items");
        foreach (var toy in shopSO.toys)
        {
            var ve = shopItem.Instantiate();
            ve.Q<VisualElement>(className: "shop-item-icon").style.backgroundImage = toy.texture;
            ve.Q<Label>(className: "shop-item-name").text = toy.type.ToString();
            ve.Q<Label>(className: "shop-item-energy").text = toy.energy.ToString();
            ve.Q<Label>(className: "shop-item-hunger").text = toy.hunger.ToString();
            ve.Q<Label>(className: "shop-item-love").text = toy.love.ToString();
            ve.Q<Label>(className: "shop-item-purchase-price").text = toy.price.ToString();
            ve.Q<Button>(className: "shop-item-purchase").clicked += () => Debug.Log("Purchasing" + toy.type.ToString());
            toyItems.Add(ve);
        }

        furniture = shop.Q<VisualElement>("Furniture");
        var furnitureItems = furniture.Q<ScrollView>("Items");
        foreach (var furniture in shopSO.furniture)
        {
            var ve = shopItem.Instantiate();
            ve.Q<VisualElement>(className: "shop-item-icon").style.backgroundImage = furniture.texture;
            ve.Q<Label>(className: "shop-item-energy").text = furniture.energy.ToString();
            ve.Q<Label>(className: "shop-item-name").text = furniture.type.ToString();
            ve.Q<Label>(className: "shop-item-purchase-price").text = furniture.price.ToString();
            furnitureItems.Add(ve);
        }

        tools = shop.Q<VisualElement>("Tools");
        var toolItems = tools.Q<ScrollView>("Items");
        foreach (var tool in shopSO.tools)
        {
            var ve = shopItem.Instantiate();
            ve.Q<VisualElement>(className: "shop-item-icon").style.backgroundImage = tool.texture;
            ve.Q<Label>(className: "shop-item-energy").text = tool.energy.ToString();
            ve.Q<Label>(className: "shop-item-hunger").text = tool.hunger.ToString();
            ve.Q<Label>(className: "shop-item-love").text = tool.love.ToString();
            ve.Q<Label>(className: "shop-item-name").text = tool.type.ToString();
            ve.Q<Label>(className: "shop-item-purchase-price").text = tool.price.ToString();
            toolItems.Add(ve);
        }

        upgrades = shop.Q<VisualElement>("Upgrades");
        var upgradeItems = upgrades.Q<ScrollView>("Items");
        foreach (var upgrade in shopSO.upgrades)
        {
            var ve = shopItem.Instantiate();
            ve.Q<VisualElement>(className: "shop-item-icon").style.backgroundImage = upgrade.texture;
            ve.Q<Label>(className: "shop-item-name").text = upgrade.type.ToString();
            ve.Q<Label>(className: "shop-item-purchase-price").text = upgrade.price.ToString();
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
}
