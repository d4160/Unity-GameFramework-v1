namespace d4160.GameFramework
{
    using UnityEngine;

    public class DefaultArchetypesAttribute : PropertyAttribute
    {
        public readonly string[] kValues;

        public bool OnlyOnEmptyLists { get; set; } = true;

        public DefaultArchetypesAttribute(params string[] values)
        {
            kValues = values;
        }
    }
}
