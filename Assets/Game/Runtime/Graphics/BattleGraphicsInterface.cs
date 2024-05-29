using System;
using System.Collections;

using UnityEngine;

using Eevee;
using Voltorb;

namespace Mystra
{
	internal sealed class BattleGraphicsInterface : GraphicalUserInterface
	{
		[SerializeField]
		private Typewriter m_Typewriter;

		[SerializeField]
		private EnemyPanel m_EnemyPanel;

		[SerializeField]
		private MovesMenu m_MovesMenu;

		[SerializeField]
		private AbilitiesMenu m_AbilitiesMenu;

		[SerializeField]
		private Healthbar m_PlayerHealthBar;

		[SerializeField]
		private Healthbar m_EnemyHealthBar;

		protected override void BindSceneGraphicReferences()
		{
			Add(m_EnemyPanel);

			Add(m_MovesMenu);
			Add(m_AbilitiesMenu);
		}

		public void Show(string name)
		{
			StartCoroutine(BattleGraphicsInterface.ShowAsync<Menu>(name));
		}

		public void Hide(string name)
		{
			StartCoroutine(BattleGraphicsInterface.HideAsync<Menu>(name));
		}

        internal void SetPlayerProperties(Combatant player)
		{
			player.image.sprite = player.psychic.asset.sprites[0];

            m_MovesMenu.SetProperties(player.psychic);
            m_AbilitiesMenu.SetProperties(player.psychic);

			m_PlayerHealthBar.SetProperties(player.psychic);
		}

		internal void SetEnemyProperties(Combatant enemy)
		{
			enemy.image.sprite = enemy.psychic.asset.sprites[0];

			m_EnemyPanel.SetProperties(enemy.psychic);
			m_EnemyHealthBar.SetProperties(enemy.psychic);
		}

		internal IEnumerator InterpolatePlayerHealthBar()
		{
			m_PlayerHealthBar.gameObject.SetActive(true);

			yield return m_PlayerHealthBar.SetHealthBarValue();

			m_PlayerHealthBar.gameObject.SetActive(false);
		}

        internal IEnumerator InterpolateEnemyHealthBar()
        {
            m_EnemyHealthBar.gameObject.SetActive(true);

            yield return m_EnemyHealthBar.SetHealthBarValue();

            m_EnemyHealthBar.gameObject.SetActive(false);
        }

        internal IEnumerator TypeTextCharByChar(string text)
		{
			yield return m_Typewriter.TypeTextCharByChar(text);
		}

		internal void CleanupTypewriterAndClearText()
		{
			m_Typewriter.CleanupAndClearAllText();
		}
    }
}
