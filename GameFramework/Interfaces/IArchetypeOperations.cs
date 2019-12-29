namespace d4160.GameFramework
{
    public interface IArchetypeOperations
    {
        int Count { get; }

        void AddRange(string[] names);

        void Clear();
    }
}
