namespace d4160.GameFramework
{
    using GraphProcessor;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    [System.Serializable]
	public abstract class GameFrameworkBaseNode<T> : BaseNode where T : BaseNode
	{
		[Input(name = "In", allowMultiple = true)]
		public T input;

		[Output(name = "Out")]
		public T output;

        public virtual T[] NextNodes => outputPorts[0].GetEdges().Select(x => x.inputNode as T).ToArray();
        public virtual T[] PreviousNodes => inputPorts[0].GetEdges().Select(x => x.outputNode as T).ToArray();
        public virtual List<SerializableEdge> NextEdges => outputPorts[0].GetEdges();
        public virtual List<SerializableEdge> PreviousEdges => inputPorts[0].GetEdges();
        public virtual bool IsLast => NextEdges.Count == 0;
        public virtual bool IsFirst => PreviousEdges.Count == 0;

        // Called on Initialize(Graph) (when start the game is called foreach node)
        /*protected override void Enable()
        {
        }*/

        // Called when node is destroyed (in destructor)
        /*protected override void Disable()
        {
        }*/

        // Process called after inputs get, and before outputs set (When OnProcess is called, generally when Run() the Processor)
        // Generally used to set outputs for the connected nodes
        /*protected override void Process()
		{
        }*/
    }
}