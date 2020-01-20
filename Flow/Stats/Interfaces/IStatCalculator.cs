namespace d4160.Systems.Flow
{
    public interface IStatCalculator
    {
        float CalculateStat(int difficultyLevel = 1);
    }

    public interface IStatCalculator<T>
    {
        T CalculateStat(int difficultyLevel = 1);
    }
}