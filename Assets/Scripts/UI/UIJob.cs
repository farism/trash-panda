using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIJob : MonoBehaviour, IScreen
{
  public Game game;
  VisualElement root;
  VisualElement job;
  Label pollution;
  Label held;
  Button skill0;
  Button skill1;
  Button skill2;
  Button skill3;
  Button skill4;
  VisualElement skill3Fuel;
  VisualElement skill4Fuel;
  List<VisualElement> cooldowns;

  public void Show()
  {
    UI.Show(job);
  }

  public void Hide()
  {
    UI.Hide(job);
  }

  void Start()
  {
    root = GetComponent<UIDocument>().rootVisualElement;

    job = root.Q<VisualElement>("Job");

    pollution = job.Q<Label>("LocationPollution");

    held = job.Q<Label>("Held");

    skill0 = job.Q<Button>("Skill0");
    skill0.clicked += () => game.job.ActivateTool(Tool.Hand);

    skill1 = job.Q<Button>("Skill1");
    skill1.clicked += () => game.job.ActivateTool(Tool.Spike);

    skill2 = job.Q<Button>("Skill2");
    skill2.clicked += () => game.job.ActivateTool(Tool.Grabber);

    skill3 = job.Q<Button>("Skill3");
    skill3.clicked += () => game.job.ActivateTool(Tool.LeafBlower);

    skill4 = job.Q<Button>("Skill4");
    skill4.clicked += () => game.job.ActivateTool(Tool.Bulldozer);

    skill3Fuel = job.Q<VisualElement>("Skill3Fuel");

    skill4Fuel = job.Q<VisualElement>("Skill4Fuel");

    cooldowns = job.Query<VisualElement>(className: "skill-cooldown").ToList();

    Hide();

    StartCoroutine(UpdateAvailableTools());
  }

  void Update()
  {
    if (game.view == View.Job)
    {
      UpdateActiveTool();

      UpdateHeld();

      UpdateFuel();

      UpdatePollution();

      UpdateCooldown();
    }
  }

  IEnumerator UpdateAvailableTools()
  {
    skill0.EnableInClassList("inactive", !game.inventory.toolHand);
    skill1.EnableInClassList("inactive", !game.inventory.toolSpike);
    skill2.EnableInClassList("inactive", !game.inventory.toolGrabber);
    skill3.EnableInClassList("inactive", !game.inventory.toolLeafBlower || game.job.player.leafblowerFuel == 0);
    skill4.EnableInClassList("inactive", !game.inventory.toolBulldozer || game.job.player.leafblowerFuel == 0);
    yield return new WaitForSeconds(1f);
    StartCoroutine(UpdateAvailableTools());
  }

  void UpdateActiveTool()
  {
    if (Input.GetKeyDown(KeyCode.Alpha1))
    {
      game.job.ActivateTool(Tool.Hand);
    }
    else if (Input.GetKeyDown(KeyCode.Alpha2))
    {
      game.job.ActivateTool(Tool.Spike);
    }
    else if (Input.GetKeyDown(KeyCode.Alpha3))
    {
      game.job.ActivateTool(Tool.Grabber);
    }
    else if (Input.GetKeyDown(KeyCode.Alpha4))
    {
      game.job.ActivateTool(Tool.LeafBlower);
    }
    else if (Input.GetKeyDown(KeyCode.Alpha5))
    {
      game.job.ActivateTool(Tool.Bulldozer);
    }
  }

  void UpdateHeld()
  {
    held.text = game.job.player.heldText;
    held.style.left = Input.mousePosition.x;
    held.style.top = Screen.height - Input.mousePosition.y;
  }

  void UpdateFuel()
  {
    skill3Fuel.style.width = Length.Percent(game.job.player.leafblowerFuel);
    skill4Fuel.style.width = Length.Percent(game.job.player.bulldozerFuel);
  }

  void UpdateCooldown()
  {
    foreach (var cd in cooldowns)
    {
      cd.style.width = Length.Percent(game.job.cooldownRatio * 100);
    }
  }

  void UpdatePollution()
  {
    pollution.text = "Pollution: " + Mathf.RoundToInt(game.job.location.pollutionRatio * 100) + "%";
  }
}
