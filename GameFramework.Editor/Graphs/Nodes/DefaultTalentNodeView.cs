using System.Collections;
using System.Collections.Generic;
using d4160.Core.Editors.Utilities;
using d4160.GameFramework;
using GraphProcessor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[NodeCustomEditor(typeof(DefaultTalentNode))]   
public class DefaultTalentNodeView : BaseNodeView
{
    public override void Enable()
    {
        DefaultTalentNode node = (nodeTarget as DefaultTalentNode);

        ObjectField definition = UIElementsUtility.ObjectField(node.Definition, "Definition",(newValue) =>
        {
            owner.RegisterCompleteObjectUndo("Updated PrefabOverrideTalentNode Definition");
            node.Definition = newValue;
        });

        Toggle activedField = UIElementsUtility.Toggle(node.Actived, "Actived?", (newValue) =>
        {
            owner.RegisterCompleteObjectUndo("Updated PrefabOverrideTalentNode Actived");
            node.Actived = newValue;
        });

        contentContainer.Add(definition);
        contentContainer.Add(activedField);
    }
}