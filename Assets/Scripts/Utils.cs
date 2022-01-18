public static class Utils
{
    public static int RewardMultiplier(JobDifficulty difficulty)
    {
        if (difficulty == JobDifficulty.Easy)
        {
            return 1;
        }
        else if (difficulty == JobDifficulty.Medium)
        {
            return 2;
        }
        else if (difficulty == JobDifficulty.Hard)
        {
            return 5;
        }

        return 1;
    }
}
