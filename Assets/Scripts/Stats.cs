using UnityEngine;

public class Stats
{
  public float maxStat = 100;
  public float energy = 100;
  public float hunger = 100;
  public float love = 100;
  public bool isHappy { get => (energy + hunger + love) / maxStat * 3 > 0.33; }

  public void AddEnergy(int value)
  {
    energy = Mathf.Min(maxStat, energy + value);
  }

  public void AddHunger(int value)
  {
    hunger = Mathf.Min(maxStat, hunger + value);
  }

  public void AddLove(int value)
  {
    love = Mathf.Min(maxStat, love + value);
  }

  public void Update(View view)
  {
    energy = Mathf.Max(0, energy - (Time.deltaTime));
    hunger = Mathf.Max(0, hunger - Time.deltaTime);
    love = Mathf.Max(0, love - Time.deltaTime);
  }
}