using UnityEngine;
using UnityEngine.UI;

using Voltorb;

namespace Mystra
{
    internal sealed class CaseFileMenu : Menu<ScriptableText>
    {
        [SerializeField]
        private Typewriter m_Typewriter;

        public override void SetProperties(ScriptableText properties)
        {
            m_Typewriter.CleanupAndClearAllText();
            m_Typewriter.PrintTextCharByChar(properties.conversations[0].text);
        }
    }
}
