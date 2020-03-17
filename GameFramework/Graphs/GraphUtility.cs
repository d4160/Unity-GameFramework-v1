using GraphProcessor;

namespace d4160.GameFramework
{
    using UnityEngine;

    public static class GraphUtility
    {
        public static T GetEdgeNode<T>(T[] nodes, string GUID) where T : BaseNode
        {
            T node = null;

            switch (nodes.Length)
            {
                case 0:
                    break;
                case 1:
                    node = nodes[0];
                    break;
                default:
                    if (!string.IsNullOrEmpty(GUID))
                    {
                        for (int i = 0; i < nodes.Length; i++)
                        {
                            if (nodes[i].GUID == GUID)
                            {
                                node = nodes[i];
                                break;
                            }
                        }
                    }
                    else
                    {
                        var rd = UnityEngine.Random.Range(0, nodes.Length);
                        node = nodes[rd];
                    }
                    break;
            }

            return node;
        }
    }
}