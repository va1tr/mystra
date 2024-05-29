using System;

using UnityEngine;

using Eevee;

namespace Mystra
{
    [CreateAssetMenu(fileName = "new-dialogue", menuName = "ScriptableObjects/Mystra/Text/Conversation", order = 150)]
    internal sealed class ScriptableText : ScriptableObject
	{
        [SerializeField]
        private Conversation[] m_Conversations;

        internal Conversation[] conversations
        {
            get => m_Conversations;
        }
	}

    [Serializable]
    internal struct Conversation
    {
        [SerializeField]
        internal string name;

        [SerializeField, TextArea]
        internal string text;
    }
}
