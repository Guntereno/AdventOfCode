using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent
{
	public static class DayThree
	{
		public static Solution GetSolution(string input)
		{
			int answer1, answer2;

			string[] lines = input.Trim().Split('\n');

			// Part One
			{
				var tris = new Triangle[lines.Length];
				for (int i = 0; i < tris.Length; ++i)
				{
					float[] vals = ParseLine(lines[i]);
					tris[i] = new Triangle(vals[0], vals[1], vals[2]);
				}

				answer1 = CountPossible(tris);
			}

			// Part Two
			{
				var tris = new List<Triangle>(lines.Length);

				int currentLine = 0;
				const int kNumColumns = Triangle.kNumSides; // Assume hard coded column count. Ugh, stupid easter bunny.
				while (currentLine < lines.Length)
				{
					var currentTris = new Triangle[kNumColumns];

					for (int side = 0; side < Triangle.kNumSides; ++side)
					{
						float[] vals = ParseLine(lines[currentLine]);
						for (int tri = 0; tri < currentTris.Length; ++tri)
						{
							currentTris[tri][side] = vals[tri];
						}

						++currentLine;
					}

					for (int tri = 0; tri < currentTris.Length; ++tri)
					{
						tris.Add(currentTris[tri]);
					}
				}

				answer2 = CountPossible(tris.ToArray());
			}

			return new Solution()
			{
				Name = "Day Three",
				Parts = new string[]
				{
					answer1.ToString(),
					answer2.ToString()
				}
			};
		}

		private static int CountPossible(Triangle[] tris)
		{
			return tris.Where((tri) => tri.IsPossible).Count();
		}

		private static float[] ParseLine(string line)
		{
			string[] valStrings = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
			float[] vals = valStrings.Select(float.Parse).ToArray();

			return vals;
		}

		private struct Triangle
		{
			public float X;
			public float Y;
			public float Z;

			public const int kNumSides = 3;

			public Triangle(float x, float y, float z)
			{
				X = x;
				Y = y;
				Z = z;
			}

			public float this[int key]
			{
				get
				{
					switch (key)
					{
						case 0:
						return X;

						case 1:
						return Y;

						case 2:
						return Z;

						default:
						throw new ArgumentException("Index out of bounds!");
					}
				}

				set
				{
					switch (key)
					{
						case 0:
						X = value;
						break;

						case 1:
						Y = value;
						break;

						case 2:
						Z = value;
						break;

						default:
						throw new ArgumentException("Index out of bounds!");
					}
				}
			}

			public bool IsPossible
			{
				get
				{
					for (int i = 0; i < kNumSides; ++i)
					{
						float a = 0.0f;
						float b = 0.0f;

						for (int j = 0; j < kNumSides; ++j)
						{
							if (i == j)
							{
								a += this[j];
							}
							else
							{
								b += this[j];
							}
						}

						if (a >= b)
						{
							return false;
						}
					}

					return true;
				}
			}
		}
	}
}
