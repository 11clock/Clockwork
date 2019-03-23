using System;
using System.Collections.Generic;

namespace Clockwork.Utils
{
	/// <summary>
	///  Reimplementation of GDScript's "rand" functions.
	/// </summary>
	/// <remarks>
	///	Uses System.Random under the hood, so it will not have the same output as GDScript's "rand" functions.
	/// </remarks>
	public static class Rng {
	
		private static Random _rand;

		static Rng() {
			_rand = new Random();
		}
	
		/*---Reimplementing rand methods from GDScript---*/
		
		/// <summary>
		/// Random range, any floating point value between <c>from</c> and <c>to</c>.
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		public static float RandRange(float from, float to)
		{
			return _rand.NextFloat() * (to - from) + from;
		}
		
		/// <summary>
		/// Returns a random floating point value between 0 and 1.
		/// </summary>
		public static float Randf()
		{
			return _rand.NextFloat();
		}
		
		/// <summary>
		/// Returns a random 32 bit integer.
		/// </summary>
		public static int Randi()
		{
			return _rand.Next();
		}
	
		/// <summary>
		/// Randomizes the seed (or the internal state) of the random number generator.
		/// </summary>
		public static void Randomize()
		{
			_rand = new Random();
		}
		
		/// <summary>
		/// Sets seed for the random number generator.
		/// </summary>
		/// <param name="seed"></param>
		public static void Seed(int seed)
		{
			_rand = new Random(seed);
		}
	
		/*---Extra methods for convenience---*/
		
		/// <summary>
		/// Random range, any integer value between <c>from</c> and <c>to</c> exclusive.
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		public static int RandRange(int from, int to)
		{
			return _rand.Next(from, to);
		}
		
		/// <summary>
		/// Random range, any integer value between <c>from</c> and <c>to</c> inclusive.
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		public static int RandRangeIn(int from, int to)
		{
			return _rand.Next(from, to + 1);
		}
		
		/// <summary>
		/// Random range, any integer value between 0 and <c>to</c> exclusive.
		/// </summary>
		/// <param name="to"></param>
		public static int RandRange(int to)
		{
			return _rand.Next(to);
		}
		
		/// <summary>
		/// Random range, any integer value between 0 and <c>to</c> inclusive.
		/// </summary>
		/// <param name="to"></param>
		public static int RandRangeIn(int to)
		{
			return _rand.Next(to + 1);
		}
		
		/// <summary>
		/// Random range, any floating point value between 0 and <c>to</c>.
		/// </summary>
		/// <param name="to"></param>
		public static float RandRange(float to)
		{
			return RandRange(0f, to);
		}
		
		/// <summary>
		/// Randomly returns true or false, based on <c>ratio</c>.
		/// </summary>
		/// <param name="ratio"></param>
		public static bool Chance(float ratio)
		{
			return Randf() < ratio;
		}

		public static MatrixVector RandRange(MatrixVector min, MatrixVector max)
		{
			return new MatrixVector(RandRange(min.Row, max.Row), RandRange(min.Col, max.Col));
		}
		
		public static MatrixVector RandRangeIn(MatrixVector min, MatrixVector max)
		{
			return new MatrixVector(RandRangeIn(min.Row, max.Row), RandRangeIn(min.Col, max.Col));
		}

		private static float NextFloat(this Random rand)
		{
			return (float) rand.NextDouble();
		}
		
		/// <summary>
		/// Shuffles the contents of the given list.
		/// </summary>
		/// <param name="list"></param>
		/// <typeparam name="T"></typeparam>
		public static void Shuffle<T>(this IList<T> list)  
		{  
			int n = list.Count;  
			while (n > 1) {  
				n--;  
				int k = _rand.Next(n + 1);  
				T value = list[k];  
				list[k] = list[n];  
				list[n] = value;  
			}  
		}
		
		public static T GetRandomValue<T>(this T[] array)
		{
			return array[RandRange(0, array.Length)];
		}
		
		public static T GetRandomValue<T>(this List<T> list)
		{
			return list[RandRange(0, list.Count)];
		}
    
	}
}