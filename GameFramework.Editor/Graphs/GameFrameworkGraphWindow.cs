namespace d4160.GameFramework.Editor
{
    using GraphProcessor;
    using UnityEditor;
    using UnityEngine;

    public class GameFrameworkGraphWindow : BaseGraphWindow
    {

        [MenuItem("Window/Game Framework/Graph Window", false, -1)]
        public static BaseGraphWindow Open()
        {
            var graphWindow = GetWindow<GameFrameworkGraphWindow>();

            graphWindow.Show();

            return graphWindow;
        }

        protected override void InitializeWindow(BaseGraph graph)
        {
            titleContent = new GUIContent("Game Framework Graph");

            var graphView = new GameFrameworkGraphView(this);

            rootView.Add(graphView);

            graphView.Add(new GameFrameworkView(graphView));
        }
    }
}
