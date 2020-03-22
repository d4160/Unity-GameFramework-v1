using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace d4160.GameFramework
{
    [CreateAssetMenu(menuName = "Game Framework/Game Data/Default Talent Definition")]
    public class DefaultTalentDefinition : ScriptableObject
    {
        [SerializeField] protected int _type; // Can be an enum
        [SerializeField] protected string _name;
        [TextArea]
        [SerializeField] protected string _description;

        public int Type => _type;
        public string Name => _name;
        public string Description => _description;
    }
}