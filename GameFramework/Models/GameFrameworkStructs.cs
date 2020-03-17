using UnityEngine;

namespace d4160.GameFramework
{
    [System.Serializable]
    public struct LevelReference
    {
        public LevelType levelType;
        public int level;
    }

    public struct CategoryAndScene
    {
        public string scenePath;
        public string category;
    }

    [System.Serializable]
    public struct AnimatorString
    {
        public string paramName;

        private int _paramHash;

        public int HashOrId
        {
            get
            {
                if (_paramHash == default)
                    _paramHash = Animator.StringToHash(paramName);

                return _paramHash;
            }
        }
    }
}
