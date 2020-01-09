namespace d4160.GameFramework.Editor
{
	using UnityEditor;
	using GraphProcessor;

	public class GameFrameworkGraphProcessorMenuItems : NodeGraphProcessorMenuItems
	{
		[MenuItem("Assets/Create/GameProcessorGraph Script/Node C# Script", false, MenuItemPosition.afterCreateScript)]
		private static void CreateNodeCSharpScritpt() => CreateDefaultNodeCSharpScritpt();

		[MenuItem("Assets/Create/GameProcessorGraph Script/Node View C# Script", false, MenuItemPosition.afterCreateScript + 1)]
		private static void CreateNodeViewCSharpScritpt() => CreateDefaultNodeViewCSharpScritpt();

		// To add your C# script creation with you own templates, use ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, defaultFileName)
	}
}