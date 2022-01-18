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
    public int pollution { get; private set; }
    public float pollutionRatio { get => (float)pollution / pollutionMax; }
    public int reward { get => pollution * Utils.RewardMultiplier(difficulty); }
    public bool isPolluted { get => pollutionRatio > 0.1f; }

    Game game;
    int tickRate = 5;

    void Awake()
    {
        game = FindObjectOfType<Game>();
    }

    void Start()
    {
        pollution = Random.Range(10, 20);

        StartCoroutine(PollutionTimer());
    }

    void Update()
    {
    }

    IEnumerator PollutionTimer()
    {
        yield return new WaitForSeconds(tickRate);

        pollution = Mathf.Min(pollutionMax, pollution + pollutionRate);

        StartCoroutine(PollutionTimer());
    }
}
