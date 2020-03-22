namespace d4160.GameFramework
{
    using System.Linq;
    using d4160.Core;
    using GraphProcessor;
    using Unity.Jobs;

    public class DefaultTalentGraphProcessor : BaseGraphProcessor
    {
        protected DefaultTalentNode[] _processArray;

        public DefaultTalentNode[] ProcessArray => _processArray;

        /// <summary>
        /// Manage graph scheduling and processing
        /// </summary>
        /// <param name="graph">Graph to be processed</param>
        public DefaultTalentGraphProcessor(TalentGraph graph) : base(graph)
        {
            graph.Processor = this;
        }

        public override void UpdateComputeOrder()
		{
			_processArray = graph.nodes.OfType<DefaultTalentNode>().OrderBy((x) => x.computeOrder).ToArray();

            JobHandle.ScheduleBatchedJobs();
        }

		public override void Run()
		{
            foreach (var node in ProcessArray)
            {
                if (node.Actived)
                {
                    node.OnProcess();
                }
                else
                {
                    node.Unprocess();
                }
            }
        }
    }
}