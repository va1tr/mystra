using System.Collections;
using UnityEngine;

using Voltorb;

namespace Mystra
{
	internal sealed class MapMenu : SubMenu
	{
		[SerializeField]
		private Typewriter m_Typewriter;

        public override IEnumerator Show()
        {
            m_Typewriter.PrintCompletedText("overworld mode");

            yield return base.Show();
        }
    }
}
