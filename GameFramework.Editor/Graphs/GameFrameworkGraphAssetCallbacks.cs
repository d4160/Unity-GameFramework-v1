namespace d4160.GameFramework.Editor
{
	using UnityEngine;
	using UnityEditor;
	using GraphProcessor;
	using UnityEditor.Callbacks;

	public class GameFrameworkGraphAssetCallbacks
	{
		/*[MenuItem("Assets/Create/GraphProcessor", false, 10)]
		public static void CreateGraphPorcessor()
		{
			var		obj = Selection.activeObject;
			string	path;

			if (obj == null)
				path = "Assets";
			else
				path = AssetDatabase.GetAssetPath(obj.GetInstanceID());

			var graph = ScriptableObject.CreateInstance< BaseGraph >();

			ProjectWindowUtil.CreateAsset(graph, path + "/GraphProcessor.asset");
		}*/

		[OnOpenAsset(0)]
		public static bool OnBaseGraphOpened(int instanceID, int line)
		{
			var asset = EditorUtility.InstanceIDToObject(instanceID);

			if (asset is GameFrameworkBaseGraph) // && AssetDatabase.GetAssetPath(asset).Contains("Examples")
			{
				GameFrameworkGraphWindow.Open().InitializeGraph(asset as GameFrameworkBaseGraph);
				return true;
			}
			return false;
		}
	}
}