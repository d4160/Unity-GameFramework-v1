namespace d4160.GameFramework
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game Framework/Game/Chapter Graph", order = 0)]
    public class ChapterGraph : GameFrameworkBaseGraph
    {
    }
}

// TODO
// Can add more nodes for playables like CC
// CaseDescriptionNode, FlowchartNode, QuestionNode (questions to guide branches), CharacterStateNode, PatientExamNode (instrument, bodypart), ExamResultsNode (text or image), CharacterClothNode, CharacterExpressionNode, etc.