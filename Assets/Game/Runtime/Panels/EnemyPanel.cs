using System;

using UnityEngine;
using UnityEngine.UI;

using Eevee;
using Voltorb;

namespace Mystra
{
	internal sealed class EnemyPanel : Menu<PsychicSpec>
	{
		[Serializable]
		private sealed class Settings
		{
			[SerializeField]
			internal Text description;
		}

		[SerializeField]
		private Settings m_Settings = new Settings();

        public override void SetProperties(PsychicSpec properties)
        {
            string text = string.Empty;

            if (properties.trait.isUnlocked)
            {
                text += string.Concat($"-{properties.trait.name}\n{properties.trait.asset.description}");
            }
            else
            {
                text += string.Concat($"-???");
            }

            m_Settings.description.text = text;
        }
    }
}
