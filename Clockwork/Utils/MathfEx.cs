using System;

namespace Clockwork.Utils
{
	public static partial class Mathf
	{
		// Define constants with Decimal precision and cast down to double or float.

		public const float E = (float) 2.7182818284590452353602874714M; // 2.7182817f and 2.718281828459045
		public const float Sqrt2 = (float) 1.4142135623730950488016887242M; // 1.4142136f and 1.414213562373095

#if REAL_T_IS_DOUBLE
        public const float Epsilon = 1e-14; // Epsilon size should depend on the precision used.
#else
		public const float Epsilon = 1e-06f;
#endif

		public static int CeilToInt(float s)
		{
			return (int) Math.Ceiling(s);
		}

		public static int FloorToInt(float s)
		{
			return (int) Math.Floor(s);
		}

		public static int RoundToInt(float s)
		{
			return (int) Math.Round(s);
		}
	}
}