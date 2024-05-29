using UnityEngine;

using Voltorb;

namespace Mystra
{
	internal sealed class GameGraphicsInterface : GraphicalUserInterface
	{
        [SerializeField]
        private TitleMenu m_TitleMenu;

        [SerializeField]
        private SubMenu m_MapSubMenu;

		[SerializeField]
		private SubMenu m_ConfirmSubMenu;

        protected override void BindSceneGraphicReferences()
        {
            Add(m_TitleMenu);

            Add(m_MapSubMenu, "map");
            Add(m_ConfirmSubMenu, "confirm");
        }

        public void Show(string name)
        {
            StartCoroutine(GameGraphicsInterface.ShowAsync<Menu>(name));
        }

        public void Hide(string name)
        {
            StartCoroutine(GameGraphicsInterface.HideAsync<Menu>(name));
        }
    }
}
