using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent
{
	public static class DayTwo
	{
		const string kInput =
			"LURLDDLDULRURDUDLRULRDLLRURDUDRLLRLRURDRULDLRLRRDDULUDULURULLURLURRRLLDURURLLUURDLLDUUDRRDLDLLRUUDURURRULURUURLDLLLUDDUUDRULLRUDURRLRLLDRRUDULLDUUUDLDLRLLRLULDLRLUDLRRULDDDURLUULRDLRULRDURDURUUUDDRRDRRUDULDUUULLLLURRDDUULDRDRLULRRRUUDUURDULDDRLDRDLLDDLRDLDULUDDLULUDRLULRRRRUUUDULULDLUDUUUUDURLUDRDLLDDRULUURDRRRDRLDLLURLULDULRUDRDDUDDLRLRRDUDDRULRULULRDDDDRDLLLRURDDDDRDRUDUDUUDRUDLDULRUULLRRLURRRRUUDRDLDUDDLUDRRURLRDDLUUDUDUUDRLUURURRURDRRRURULUUDUUDURUUURDDDURUDLRLLULRULRDURLLDDULLDULULDDDRUDDDUUDDUDDRRRURRUURRRRURUDRRDLRDUUULLRRRUDD\n" +
			"DLDUDULDLRDLUDDLLRLUUULLDURRUDLLDUDDRDRLRDDUUUURDULDULLRDRURDLULRUURRDLULUDRURDULLDRURUULLDLLUDRLUDRUDRURURUULRDLLDDDLRUDUDLUDURLDDLRRUUURDDDRLUDDDUDDLDUDDUUUUUULLRDRRUDRUDDDLLLDRDUULRLDURLLDURUDDLLURDDLULLDDDRLUDRDDLDLDLRLURRDURRRUDRRDUUDDRLLUDLDRLRDUDLDLRDRUDUUULULUDRRULUDRDRRLLDDRDDDLULURUURULLRRRRRDDRDDRRRDLRDURURRRDDULLUULRULURURDRRUDURDDUURDUURUURUULURUUDULURRDLRRUUDRLLDLDRRRULDRLLRLDUDULRRLDUDDUUURDUDLDDDUDL\n" +
			"RURDRUDUUUUULLLUULDULLLDRUULURLDULULRDDLRLLRURULLLLLLRULLURRDLULLUULRRDURRURLUDLULDLRRULRDLDULLDDRRDLLRURRDULULDRRDDULDURRRUUURUDDURULUUDURUULUDLUURRLDLRDDUUUUURULDRDUDDULULRDRUUURRRDRLURRLUUULRUDRRLUDRDLDUDDRDRRUULLLLDUUUULDULRRRLLRLRLRULDLRURRLRLDLRRDRDRLDRUDDDUUDRLLUUURLRLULURLDRRULRULUDRUUURRUDLDDRRDDURUUULLDDLLDDRUDDDUULUDRDDLULDDDDRULDDDDUUUURRLDUURULRDDRDLLLRRDDURUDRRLDUDULRULDDLDDLDUUUULDLLULUUDDULUUDLRDRUDLURDULUDDRDRDRDDURDLURLULRUURDUDULDDLDDRUULLRDRLRRUURRDDRDUDDLRRLLDRDLUUDRRDDDUUUDLRRLDDDUDRURRDDUULUDLLLRUDDRULRLLLRDLUDUUUUURLRRUDUDDDDLRLLULLUDRDURDDULULRDRDLUDDRLURRLRRULRL\n" +
			"LDUURLLULRUURRDLDRUULRDRDDDRULDLURDDRURULLRUURRLRRLDRURRDRLUDRUUUULLDRLURDRLRUDDRDDDUURRDRRURULLLDRDRDLDUURLDRUULLDRDDRRDRDUUDLURUDDLLUUDDULDDULRDDUUDDDLRLLLULLDLUDRRLDUUDRUUDUDUURULDRRLRRDLRLURDRURURRDURDURRUDLRURURUUDURURUDRURULLLLLUDRUDUDULRLLLRDRLLRLRLRRDULRUUULURLRRLDRRRDRULRUDUURRRRULDDLRULDRRRDLDRLUDLLUDDRURLURURRLRUDLRLLRDLLDRDDLDUDRDLDDRULDDULUDDLLDURDULLDURRURRULLDRLUURURLLUDDRLRRUUDULRRLLRUDRDUURLDDLLURRDLRUURLLDRDLRUULUDURRDULUULDDLUUUDDLRRDRDUDLRUULDDDLDDRUDDD\n" +
			"DRRDRRURURUDDDRULRUDLDLDULRLDURURUUURURLURURDDDDRULUDLDDRDDUDULRUUULRDUDULURLRULRDDLDUDLDLULRULDRRLUDLLLLURUDUDLLDLDRLRUUULRDDLUURDRRDLUDUDRULRRDDRRLDUDLLDLURLRDLRUUDLDULURDDUUDDLRDLUURLDLRLRDLLRUDRDUURDDLDDLURRDDRDRURULURRLRLDURLRRUUUDDUUDRDRULRDLURLDDDRURUDRULDURUUUUDULURUDDDDUURULULDRURRDRDURUUURURLLDRDLDLRDDULDRLLDUDUDDLRLLRLRUUDLUDDULRLDLLRLUUDLLLUUDULRDULDLRRLDDDDUDDRRRDDRDDUDRLLLDLLDLLRDLDRDLUDRRRLDDRLUDLRLDRUURUDURDLRDDULRLDUUUDRLLDRLDLLDLDRRRLLULLUDDDLRUDULDDDLDRRLLRDDLDUULRDLRRLRLLRUUULLRDUDLRURRRUULLULLLRRURLRDULLLRLDUUUDDRLRLUURRLUUUDURLRDURRDUDDUDDRDDRUD";

		static readonly char[,] kKeypad1 =
		{
			{ '1', '2', '3' },
			{ '4', '5', '6' },
			{ '7', '8', '9' }
		};

		static readonly char[,] kKeypad2 =
		{
			{ '\0', '\0', '1', '\0', '\0' },
			{ '\0',  '2', '3',  '4', '\0' },
			{  '5',  '6', '7',  '8',  '9' },
			{ '\0',  'A', 'B',  'C', '\0' },
			{ '\0', '\0', 'D', '\0', '\0' },
		};

		public static string GetSolution()
		{
			string answer = "Day 1\n";

			answer += "Part One: " + CombinationToString(GetCombination(kKeypad1)) + "\n";
			answer += "Part Two: " + CombinationToString(GetCombination(kKeypad2));

			return answer;
		}

		private static char[] GetCombination(char[,] keypad)
		{
			var follower = new KeypadFollower(keypad);

			string[] lines = kInput.Split('\n');
			var combination = new char[lines.Length];
			for (int i = 0; i < lines.Length; ++i)
			{
				char digit = follower.FindDigit(lines[i]);
				combination[i] = digit;
			}

			return combination;
		}

		private static string CombinationToString(char[] combination)
		{
			return string.Join(",", combination.Select(x => x.ToString()).ToArray());
		}

		private class KeypadFollower
		{
			private char[,] _keypad;
			private Location _currentLocation;

			public KeypadFollower(char[,] keypad)
			{
				_keypad = keypad;
				_currentLocation = new Location(1, 1);
			}

			public char FindDigit(string line)
			{
				foreach(char ch in line)
				{
					Go(ch);
				}

				return GetDigitAtLocation(_currentLocation);
			}

			private void Go(char dir)
			{
				switch (dir)
				{
					case 'U':
					{
						int newY = _currentLocation.Y - 1;
						Location newLocation = _currentLocation.WithY(newY);
						if ((newY >= 0) && (GetDigitAtLocation(newLocation) > '\0'))
						{
							_currentLocation = newLocation;
						}
					}
					break;

					case 'R':
					{
						int newX = _currentLocation.X + 1;
						Location newLocation = _currentLocation.WithX(newX);
						if ((newX < _keypad.GetLength(0)) && (GetDigitAtLocation(newLocation) > '\0'))
						{
							_currentLocation = newLocation;
						}
					}
					break;

					case 'D':
					{
						int newY = _currentLocation.Y + 1;
						Location newLocation = _currentLocation.WithY(newY);
						if (newY < _keypad.GetLength(1) && (GetDigitAtLocation(newLocation) > '\0'))
						{
							_currentLocation = newLocation;
						}
					}
					break;

					case 'L':
					{
						int newX = _currentLocation.X - 1;
						Location newLocation = _currentLocation.WithX(newX);
						if ((newX >= 0) && (GetDigitAtLocation(newLocation) > '\0'))
						{
							_currentLocation = _currentLocation.WithX(newX);
						}
					}
					break;

					default:
					throw new Exception("Invalid direction!: " + dir);
				}
			}

			private char GetDigitAtLocation(Location location)
			{
				return _keypad[location.Y, location.X];
			}
		}
	}
}
