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
			Console.WriteLine(DayOne.GetSolution());
			Console.WriteLine(DayTwo.GetSolution());

			string day3Input = File.ReadAllText("data/day3.txt");
			Console.WriteLine(DayThree.GetSolution(day3Input));
		}
	}
}
