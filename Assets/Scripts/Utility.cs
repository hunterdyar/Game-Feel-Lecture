using System.Collections.Generic;

namespace Peggle
{
	public static class Utility
	{
		public static void Shuffle<T>(this IList<T> list)
		{
			//fisher-yates
			for (int i = 0; i < list.Count; i++)
			{
				int r = UnityEngine.Random.Range(i, list.Count);
				(list[i], list[r]) = (list[r], list[i]);
			}
		}
	}
}