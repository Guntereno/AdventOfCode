using System;
using System.Security.Cryptography;
using System.Text;

namespace Advent
{
	class DayFive
	{
		private enum Method
		{
			Part1,
			Part2
		}


		public static Solution GetSolution()
		{
			const string kInput = "uqwqemis";

			string passcode1 = GeneratePasscode(kInput, Method.Part1);
			string passcode2 = GeneratePasscode(kInput, Method.Part2);

			return new Solution()
			{
				Name = "Day Five",
				Parts = new string[]
				{
					passcode1,
					passcode2
				}
			};
		}

		private const char kUnset = '\0';

		private static string GeneratePasscode(string kInput, Method method)
		{
			char[] passcode = "\0\0\0\0\0\0\0\0".ToCharArray();

			var md5 = MD5.Create();
			var random = new Random();

			int index = 0;
			bool succeeded;
			do
			{
				succeeded = false;

				string input = kInput + (index++);
				string hash = GenerateHash(md5, input);
				if (hash.StartsWith("00000"))
				{
					char character, location;
					switch (method)
					{
						case Method.Part1:
						{
							character = hash[5];
							location = NextFreeIndex(passcode);
						}
						break;

						case Method.Part2:
						{
							location = (char)(hash[5] - '0');
							character = hash[6];
						}
						break;

						default:
						{
							throw new Exception("Invalid method!: " + method);
						}
					}

					if ((location < passcode.Length) && (passcode[location] == kUnset))
					{
						passcode[location] = character;
						succeeded = true;
					}
				}

				if ((index % 1024) == 0)
				{
					OutputProgressToConsole(passcode, random, index);
				}
			}
			while (!succeeded || !IsCodeComplete(passcode));

			return new string(passcode);
		}

		private static void OutputProgressToConsole(char[] passcode, Random random, int index)
		{
			int y = Console.CursorTop;
			Console.SetCursorPosition(0, y);

			for (int i = 0; i < passcode.Length; ++i)
			{
				if (passcode[i] == kUnset)
				{
					Console.Write((char)random.Next('!', '?'));
				}
				else
				{
					Console.Write(passcode[i]);
				}
			}
			Console.Write(" Attempts: " + index);
		}

		private static char NextFreeIndex(char[] code)
		{
			for (char i = '\0'; i < code.Length; ++i)
			{
				if (code[i] == kUnset)
				{
					return i;
				}
			}

			return char.MaxValue;
		}

		private static bool IsCodeComplete(char[] code)
		{
			for (char i = '\0'; i < code.Length; ++i)
			{
				if (code[i] == kUnset)
				{
					return false;
				}
			}

			return true;
		}

		private static string GenerateHash(MD5 md5, string input)
		{
			byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
			byte[] hash = md5.ComputeHash(inputBytes);

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < hash.Length; i++)
			{
				sb.Append(hash[i].ToString("X2"));
			}

			return sb.ToString();
		}
	}
}
