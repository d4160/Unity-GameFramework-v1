namespace d4160.GameFramework
{
    using System.Linq;
    using d4160.Core;
    using GraphProcessor;
    using Unity.Jobs;

    public class GameModeGraphProcessor : BaseGraphProcessor
    {
        protected GameModeBaseNode[] m_processArray;
        protected GameModeBaseNode m_current;

        public GameModeBaseNode[] ProcessArray => m_processArray;
        public GameModeBaseNode Current => m_current;

        /// <summary>
		/// Manage graph scheduling and processing
		/// </summary>
		/// <param name="graph">Graph to be processed</param>
		public GameModeGraphProcessor(GameModeGraph graph) : base(graph) {}

        public override void UpdateComputeOrder()
		{
			m_processArray = graph.nodes.Where(n => n is GameModeBaseNode).OrderBy(n => n.computeOrder).Select(n => n as GameModeBaseNode).ToArray();

            JobHandle.ScheduleBatchedJobs();
        }

		/// <summary>
		/// Process the current Node
		/// </summary>
		public override void Run()
		{
            if (m_current != null)
                m_current.OnProcess();
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
                var node = GetEdgeNode(nodes, GUID);
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
                var node = GetEdgeNode(nodes, GUID);
                if (node != null) m_current = node;
            }

            JobHandle.ScheduleBatchedJobs();
        }

        protected static GameModeBaseNode GetEdgeNode(GameModeBaseNode[] nodes, string GUID)
        {
            GameModeBaseNode node = null;

            switch (nodes.Length)
            {
                case 0:
                    break;
                case 1:
                    node = nodes[0];
                    break;
                default:
                    if (!string.IsNullOrEmpty(GUID))
                    {
                        for (int i = 0; i < nodes.Length; i++)
                        {
                            if (nodes[i].GUID == GUID)
                            {
                                node = nodes[i];
                                break;
                            }
                        }
                    }
                    else
                    {
                        var rd = UnityEngine.Random.Range(0, nodes.Length);
                        node = nodes[rd];
                    }
                    break;
            }

            return node;
        }
    }
}