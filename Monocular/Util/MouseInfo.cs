using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monocular.Util
{
	public struct MouseInfo
	{
		public ButtonState LeftButton;
		public int X;
		public int Y;
		public Point Position => new Point(X, Y);

		public static MouseInfo FromState(MouseState ms)
		{
			return new MouseInfo
			{
				LeftButton = ms.LeftButton,
				X = ms.X,
				Y = ms.Y,
			};
		}
	}
}
