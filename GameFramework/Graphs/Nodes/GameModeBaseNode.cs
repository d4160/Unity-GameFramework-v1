namespace d4160.GameFramework
{
    using GraphProcessor;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    [System.Serializable]
	public abstract class GameModeBaseNode : BaseNode
	{
		[Input(name = "In", allowMultiple = true)]
		public GameModeBaseNode input;

		[Output(name = "Out")]
		public GameModeBaseNode output;

        public GameModeBaseNode[] NextNodes => outputPorts[0].GetEdges().Select(x => x.inputNode as GameModeBaseNode).ToArray();
        public GameModeBaseNode[] PreviousNodes => inputPorts[0].GetEdges().Select(x => x.outputNode as GameModeBaseNode).ToArray();
        public List<SerializableEdge> NextEdges => outputPorts[0].GetEdges();
        public List<SerializableEdge> PreviousEdges => inputPorts[0].GetEdges();
        public bool IsLast => NextEdges.Count == 0;
        public bool IsFirst => PreviousEdges.Count == 0;

        // Called on Initialize(Graph) (when start the game is called foreach node)
        /*protected override void Enable()
        {
        }*/

        // Called when node is destroyed (in destructor)
        /*protected override void Disable()
        {
        }*/

        // Process called after inputs get, and before outputs set
        // Generally used to set outputs for the connected nodes
        /*protected override void Process()
		{
        }*/
    }
}