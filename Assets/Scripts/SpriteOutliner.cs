using System.Collections;
using UnityEngine;
using DG.Tweening;

public class SpriteOutliner : MonoBehaviour
{
    public UI ui;
    public UIHome uihome;
    public Color color = Color.clear;
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

    void OnMouseEnter()
    {
        if (!uihome.isMouseOverUI && renderer.material.color != Color.yellow)
        {
            renderer.material.DOColor(Color.yellow, "_SolidOutline", 0.3f);
        }
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(ui.defaultCursor, new Vector2(10, 10), CursorMode.Auto);
        renderer.material.DOColor(Color.clear, "_SolidOutline", 0.3f);
    }

    void OnMouseOver()
    {
        Cursor.SetCursor(ui.handCursor, new Vector2(10, 10), CursorMode.Auto);

        if (!uihome.isMouseOverUI && renderer.material.color != Color.yellow)
        {
            renderer.material.DOColor(Color.yellow, "_SolidOutline", 0.3f);
        }
    }

    void OnMouseDown()
    {
        if (!uihome.isMouseOverUI)
        {
            Debug.Log("using sprite item");
        }
    }
}
