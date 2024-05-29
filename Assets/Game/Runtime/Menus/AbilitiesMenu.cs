using UnityEngine;

using Eevee;

namespace Mystra
{
	internal sealed class AbilitiesMenu : SubMenu<PsychicSpec>
	{
		[SerializeField]
		private AbilityButton[] m_Buttons;

        private const int kMaxNumberOfButtons = 4;

        public override void SetProperties(PsychicSpec properties)
        {
            var abilities = properties.GetAllAbilities();

            for (int i = 0; i < kMaxNumberOfButtons; i++)
            {
                if (abilities[i] != null)
                {
                    m_Buttons[i].BindProperties(abilities[i]);
                }
                else
                {
                    m_Buttons[i].BindPropertiesToNull();
                }
            }
        }
    }
}
