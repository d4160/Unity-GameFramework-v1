namespace d4160.GameFramework.Editors
{
	using UnityEngine;
	using UnityEditor;
	using UnityEditor.UIElements;
	using UnityEditor.Experimental.GraphView;
	using UnityEngine.UIElements;
	using GraphProcessor;
    using d4160.Levels;
    using System.Linq;
    using System.Collections.Generic;
    using d4160.Core.Editors.Utilities;
  using d4160.Worlds;

  [NodeCustomEditor(typeof(ChapterNode))]
	public class ChapterNodeView : BaseNodeView
	{
		VisualElement _worldSceneField, _levelSceneField;

		public override void Enable()
		{
			var chapterNode = nodeTarget as ChapterNode;

			CreateChapterTypeField(chapterNode);

			CreateTitleField(chapterNode);

			CreateDescriptionField(chapterNode);

			var worldField = CreateWorldField(chapterNode);
			_worldSceneField = CreateWorldSceneField(chapterNode);
			controlsContainer.Add(worldField);
			controlsContainer.Add(_worldSceneField);

			var levelField = CreateLevelCategoryField(chapterNode);
			_levelSceneField = CreateLevelSceneField(chapterNode);
			controlsContainer.Add(levelField);
			controlsContainer.Add(_levelSceneField);

			CreateUnlockedField(chapterNode);

			CreateTutorialActivednField(chapterNode);
		}

		private void CreateChapterTypeField(ChapterNode chapterNode)
		{
			EnumField field = new EnumField("Chapter Type", ChapterType.Play)
			{
				value = chapterNode.ChapterType
			};

			field.RegisterValueChangedCallback((v) => {
				owner.RegisterCompleteObjectUndo("Updated chapterNode type");
				chapterNode.ChapterType = (ChapterType)v.newValue;
			});

			controlsContainer.Add(field);
		}

		private void CreateTitleField(ChapterNode chapterNode)
		{
			TextField field = new TextField("Title")
			{
				multiline = false,
				value = chapterNode.Title,
			};

			field.RegisterValueChangedCallback((v) => {
				owner.RegisterCompleteObjectUndo("Updated chapterNode title");
				chapterNode.Title = v.newValue;
			});

			controlsContainer.Add(field);
		}

		private void CreateDescriptionField(ChapterNode chapterNode)
		{
			TextField field = new TextField("Description")
			{
				multiline = true,
				value = chapterNode.Description,
			};

			field.RegisterValueChangedCallback((v) => {
				owner.RegisterCompleteObjectUndo("Updated chapterNode description");
				chapterNode.Description = v.newValue;
			});

			controlsContainer.Add(field);
		}

		private VisualElement CreateWorldField(ChapterNode chapterNode)
		{
			var worldNames = GameFrameworkSettings.GameDatabase.GetGameData<DefaultWorldsSO>(2).ArchetypeNames;
			worldNames = EditorUtilities.GetNoneSelectableFrom(worldNames);

			PopupField<string> field = new PopupField<string>(
				"World", worldNames.ToList(), 0)
			{
				index = chapterNode.WorldScene.world + 1
			};

			field.RegisterValueChangedCallback((v) => {
				owner.RegisterCompleteObjectUndo("Updated chapterNode world");
				var val = chapterNode.WorldScene;
				val.world = field.index - 1;
				chapterNode.WorldScene = val;

				var veIdx = controlsContainer.IndexOf(_worldSceneField);
				controlsContainer.RemoveAt(veIdx);

				_worldSceneField = CreateWorldSceneField(chapterNode);
				controlsContainer.Insert(veIdx, _worldSceneField);
			});

			return field;
		}

		private VisualElement CreateWorldSceneField(ChapterNode chapterNode)
		{
			var worldScenes = GameFrameworkSettings.GameDatabase.GetGameData<DefaultWorldsSO>(2).GetSceneNames(chapterNode.WorldScene.world).ToList();
			if (worldScenes.Count == 0) worldScenes = new List<string>(){ "" };

			PopupField<string> field = new PopupField<string>(
				"World Scene", worldScenes, 0)
			{
				index = chapterNode.WorldScene.worldScene
			};

			field.RegisterValueChangedCallback((v) => {
				owner.RegisterCompleteObjectUndo("Updated chapterNode worldScene");
				var val = chapterNode.WorldScene;
				val.worldScene = field.index;
				chapterNode.WorldScene = val;
			});

			return field;
		}

		private VisualElement CreateLevelCategoryField(ChapterNode chapterNode)
		{
			var levelNames = GameFrameworkSettings.GameDatabase.GetGameData<DefaultLevelCategoriesSO>(3).ArchetypeNames;
			levelNames = EditorUtilities.GetNoneSelectableFrom(levelNames);

			PopupField<string> field = new PopupField<string>(
				"Level Category", levelNames.ToList(), 0)
			{
				index = chapterNode.LevelScene.levelCategory + 1
			};

			field.RegisterValueChangedCallback((v) => {
				owner.RegisterCompleteObjectUndo("Updated chapterNode levelCategory");
				var val = chapterNode.LevelScene;
				val.levelCategory = field.index - 1;
				chapterNode.LevelScene = val;

				var veIdx = controlsContainer.IndexOf(_levelSceneField);
				controlsContainer.RemoveAt(veIdx);

				_levelSceneField = CreateLevelSceneField(chapterNode);
				controlsContainer.Insert(veIdx, _levelSceneField);
			});

			return field;
		}

		private VisualElement CreateLevelSceneField(ChapterNode chapterNode)
		{
			var levelScenes = GameFrameworkSettings.GameDatabase.GetGameData<DefaultLevelCategoriesSO>(3).GetSceneNames(chapterNode.LevelScene.levelCategory).ToList();
			if (levelScenes.Count == 0) levelScenes = new List<string>(){ "" };

			PopupField<string> field = new PopupField<string>(
				"Level Scene", levelScenes, 0)
			{
				index = chapterNode.LevelScene.levelScene
			};

			field.RegisterValueChangedCallback((v) => {
				owner.RegisterCompleteObjectUndo("Updated chapterNode levelScene");
				var val = chapterNode.LevelScene;
				val.levelScene = field.index;
				chapterNode.LevelScene = val;
			});

			return field;
		}

		private void CreateUnlockedField(ChapterNode chapterNode)
		{
			Toggle field = new Toggle("Unlocked?")
			{
				value = chapterNode.Unlocked,
			};

			field.RegisterValueChangedCallback((v) => {
				owner.RegisterCompleteObjectUndo("Updated chapterNode unlocked");
				chapterNode.Unlocked = v.newValue;
			});

			controlsContainer.Add(field);
		}

		private void CreateTutorialActivednField(ChapterNode chapterNode)
		{
			Toggle field = new Toggle("Tutorial Actived?")
			{
				value = chapterNode.TutorialActived,
			};

			field.RegisterValueChangedCallback((v) => {
				owner.RegisterCompleteObjectUndo("Updated chapterNode tutorialActived");
				chapterNode.TutorialActived = v.newValue;
			});

			controlsContainer.Add(field);
		}
	}
}