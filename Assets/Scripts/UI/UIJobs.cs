using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIJobs : MonoBehaviour, IScreen
{
    public Game game;
    VisualElement root;
    VisualElement jobs;
    VisualElement locations;
    List<Button> locationBtns = new List<Button>();
    VisualElement locationInfo;
    Label locationInfoType;
    Label locationInfoTimezone;
    Label locationInfoDifficulty;
    Label locationInfoPollutionRate;
    Label locationInfoPollution;
    Label locationInfoReward;
    Button locationInfoStart;

    int activeJobIndex = -1;

    public void Show()
    {
        UI.Show(jobs);
    }

    public void Hide()
    {
        activeJobIndex = -1;
        UI.Hide(locationInfo);
        UI.Hide(jobs);
    }

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        jobs = root.Q<VisualElement>("Jobs");
        locations = root.Q<VisualElement>("Locations");
        locationInfoType = root.Q<Label>("LocationInfoType");
        locationInfoTimezone = root.Q<Label>("LocationInfoTimezone");
        locationInfoDifficulty = root.Q<Label>("LocationInfoDifficulty");
        locationInfoPollutionRate = root.Q<Label>("LocationInfoPollutionRate");
        locationInfoPollution = root.Q<Label>("LocationInfoPollution");
        locationInfoReward = root.Q<Label>("LocationInfoReward");
        locationInfoStart = root.Q<Button>("LocationInfoStart");
        locationInfoStart.clicked += () => game.StartJob(activeJobIndex);
        locationInfo = root.Q<VisualElement>("LocationInfo");
        for (var i = 0; i < game.jobs.locations.Length; i++)
        {
            CreateLocation(i);
        }

        UI.Hide(locationInfo);

        Hide();
    }

    void Update()
    {
        if (game.view == View.Jobs)
        {
            UpdateLocationInfo();

            UpdateLocationPositions();

            UpdateLocationPollution();
        }
    }

    void UpdateLocationPositions()
    {
        for (var i = 0; i < game.jobs.locations.Length; i++)
        {
            var loc = game.jobs.locations[i];
            var btn = locationBtns[i];

            if (game.jobs.IsActive(loc))
            {
                var screenPos = Camera.main.WorldToScreenPoint(loc.transform.position);
                btn.style.left = screenPos.x - btn.resolvedStyle.width / 2;
                btn.style.top = Screen.height - screenPos.y - btn.resolvedStyle.height / 2;
                btn.style.opacity = loc.transform.position.z < 0.1f ? 1 : 0;
                btn.style.display = DisplayStyle.Flex;

                if (btn.style.opacity == 1)
                {
                    btn.BringToFront();
                }
                else
                {
                    btn.SendToBack();
                }
            }
            else
            {
                btn.style.display = DisplayStyle.None;
            }
        }
    }


    void UpdateLocationPollution()
    {
        for (var i = 0; i < game.jobs.locations.Length; i++)
        {
            var loc = game.jobs.locations[i];
            var btn = locationBtns[i];

            if (game.jobs.IsActive(loc))
            {
                var color = new StyleColor(Color.Lerp(btn.style.color.value, Color.red, loc.pollutionRatio));
                var width = 3 + 7 * loc.pollutionRatio;
                btn.style.borderTopColor = btn.style.borderBottomColor = btn.style.borderLeftColor = btn.style.borderRightColor = color;
                btn.style.borderTopWidth = btn.style.borderBottomWidth = btn.style.borderLeftWidth = btn.style.borderRightWidth = width;
            }
        }
    }

    void UpdateLocationInfo()
    {
        if (Input.GetMouseButtonDown(1))
        {
            activeJobIndex = -1;
            UI.Hide(locationInfo);
        }

        if (activeJobIndex >= 0)
        {
            var loc = game.jobs.locations[activeJobIndex];
            var btn = locationBtns[activeJobIndex];

            if (loc != null && btn != null)
            {
                locationInfoType.text = "Type: " + loc.type.ToString();
                locationInfoTimezone.text = "Timezone: " + loc.timezone.ToString().Replace("Minus", "-").Replace("Plus", "+");
                locationInfoDifficulty.text = "Difficulty: " + loc.difficulty.ToString();
                locationInfoPollutionRate.text = "Pollution Rate: " + loc.pollutionRate;
                locationInfoPollution.text = "Pollution: " + loc.pollution + "kg / " + loc.pollutionMax + "kg";
                locationInfoReward.text = "Reward: " + loc.reward + " shiny things";

                locationInfo.style.top = btn.resolvedStyle.top;
                locationInfo.style.left = btn.resolvedStyle.left - locationInfo.resolvedStyle.width / 2 + btn.resolvedStyle.width / 2;
                locationInfo.style.opacity = btn.style.opacity;
                locationInfo.style.display = locationInfo.resolvedStyle.opacity > 0 ? DisplayStyle.Flex : DisplayStyle.None;
            }
        }
    }

    void CreateLocation(int index)
    {
        var btn = new Button();
        btn.AddToClassList("location");
        btn.AddToClassList(game.jobs.locations[index].type.ToString());
        btn.style.color = Color.white;//btn.style.borderTopColor; // assign border-color to color for lerping later

        var pollution = new VisualElement();
        pollution.AddToClassList("pollution");
        btn.Add(pollution);

        btn.clicked += () =>
        {
            if (btn.resolvedStyle.opacity > 0)
            {
                OnClickLocation(index);
            }
        };

        locations.Add(btn);
        locationBtns.Add(btn);
    }

    void OnClickLocation(int index)
    {
        activeJobIndex = index;

        var type = game.jobs.locations[index].type.ToString();

        locationInfo.Query<VisualElement>(className: "location-info-image").ForEach((ve) =>
        {
            ve.RemoveFromClassList("City");
            ve.RemoveFromClassList("Desert");
            ve.RemoveFromClassList("Meadow");
            ve.AddToClassList(type);
        });
    }
}
