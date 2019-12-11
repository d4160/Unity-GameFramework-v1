namespace d4160.GameFramework
{
    using Malee;
    using d4160.Systems.DataPersistence;

    //[CreateAssetMenu(fileName = "New PlayTrials_SO.asset", menuName = "Game Framework/Game Data/Leaderboards")]
    public abstract class DefaultPlayTrialsSO<T1, T2, T3> : ReorderableSO<T1, T2, T3>
        where T1 : ReorderableArray<T2>
        where T2 : new()
        where T3 : BaseSerializableData
    {

    }
}