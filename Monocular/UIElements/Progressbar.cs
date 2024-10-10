using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monocular.UIElements
{
	public class Progressbar : UIElement
	{
		public float Value;
		private Color borderColour = new Color(.1f, .1f, .1f);
		public Progressbar(Rect rect) : base(rect)
		{
		}

		public override void Update(float dt)
		{

			base.Update(dt);
		}

		public override void Render(SpriteBatch sb)
		{
			UpdateDrawRect();

			int offsetX = 8 * 3;

			//Draw background fill
			sb.Draw(UIManager.Texture, new Rectangle(DrawRectangle.X + 8, DrawRectangle.Y + 8, (int)((DrawRectangle.Width - 16) * Value), DrawRectangle.Height - 16), new Rectangle(offsetX + 8, 8, 8, 8), Color);

			//Draw corners
			sb.Draw(UIManager.Texture, DrawRectangle.Location.ToVector2(), new Rectangle(offsetX, 0, 8, 8), borderColour);
			sb.Draw(UIManager.Texture, DrawRectangle.Location.ToVector2() + new Vector2(DrawRectangle.Width - 8, 0), new Rectangle(offsetX + 16, 0, 8, 8), borderColour);
			sb.Draw(UIManager.Texture, DrawRectangle.Location.ToVector2() + new Vector2(0, DrawRectangle.Height - 8), new Rectangle(offsetX, 16, 8, 8), borderColour);
			sb.Draw(UIManager.Texture, DrawRectangle.Location.ToVector2() + new Vector2(DrawRectangle.Width - 8, DrawRectangle.Height - 8), new Rectangle(offsetX + 16, 16, 8, 8), borderColour);

			//Draw edges
			sb.Draw(UIManager.Texture, new Rectangle(DrawRectangle.X + 8, DrawRectangle.Y, DrawRectangle.Width - 16, 8), new Rectangle(offsetX + 8, 0, 8, 8), borderColour);
			sb.Draw(UIManager.Texture, new Rectangle(DrawRectangle.X, DrawRectangle.Y + 8, 8, DrawRectangle.Height - 16), new Rectangle(offsetX + 0, 8, 8, 8), borderColour);
			sb.Draw(UIManager.Texture, new Rectangle(DrawRectangle.X + DrawRectangle.Width - 8, DrawRectangle.Y + 8, 8, DrawRectangle.Height - 16), new Rectangle(offsetX + 16, 8, 8, 8), borderColour);
			sb.Draw(UIManager.Texture, new Rectangle(DrawRectangle.X + 8, DrawRectangle.Y + DrawRectangle.Height - 8, DrawRectangle.Width - 16, 8), new Rectangle(offsetX + 8, 16, 8, 8), borderColour);

			//PUSH A NEW FRESH CURSOR FOR CONTENTS
			DrawRectangle.Y -= 1;
			UIManager.PushCursor(DrawRectangle);
			base.Render(sb);
			//POP THAT FRESH CURSOR
			_ = UIManager.Cursor.Pop();
		}
	}
}
