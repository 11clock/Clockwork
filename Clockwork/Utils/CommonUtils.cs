using System.Collections.Generic;

namespace Clockwork.Utils
{
	public static class CommonUtils
	{
		public static IEnumerable<int> Sequence(int start, int end)
		{
			switch (Mathf.Sign(end - start))
			{
				case -1:
					while (start >= end)
					{
						yield return start--;
					}

					break;

				case 1:
					while (start <= end)
					{
						yield return start++;
					}

					break;

				default:
					yield break;
			}
		}
	}
}