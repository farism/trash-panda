using System;
using UnityEngine;

public class Konami : MonoBehaviour
{
  public Game game;

  Array keys;
  KeyCode[] konami;
  int currentKeyIndex = 0;

  void Start()
  {
    keys = Enum.GetValues(typeof(KeyCode));

    konami = new KeyCode[] {
      KeyCode.UpArrow,
      KeyCode.UpArrow,
      KeyCode.DownArrow,
      KeyCode.DownArrow,
      KeyCode.LeftArrow,
      KeyCode.RightArrow,
      KeyCode.LeftArrow,
      KeyCode.RightArrow,
      KeyCode.B,
      KeyCode.A,
    };
  }

  void Update()
  {
    foreach (KeyCode keyCode in keys)
    {
      if (Input.GetKeyDown(keyCode))
      {
        OnInput(keyCode);
      }
    }
  }

  void OnInput(KeyCode keycode)
  {
    if (keycode == konami[currentKeyIndex])
    {
      currentKeyIndex++;

      if (currentKeyIndex >= konami.Length)
      {
        currentKeyIndex = 0;
        game.inventory.Unlock();
      }
    }
    else
    {
      currentKeyIndex = 0;
    }
  }
}
