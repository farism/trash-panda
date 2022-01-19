using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIJob : MonoBehaviour, IScreen
{
    public Game game;
    VisualElement root;
    VisualElement job;
    List<VisualElement> cooldowns;
    Button skill0;
    Button skill1;
    Button skill2;
    Button skill3;
    Button skill4;

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

        cooldowns = job.Query<VisualElement>(className: "skill-cooldown").ToList();

        Hide();
    }

    void Update()
    {
        if (game.view == View.Job)
        {
            UpdateActiveTool();

            UpdateCooldown();
        }
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

    void UpdateCooldown()
    {
        foreach (var cd in cooldowns)
        {
            cd.style.width = Length.Percent(game.job.cooldownRatio * 100);
        }
    }
}
