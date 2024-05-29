using UnityEditor;
using UnityEngine;

namespace Slowbro
{
    internal sealed class EmptyBehaviour : MonoBehaviour
    {
        internal static EmptyBehaviour instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = FindObjectOfType<EmptyBehaviour>();

                    if (s_Instance == null)
                    {
                        s_Instance = CreateHiddenGameObjectInstanceAndDontSave<EmptyBehaviour>(s_Instance.gameObject);
                    }
                }

                return s_Instance;
            }
        }

        private static EmptyBehaviour s_Instance;

        private static T CreateHiddenGameObjectInstanceAndDontSave<T>(GameObject original) where T : MonoBehaviour
        {
            return (GameObject.Instantiate(original).AddComponent<T>()).GetComponent<T>();
        }
    }
}
