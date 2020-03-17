namespace d4160.GameFramework
{
    using System.Linq;
    using d4160.Core;
    using GraphProcessor;
    using Unity.Jobs;

    public class DefaultGameModeGraphProcessor : BaseGraphProcessor
    {
        protected GameModeBaseNode[] m_processArray;
        protected GameModeBaseNode m_current;

        public GameModeBaseNode[] ProcessArray => m_processArray;
        public GameModeBaseNode Current => m_current;

        /// <summary>
        /// Manage graph scheduling and processing
        /// </summary>
        /// <param name="graph">Graph to be processed</param>
        public DefaultGameModeGraphProcessor(GameModeGraph graph) : base(graph)
        {
            graph.Processor = this;
        }

        public override void UpdateComputeOrder()
		{
			m_processArray = graph.nodes.OfType<GameModeBaseNode>().OrderBy(n => n.computeOrder).ToArray();

            JobHandle.ScheduleBatchedJobs();
        }

		/// <summary>
		/// Process the current Node
		/// </summary>
		public override void Run()
        {
            m_current?.OnProcess();
        }

        public virtual void MoveTo(int index)
        {
            if (m_processArray.IsValidIndex(index))
                m_current =  m_processArray[index];
        }

        /// <summary>
        /// Check the next node and set as current
        /// </summary>
        /// <param name="GUID"></param>
        public virtual void MoveNext(string GUID = null)
        {
            if (m_current != null)
            {
                var nodes = m_current.NextNodes;
                var node = GraphUtility.GetEdgeNode(nodes, GUID);
                if (node != null) m_current = node;
            }

            JobHandle.ScheduleBatchedJobs();
        }

        /// <summary>
        /// Check the previous node and set as current
        /// </summary>
        /// <param name="GUID"></param>
        public virtual void MovePrevious(string GUID = null)
        {
            if (m_current != null)
            {
                var nodes = m_current.PreviousNodes;
                var node = GraphUtility.GetEdgeNode(nodes, GUID);
                if (node != null) m_current = node;
            }

            JobHandle.ScheduleBatchedJobs();
        }
    }
}