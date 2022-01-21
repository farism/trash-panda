using UnityEngine;

public class Bed : MonoBehaviour
{
    public FurnitureScriptableObject furniture;

    Game game;
    UIHome uihome;
    SpriteRenderer spriteRenderer;
    SpriteOutliner spriteOutliner;

    void Start()
    {
        game = FindObjectOfType<Game>();
        uihome = FindObjectOfType<UIHome>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteOutliner = GetComponent<SpriteOutliner>();
        gameObject.SetActive(false);
    }

    void OnMouseDown()
    {
        if (!uihome.isMouseOverUI)
        {
            if (game.sleepCooldown == 0)
            {
                game.home.Sleep(furniture);
                spriteOutliner.color = Color.gray;
            }
        }
    }

    void Update()
    {
        if (game.view == View.Home)
        {
            spriteOutliner.color = game.sleepCooldown == 0 ? Color.yellow : Color.gray;
            spriteRenderer.color = Color.Lerp(Color.white, Color.gray, game.sleepCooldown / 30);
        }
    }
}
