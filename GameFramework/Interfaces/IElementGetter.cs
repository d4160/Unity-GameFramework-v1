namespace d4160.GameFramework
{
    public interface IElementGetter<T>
    {
        T GetElementAt(int idx);

        T GetElementWith(int id);

        int ElementsCount { get; }

        void Add(T element);

        void Remove(T element);

        void Clear();

        T[] GetAll();

        void SetAll(T[] elements);
    }
}
