using UnityEngine;
using UnityEngine.UIElements;

public class UICurrency : MonoBehaviour
{
    public Game game;

    VisualElement root;
    Label currency;

    float lerpedCurrency = 0;

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        currency = root.Q<Label>("CurrencyText");
    }

    void Update()
    {
        lerpedCurrency = Mathf.Lerp(lerpedCurrency, game.inventory.currency, 0.025f);

        currency.text = Mathf.RoundToInt(lerpedCurrency).ToString();
    }
}
