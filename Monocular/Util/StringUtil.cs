using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace Monocular.Util
{
	public static class StringUtils
	{
		public static string LineBreakText(string text, DynamicSpriteFont font, float maxWidth, bool breakOnWord = true)
		{
			List<string> lines = new List<string>();
			string[] words = text.Split(' ');
			string currentLine = "";

			foreach (string word in words)
			{
				string testLine = currentLine.Length > 0 ? currentLine + " " + word : word;
				Vector2 size = font.MeasureString(testLine);

				if (size.X <= maxWidth)
				{
					currentLine = testLine;
				}
				else
				{
					if (breakOnWord)
					{
						if (currentLine.Length > 0)
							lines.Add(currentLine);
						currentLine = word;
					}
					else
					{
						while (currentLine.Length > 0 && font.MeasureString(currentLine + "-").X > maxWidth)
						{
							int lastChar = currentLine.Length - 1;
							lines.Add(currentLine[..lastChar] + "-");
							currentLine = currentLine[lastChar..];
						}
						currentLine += word;
					}
				}
			}

			if (currentLine.Length > 0)
				lines.Add(currentLine);

			return string.Join("\n", lines);
		}
	}
}
