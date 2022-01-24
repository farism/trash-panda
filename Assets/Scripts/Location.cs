using System.Collections;
using UnityEngine;

public class Location : MonoBehaviour
{
  public JobType type;
  public JobTimezone timezone;
  public JobDifficulty difficulty;
  public int pollutionMax = 100;
  public int pollutionRate = 5;
  public int index { get; set; }
  public int selected { get; set; }
  public int pollution { get; private set; } = 1;
  public float pollutionRatio { get => (float)pollution / pollutionMax; }
  public int reward { get => pollution * Utils.RewardMultiplier(difficulty); }
  public bool isPolluted { get => pollutionRatio > 0.1f; }

  Game game;
  int tickRate = 5;
  int prevPollution = 0;

  public void RemovePollution(int amount)
  {
    pollution = Mathf.Max(0, pollution - amount);
    prevPollution = pollution;
  }

  void Awake()
  {
    pollution = Mathf.CeilToInt(pollutionMax / 100);
    pollutionRate = Mathf.CeilToInt(pollutionMax / 100);
    game = FindObjectOfType<Game>();
  }

  void Start()
  {
    StartCoroutine(PollutionTimer());
  }

  void Update()
  {
    if (pollution < prevPollution && pollutionRatio < 0.1)
    {
      game.jobs.Remove(this);
    }

    prevPollution = pollution;
  }

  IEnumerator PollutionTimer()
  {
    yield return new WaitForSeconds(tickRate);

    if (game.job.location != this)
    {
      pollution = Mathf.Min(pollutionMax, pollution + PollutionRateForView());
    }

    StartCoroutine(PollutionTimer());
  }

  int PollutionRateForView()
  {
    if (game.view == View.Job)
    {
      return Mathf.CeilToInt(pollutionRate / 2); // reduce pollution rate while actively cleaning it up
    }
    else
    {
      return Mathf.CeilToInt(pollutionRate);
    }
  }
}
