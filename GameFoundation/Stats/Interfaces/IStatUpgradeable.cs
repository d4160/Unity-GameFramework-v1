namespace d4160.GameFoundation
{
    public interface IStatUpgradeable
    {
        void UpdateStat(float value);
    }

    public interface IMultipleStatUpgradeable
    {
        void UpdateStat(int index, float value);
    }
}

