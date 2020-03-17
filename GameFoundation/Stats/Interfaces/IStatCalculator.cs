namespace d4160.GameFoundation
{
    public interface IStatCalculator
    {
        float CalculateStat(int difficultyLevel = 1);
    }

    public interface IMultipleStatCalculator
    {
        float[] CalculateStats(int difficultyLevel = 1, int additionalStats = 0);

        float CalculateStat(int index, int difficultyLevel = 1);
    }

    public interface IStatCalculator<T>
    {
        T CalculateStat(int difficultyLevel = 1);
    }
}