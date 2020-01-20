namespace d4160.Systems.Flow
{
    using UnityEngine;

    public abstract class StatCalculatorDefinitionBase : ScriptableObject, IStatCalculator
    {
        public abstract float CalculateStat(int difficultyLevel = 1);
    }

    public abstract class StatCalculatorDefinitionBase<T> : ScriptableObject, IStatCalculator<T>
    {
        public abstract T CalculateStat(int difficultyLevel = 1);
    }
}