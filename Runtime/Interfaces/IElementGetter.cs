namespace d4160.GameFramework
{
    public interface IElementGetter<TObject>
    {
        TObject GetElementAt(int idx);

        TObject GetElementWith(int id);
    }
}
