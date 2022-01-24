using UnityEngine;
using UnityEngine.UIElements;

public class UITooltip : Manipulator
{
  Game game;
  VisualElement tooltip { get => game.ui.tooltip; }

  public UITooltip(Game game)
  {
    this.game = game;
  }

  protected override void RegisterCallbacksOnTarget()
  {
    target.RegisterCallback<MouseEnterEvent>(MouseIn);
    target.RegisterCallback<MouseOutEvent>(MouseOut);
    target.RegisterCallback<MouseMoveEvent>(MouseMove);
  }

  protected override void UnregisterCallbacksFromTarget()
  {
    target.UnregisterCallback<MouseEnterEvent>(MouseIn);
    target.UnregisterCallback<MouseOutEvent>(MouseOut);
    target.UnregisterCallback<MouseMoveEvent>(MouseMove);
  }

  private void MouseIn(MouseEnterEvent e)
  {
    if (game.view == View.Job)
    {
      return;
    }

    game.ui.SetTooltip(this);
  }

  private void MouseOut(MouseOutEvent e)
  {
    tooltip.RemoveFromClassList("active");
  }

  private void MouseMove(MouseMoveEvent e)
  {
    game.ui.SetTooltip(this);
    var mx = Input.mousePosition.x;
    var my = Input.mousePosition.y;
    var ttw = tooltip.resolvedStyle.width;
    var tth = tooltip.resolvedStyle.height;
    var tw = target.resolvedStyle.width;
    var th = target.resolvedStyle.height;
    var offset = Mathf.Max(32, th / 2);
    tooltip.style.left = mx - ttw / 2;

    if (my > Screen.height / 2)
    {
      tooltip.style.top = Screen.height - my + offset + 128;  // top half of screen, tooltip goes below
    }
    else
    {
      tooltip.style.top = Screen.height - my - tth - offset;
    }
  }
}