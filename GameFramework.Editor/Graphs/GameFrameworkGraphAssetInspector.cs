namespace d4160.GameFramework.Editor
{
	using UnityEngine;
	using UnityEditor;
	using GraphProcessor;
	using UnityEngine.UIElements;

	[CustomEditor(typeof(GameFrameworkBaseGraph), true)]
	public class GameFrameworkGraphAssetInspector : GraphInspector
	{
		// protected override void CreateInspector()
		// {
		// }

		protected override void CreateInspector()
		{
			base.CreateInspector();

			root.Add(new Button(() => GameFrameworkGraphWindow.Open().InitializeGraph(target as BaseGraph))
			{
				text = "Open Game Framework graph window"
			});
		}
	}
}