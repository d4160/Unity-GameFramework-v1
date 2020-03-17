using d4160.Core.Attributes;

namespace d4160.GameFoundation
{
    [System.Serializable]
    public struct ItemStatPair
    {
        [Dropdown(ValuesProperty = "ItemNames")]
        public int item;
        [Dropdown(ValuesProperty = "StatNames")]
        public int stat;
    }
}