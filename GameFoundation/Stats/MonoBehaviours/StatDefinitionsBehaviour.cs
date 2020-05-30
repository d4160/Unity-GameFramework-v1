using d4160.Core.Attributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.GameFoundation;

public class StatDefinitionsBehaviour : MonoBehaviour
{
    [Dropdown(ValuesProperty = "StatNames")]
    [SerializeField]
    protected int[] _stats;

    public StatDefinition StatDefinition(int index) => StatManager.catalog.GetStatDefinitions()[_stats[index]];

    public string StatDefinitionId(int index) => StatDefinition(index).id;

    public int StatDefinitionHash(int index) => StatDefinition(index).idHash;

#if UNITY_EDITOR
    protected string[] StatNames =>
        StatManager.catalog.GetStatDefinitions().Select(x => x.displayName).ToArray();
#endif
}
