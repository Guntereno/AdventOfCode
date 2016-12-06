using System.Text;

namespace Advent
{
	public class Solution
	{
		public string Name { get; set;}
		public string [] Parts { get; set; }

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendLine(Name);
			if (Parts != null)
			{
				for (int i = 0; i < Parts.Length; ++i)
				{
					sb.Append("Part ");
					sb.Append(i.ToString());
					sb.Append(": ");
					sb.AppendLine(Parts[i]);
				}
			}

			return sb.ToString();
		}
	}
}
