using System.Collections.Generic;

namespace Clockwork.Utils.Extensions
{
	public static class EnumerableExtensions
	{
		public static MatrixVector CoordinatesOf<T>(this T[,] matrix, T value)
		{
			int rows = matrix.GetLength(0);
			int cols = matrix.GetLength(1);

			for (int row = 0; row < rows; row++)
			{
				for (int col = 0; col < cols; col++)
				{
					if (matrix[row, col].Equals(value))
						return new MatrixVector(row, col);
				}
			}
		
			return new MatrixVector(-1, -1);
		}

		public static int GetRows<T>(this T[,] matrix)
		{
			return matrix.GetLength(0);
		}
		
		public static int GetCols<T>(this T[,] matrix)
		{
			return matrix.GetLength(1);
		}

		public static T GetValue<T>(this T[,] matrix, MatrixVector coords)
		{
			return matrix[coords.Row, coords.Col];
		}
		
		public static void SetValue<T>(this T[,] matrix, T value, MatrixVector coords)
		{
			matrix[coords.Row, coords.Col] = value;
		}

		public static string ToJoinedString<T>(this IEnumerable<T> enumerable)
		{
			return $"[{string.Join(", ", enumerable)}]";
		}
	}
}