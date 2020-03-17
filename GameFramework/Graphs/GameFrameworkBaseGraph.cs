namespace d4160.GameFramework
{
    using GraphProcessor;

    public class GameFrameworkBaseGraph : BaseGraph
    {
        protected BaseGraphProcessor _processor;

        public BaseGraphProcessor Processor
        {
            get => _processor;
            set => _processor = value;
        }

        public T GetProcessor<T>() where T : BaseGraphProcessor => _processor as T;
    }
}