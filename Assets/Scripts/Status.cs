using UnityEngine;

public class Status
{
    public float energy = 25;

    public float hunger = 50;

    public float love = 75;

    public void AddEnergy(int value)
    {
        energy = Mathf.Min(100, energy + value);
    }

    public void AddHunger(int value)
    {
        hunger = Mathf.Min(100, hunger + value);
    }

    public void AddLove(int value)
    {
        love = Mathf.Min(100, love + value);
    }

    public void Update(View view)
    {
        energy = Mathf.Max(0, energy - Time.deltaTime);
        hunger = Mathf.Max(0, hunger - Time.deltaTime);
        love = Mathf.Max(0, love - Time.deltaTime);
    }
}