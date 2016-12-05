using System;

namespace Advent
{
	public struct Location
	{
		public Location(int x, int y)
		{
			X = x;
			Y = y;
		}

		public override string ToString()
		{
			return "[" + X + "," + Y + "]";
		}

		public int ManhattanDistanceFromOrigin
		{
			get
			{
				return Math.Abs(X) + Math.Abs(Y);
			}
		}

		public Location WithX(int x)
		{
			return new Location(x, Y);
		}

		public Location WithY(int y)
		{
			return new Location(X, y);
		}

		public int X;
		public int Y;
	}
}
