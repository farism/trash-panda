using UnityEngine;

public class Status
{
    public float energy { get; private set; } = 25;

    public float hunger { get; private set; } = 50;

    public float love { get; private set; } = 75;

    public void Update(View view)
    {
        energy = Mathf.Max(0, energy - Time.deltaTime / 5);
        hunger = Mathf.Max(0, hunger - Time.deltaTime / 5);
        love = Mathf.Max(0, love - Time.deltaTime / 5);
    }
}