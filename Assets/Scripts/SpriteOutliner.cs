using System.Collections;
using UnityEngine;
using DG.Tweening;

public class SpriteOutliner : MonoBehaviour
{
  public UI ui;
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

    var material = new Material(Shader.Find("Sprites/Outline"));
    renderer = GetComponent<SpriteRenderer>();
    material.mainTexture = renderer.sprite.texture;
    material.SetColor("_SolidOutline", Color.clear);
    material.SetFloat("_Thickness", thickness);
    renderer.material = material;
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
    if (!ui.isMouseOverUI)
    {
      ui.ShowHandCursor();
      ShowOutline();
    }
  }

  void OnMouseExit()
  {
    if (!ui.isMouseOverUI)
    {
      ui.ShowDefaultCursor();
    }

    HideOutline();
  }

  void OnMouseOver()
  {
    if (!ui.isMouseOverUI)
    {
      ui.ShowHandCursor();
      renderer.material.SetColor("_SolidOutline", color);
    }
  }
}
