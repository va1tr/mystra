using UnityEngine;

using Voltorb;

namespace Mystra
{
    [RequireComponent(typeof(NavigationUtility))]
    internal class SubMenu : Menu
    {
        [SerializeField]
        private NavigationUtility m_NavigationUtility;

        public override System.Collections.IEnumerator Show()
        {
            yield return base.Show();

            m_NavigationUtility.OnShow();
        }

        public override System.Collections.IEnumerator Hide()
        {
            yield return base.Hide();
        }
    }

    internal abstract class SubMenu<T> : SubMenu
    {
        public abstract void SetProperties(T properties);
    }
}
