namespace d4160.GameFramework.Editor
{
	using GraphProcessor;

	public class GameFrameworkView : ToolbarView
	{
		public GameFrameworkView(BaseGraphView graphView) : base(graphView) {}

		protected override void AddButtons()
		{
			// Add the hello world button on the left of the toolbar
			//AddButton("Hello !", () => Debug.Log("Hello World"), left: false);

			// add the default buttons (center, show processor and show in project)
			base.AddButtons();
		}
	}
}