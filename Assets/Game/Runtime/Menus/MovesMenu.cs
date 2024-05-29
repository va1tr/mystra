using UnityEngine;
using UnityEngine.UI;

using Eevee;
using Voltorb;
using System.Collections;

namespace Mystra
{
    internal sealed class MovesMenu : SubMenu<PsychicSpec>
	{
		[SerializeField]
		private AbilityButton m_AnalyseButton;

        private const int kAnalyseAbilityIndex = 0;

        public override void SetProperties(PsychicSpec properties)
        {
            m_AnalyseButton.BindProperties(properties.analyse);
        }
    }
}
