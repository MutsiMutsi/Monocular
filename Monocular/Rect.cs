
using Microsoft.Xna.Framework;

namespace Monocular
{
	public record struct Rect(int X, int Y, int Width, int Height)
	{
		public Point Location => new Point(X, Y);
	}

	public struct Cursor
	{
		public Rect Rect;
		public int PosX;
		public int PosY;
		public int LineMax;

		public Cursor(Rect r)
		{
			Rect = r;
			PosX = 0;
			PosY = 0;
			LineMax = 0;
		}
	}
}
