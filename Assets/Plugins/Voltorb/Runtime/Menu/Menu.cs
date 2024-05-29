using UnityEngine;
using UnityEngine.UI;

namespace Voltorb
{
    public abstract class Menu : MonoBehaviour
    {
        public virtual System.Collections.IEnumerator Show()
        {
            gameObject.SetActive(true);

            yield break;
        }

        public virtual System.Collections.IEnumerator Hide()
        {
            gameObject.SetActive(false);

            yield break;
        }
    }

    public abstract class Menu<T> : Menu
    {
        public abstract void SetProperties(T properties);
    }
}
