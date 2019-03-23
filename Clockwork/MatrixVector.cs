using System;

namespace Clockwork
{
	public struct MatrixVector : IEquatable<MatrixVector>
	{
		public int Row;
		public int Col;

		public MatrixVector(int row, int col)
		{
			Row = row;
			Col = col;
		}

		public double Magnitude => Math.Sqrt((Row * Row) + (Col * Col));

		public static MatrixVector operator +(MatrixVector a, MatrixVector b)
		{
			return new MatrixVector(a.Row + b.Row, a.Col + b.Col);
		}

		public static MatrixVector operator -(MatrixVector a, MatrixVector b)
		{
			return new MatrixVector(a.Row - b.Row, a.Col - b.Col);
		}

		public static bool operator ==(MatrixVector a, MatrixVector b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(MatrixVector a, MatrixVector b)
		{
			return !a.Equals(b);
		}

		public bool Equals(MatrixVector other)
		{
			return other.Row == Row && other.Col == Col;
		}

		public override string ToString()
		{
			return "[" + Row + ", " + Col + "]";
		}

		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != GetType() || obj.GetHashCode() != GetHashCode())
			{
				return false;
			}

			MatrixVector other = (MatrixVector)obj;
			return other.Row == Row && other.Col == Col;
		}

		public override int GetHashCode()
		{
			return (Row << 16) + Col;
		}
	}
}