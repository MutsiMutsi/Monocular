using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Monocular.UIElements
{
	public class Panel : UIElement
	{

		public bool DrawBackground = true;
		public bool DrawBorder = true;

		public Action PreRenderAction;

		public Panel(Rect rect) : base(rect)
		{
		}

		public override void Render(SpriteBatch sb)
		{
			PreRenderAction?.Invoke();

			UpdateDrawRect();

			//Draw background fill
			if (DrawBackground)
			{
				sb.Draw(UIManager.Texture, new Rectangle(DrawRectangle.X + 8, DrawRectangle.Y + 8, DrawRectangle.Width - 16, DrawRectangle.Height - 16), new Rectangle(8, 8, 8, 8), Color);
			}

			if (DrawBorder)
			{
				//Draw corners
				sb.Draw(UIManager.Texture, DrawRectangle.Location.ToVector2(), new Rectangle(0, 0, 8, 8), Color);
				sb.Draw(UIManager.Texture, DrawRectangle.Location.ToVector2() + new Vector2(DrawRectangle.Width - 8, 0), new Rectangle(16, 0, 8, 8), Color);
				sb.Draw(UIManager.Texture, DrawRectangle.Location.ToVector2() + new Vector2(0, DrawRectangle.Height - 8), new Rectangle(0, 16, 8, 8), Color);
				sb.Draw(UIManager.Texture, DrawRectangle.Location.ToVector2() + new Vector2(DrawRectangle.Width - 8, DrawRectangle.Height - 8), new Rectangle(16, 16, 8, 8), Color);

				//Draw edges
				sb.Draw(UIManager.Texture, new Rectangle(DrawRectangle.X + 8, DrawRectangle.Y, DrawRectangle.Width - 16, 8), new Rectangle(8, 0, 8, 8), Color);
				sb.Draw(UIManager.Texture, new Rectangle(DrawRectangle.X, DrawRectangle.Y + 8, 8, DrawRectangle.Height - 16), new Rectangle(0, 8, 8, 8), Color);
				sb.Draw(UIManager.Texture, new Rectangle(DrawRectangle.X + DrawRectangle.Width - 8, DrawRectangle.Y + 8, 8, DrawRectangle.Height - 16), new Rectangle(16, 8, 8, 8), Color);
				sb.Draw(UIManager.Texture, new Rectangle(DrawRectangle.X + 8, DrawRectangle.Y + DrawRectangle.Height - 8, DrawRectangle.Width - 16, 8), new Rectangle(8, 16, 8, 8), Color);
			}

			//PUSH A NEW FRESH CURSOR FOR CONTENTS
			UIManager.PushCursor(DrawRectangle);
			base.Render(sb);
			//POP THAT FRESH CURSOR
			UIManager.Cursor.Pop();
		}
	}
}
