using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent
{
	public static class DayFour
	{
		public static Solution GetSolution(string input)
		{
			string[] lines = input.Trim().Split('\n');
			Room[] rooms = lines.Select((line) =>
			{
				return new Room(line);
			}).ToArray();


			Room[] realRooms = rooms.Where((room) =>
			{
				return IsRoomReal(room);
			}).ToArray();

			int sumSectors = realRooms.Sum((room) =>
				{
					return room.Sector;
				});


			Room correctRoom = realRooms.Where((room) =>
			{
				return room.DecryptedName.Contains("northpole");
			}).First();

			return new Solution()
			{
				Name = "Day Four",
				Parts = new string[]
				{
					sumSectors.ToString(),
					correctRoom.Sector.ToString()
				}
			};
		}

		private static bool IsRoomReal(Room room)
		{
			var frequencyDict = new Dictionary<char, int>();
			foreach (char ch in room.Name)
			{
				if (ch == '-')
				{
					continue;
				}

				if (frequencyDict.ContainsKey(ch))
				{
					++frequencyDict[ch];
				}
				else
				{
					frequencyDict[ch] = 1;
				}
			}

			List<char> keys = frequencyDict.Keys.ToList();
			keys.Sort((a, b) =>
			{
				int freqA = frequencyDict[a];
				int freqB = frequencyDict[b];
				if (freqA == freqB)
				{
					return a.CompareTo(b);
				}
				else
				{
					return freqB.CompareTo(freqA);
				}
			});

			string checksum = new string(keys.GetRange(0, 5).ToArray());
			return checksum == room.Checksum;
		}

		private struct Room
		{
			public string Name;
			public int Sector;
			public string Checksum;

			public string DecryptedName
			{
				get
				{
					var str = new StringBuilder();

					foreach (char ch in Name)
					{
						const int kA = 'a';
						const int kZ = 'z';
						const int kOffset = kZ - kA;

						if (ch == '-')
						{
							str.Append(' ');
						}
						else
						{
							int startOffset = ch - kA;
							int newOffset = (startOffset + Sector) % (kOffset + 1);
							str.Append((char)(kA + newOffset));
						}
					}

					return str.ToString();
				}
			}

			public Room(string str)
			{
				Parse(str, out Name, out Sector, out Checksum);
			}

			private static void Parse(string str, out string name, out int sector, out string checksum)
			{
				string currentWord = "";

				name = null;
				sector = -1;
				checksum = null;

				var nameWords = new List<string>();

				foreach (char ch in str)
				{
					switch (ch)
					{
						case '-':
						{
							nameWords.Add(currentWord);
							currentWord = "";
						}
						break;

						case '[':
						{
							sector = int.Parse(currentWord);
							currentWord = "";
						}
						break;

						case ']':
						{
							checksum = currentWord;
							currentWord = "";
						}
						break;
						
						default:
						{
							currentWord += ch;
						}
						break;
					}
				}

				name = String.Join("-", nameWords.ToArray());
			}
		}
	}
}
