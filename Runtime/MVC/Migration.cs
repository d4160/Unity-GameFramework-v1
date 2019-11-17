using UnityEngine;

namespace d4160.MVC
{
    public abstract class Migration : ScriptableObject
    {
        public abstract void AutoFill();

        public abstract void Migrate();

        public abstract void Drop();
    }
}