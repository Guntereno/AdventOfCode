﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent
{
	static class DayOne
	{
		public static Solution GetSolution()
		{
			List<string> commands = kInstructions.Split(',').Select(p => p.Trim()).ToList();
			Location? easterBunnyHq = null;

			var turtle = new ManhattanPathFollower();
			turtle.OnLocationVisited += (location, visited) =>
			{
				// Easter Bunny HQ is actually at the first location you visit twice.
				if (easterBunnyHq != null)
				{
					return;
				}
				else if (visited == 2)
				{
					easterBunnyHq = location;
				}
			};


			foreach (string command in commands)
			{
				turtle.ProcessCommand(command);
			}


			return new Solution()
			{
				Name = "Day One",
				Parts = new string[]
				{
					turtle.Location.ManhattanDistanceFromOrigin.ToString(),
					(easterBunnyHq != null) ? easterBunnyHq.Value.ManhattanDistanceFromOrigin.ToString() : "FAILED!"
				}
			};
		}

		private const string kInstructions = "R2, L5, L4, L5, R4, R1, L4, R5, R3, R1, L1, L1, R4, L4, L1, R4, L4, R4, L3, R5, R4, R1, R3, L1, L1, R1, L2, R5, L4, L3, R1, L2, L2, R192, L3, R5, R48, R5, L2, R76, R4, R2, R1, L1, L5, L1, R185, L5, L1, R5, L4, R1, R3, L4, L3, R1, L5, R4, L4, R4, R5, L3, L1, L2, L4, L3, L4, R2, R2, L3, L5, R2, R5, L1, R1, L3, L5, L3, R4, L4, R3, L1, R5, L3, R2, R4, R2, L1, R3, L1, L3, L5, R4, R5, R2, R2, L5, L3, L1, L1, L5, L2, L3, R3, R3, L3, L4, L5, R2, L1, R1, R3, R4, L2, R1, L1, R3, R3, L4, L2, R5, R5, L1, R4, L5, L5, R1, L5, R4, R2, L1, L4, R1, L1, L1, L5, R3, R4, L2, R1, R2, R1, R1, R3, L5, R1, R4";

		private enum Dir
		{
			North,
			East,
			South,
			West
		}

		private class ManhattanPathFollower
		{
			public Location Location { get; private set; }

			public Dir Facing { get; private set; }


			public Action<Location, int> OnLocationVisited;


			private Dictionary<Location, int> _locationVisitedCount = new Dictionary<Location, int>();

			public void ProcessCommand(string command)
			{
				switch (command[0])
				{
					case 'L':
					{
						if (--Facing < 0)
						{
							Facing = Dir.West;
						}
					}
					break;

					case 'R':
					{
						if (++Facing > Dir.West)
						{
							Facing = Dir.North;
						}
					}
					break;

					default:
					throw new Exception("Invalid command!");
				}

				int count = int.Parse(command.Substring(1));
				for (int i = 0; i < count; ++i)
				{
					Walk();
				}
			}

			private void Walk()
			{
				switch (Facing)
				{
					case Dir.North:
					{
						Location = new Location(Location.X, Location.Y + 1);
					}
					break;

					case Dir.East:
					{
						Location = new Location(Location.X + 1, Location.Y);
					}
					break;

					case Dir.South:
					{
						Location = new Location(Location.X, Location.Y - 1);
					}
					break;

					case Dir.West:
					{
						Location = new Location(Location.X - 1, Location.Y);
					}
					break;

					default:
					throw new Exception("Invalid facing!");
				}

				if (_locationVisitedCount.ContainsKey(Location))
				{
					++(_locationVisitedCount[Location]);
				}
				else
				{
					_locationVisitedCount[Location] = 1;
				}

				if (OnLocationVisited != null)
				{
					OnLocationVisited(Location, _locationVisitedCount[Location]);
				}
			}
		}
	}
}
