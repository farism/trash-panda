using System.Collections;
using UnityEngine;
using DG.Tweening;

public class SpriteOutliner : MonoBehaviour
{
    public Color color = Color.clear;
    public float thickness = 4;

    new SpriteRenderer renderer;
    Material material;

    void Start()
    {
        var material = new Material(Shader.Find("Sprites/Outline"));
        renderer = GetComponent<SpriteRenderer>();
        material.mainTexture = renderer.sprite.texture;
        material.SetColor("_SolidOutline", Color.clear);
        material.SetFloat("_Thickness", thickness);
        renderer.material = material;
    }

    void OnMouseEnter()
    {
        renderer.material.DOColor(Color.yellow, "_SolidOutline", 0.3f);
    }

    void OnMouseExit()
    {
        renderer.material.DOColor(Color.clear, "_SolidOutline", 0.3f);
    }
}
