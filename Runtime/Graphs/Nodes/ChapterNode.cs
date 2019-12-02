namespace d4160.GameFramework
{
	using UnityEngine;
	using GraphProcessor;

	[System.Serializable, NodeMenuItem("Game Mode/Chapter")]
	public class ChapterNode : GameModeBaseNode
	{
		[SerializeField] protected ChapterType m_type;
		[SerializeField] protected string m_title;
		[SerializeField] protected string m_description;
		[SerializeField] protected WorldScene m_worldScene;
		[SerializeField] protected LevelScene m_playOrCinematicScene;
		[SerializeField] protected bool m_tutorialActived;
		[SerializeField] protected bool m_unlocked;
		[SerializeField] protected ChapterGraph m_chapterGraph;

		public ChapterType ChapterType { get => m_type; set => m_type = value; }
		public string Title { get => m_title; set => m_title = value; }
		public string Description { get => m_description; set => m_description = value; }
		public WorldScene WorldScene { get => m_worldScene; set => m_worldScene = value; }
		public LevelScene LevelScene { get => m_playOrCinematicScene; set => m_playOrCinematicScene = value; }
		public bool TutorialActived { get => m_tutorialActived; set => m_tutorialActived = value; }
		public bool Unlocked { get => m_unlocked; set => m_unlocked = value; }
		public ChapterGraph ChapterGraph { get => m_chapterGraph; set => m_chapterGraph = value; }

		public override string name => $"Chapter \"{m_title}\" ({m_type})";
	}
}