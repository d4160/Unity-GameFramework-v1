using System;

namespace d4160.MVC
{
    public abstract class Model
    {
        public abstract int Id { get; }

        protected abstract string[] Values { get; }

        public abstract void SaveChanges(Action onFinish = null);

        public abstract void Delete();

        public static string GetPrimaryKeysString(string[] primaryKeys)
        {
            string value = string.Empty;
            for (int i = 0; i < primaryKeys.Length; i++)
            {
                if (i != primaryKeys.Length - 1)
                    value += ($"{primaryKeys[i]},");
                else
                    value += ($"{primaryKeys[i]}");
            }

            return value;
        }
    }
}