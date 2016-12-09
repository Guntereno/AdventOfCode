using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent
{
	class Program
	{
		static void Main(string[] args)
		{
			Solution[] solutions = new Solution[]
			{
				DayOne.GetSolution(),
				DayTwo.GetSolution(),
				DayThree.GetSolution(File.ReadAllText("data/day3.txt")),
				DayFour.GetSolution(File.ReadAllText("data/day4.txt")),
				DayFive.GetSolution()
			};

			for (int i = 0; i < solutions.Length; ++i)
			{
				Console.Write(solutions[i]);
			}

			return;
		}
	}
}
