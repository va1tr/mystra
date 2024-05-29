using System;

using UnityEngine;

namespace Mystra
{
    [Serializable]
	internal sealed class GameStateObject
	{
		[SerializeField]
		internal GameObject context;

		[SerializeField]
		internal GameObject navigation;
	}
}
