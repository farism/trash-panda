using System.Collections;
using UnityEngine;
using DG.Tweening;

public class SpriteOutliner : MonoBehaviour
{
    public UI ui;
    public UIHome uihome;
    public Color color = Color.yellow;
    public float thickness = 4;

    new SpriteRenderer renderer;
    Material material;

    public void Hide()
    {
        renderer.material.DOColor(Color.clear, "_SolidOutline", 0.3f);
    }

    void Start()
    {
        ui = FindObjectOfType<UI>();
        uihome = FindObjectOfType<UIHome>();

        var material = new Material(Shader.Find("Sprites/Outline"));
        renderer = GetComponent<SpriteRenderer>();
        material.mainTexture = renderer.sprite.texture;
        material.SetColor("_SolidOutline", Color.clear);
        material.SetFloat("_Thickness", thickness);
        renderer.material = material;
    }

    void ShowDefaultCursor()
    {
        Cursor.SetCursor(ui.defaultCursor, new Vector2(12, 12), CursorMode.Auto);
    }

    void ShowHandCursor()
    {
        Cursor.SetCursor(ui.handCursor, new Vector2(12, 12), CursorMode.Auto);
    }

    void ShowOutline()
    {
        renderer.material.DOColor(color, "_SolidOutline", 0.3f);
    }

    void HideOutline()
    {
        renderer.material.DOColor(Color.clear, "_SolidOutline", 0.3f);
    }

    void OnMouseEnter()
    {
        if (!uihome.isMouseOverUI)
        {
            ShowHandCursor();
            ShowOutline();
        }
    }

    void OnMouseExit()
    {
        if (!uihome.isMouseOverUI)
        {
            ShowDefaultCursor();
        }

        HideOutline();
    }

    void OnMouseOver()
    {
        if (!uihome.isMouseOverUI)
        {
            ShowHandCursor();
            renderer.material.SetColor("_SolidOutline", color);
        }
    }
}
