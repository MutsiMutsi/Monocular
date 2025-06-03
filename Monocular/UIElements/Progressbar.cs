using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocular.Util;

namespace Monocular.UIElements
{
	public class Progressbar : UIElement
	{
		public float Value;
		private Color borderColour = Color.White;
		public bool drawOutline = true;
		public bool drawBackground = true;

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
			if (!drawOutline)
			{
				sb.Draw(UIManager.Texture, new Rectangle(DrawRectangle.X, DrawRectangle.Y, (int)((DrawRectangle.Width)), DrawRectangle.Height), new Rectangle(255, 0, 1, 1), Color * 0.35f);
			}


			if (drawOutline)
			{
				//Draw fill
				NineSlice outlinens = new NineSlice(new(24, 0, 24, 24));
				outlinens.Draw(sb, UIManager.Texture, DrawRectangle, borderColour);

				//Draw corners
				/*sb.Draw(UIManager.Texture, DrawRectangle.Location.ToVector2(), new Rectangle(offsetX, 0, 8, 8), borderColour);
				sb.Draw(UIManager.Texture, DrawRectangle.Location.ToVector2() + new Vector2(DrawRectangle.Width - 8, 0), new Rectangle(offsetX + 16, 0, 8, 8), borderColour);
				sb.Draw(UIManager.Texture, DrawRectangle.Location.ToVector2() + new Vector2(0, DrawRectangle.Height - 8), new Rectangle(offsetX, 16, 8, 8), borderColour);
				sb.Draw(UIManager.Texture, DrawRectangle.Location.ToVector2() + new Vector2(DrawRectangle.Width - 8, DrawRectangle.Height - 8), new Rectangle(offsetX + 16, 16, 8, 8), borderColour);

				//Draw edges
				sb.Draw(UIManager.Texture, new Rectangle(DrawRectangle.X + 8, DrawRectangle.Y, DrawRectangle.Width - 16, 8), new Rectangle(offsetX + 8, 0, 8, 8), borderColour);
				sb.Draw(UIManager.Texture, new Rectangle(DrawRectangle.X, DrawRectangle.Y + 8, 8, DrawRectangle.Height - 16), new Rectangle(offsetX + 0, 8, 8, 8), borderColour);
				sb.Draw(UIManager.Texture, new Rectangle(DrawRectangle.X + DrawRectangle.Width - 8, DrawRectangle.Y + 8, 8, DrawRectangle.Height - 16), new Rectangle(offsetX + 16, 8, 8, 8), borderColour);
				sb.Draw(UIManager.Texture, new Rectangle(DrawRectangle.X + 8, DrawRectangle.Y + DrawRectangle.Height - 8, DrawRectangle.Width - 16, 8), new Rectangle(offsetX + 8, 16, 8, 8), borderColour);*/
			}

			//Draw fill
			NineSlice ns = new NineSlice(new(0, 80, 24, 24));
			ns.Draw(sb, UIManager.Texture, new Rectangle(DrawRectangle.X + 2, DrawRectangle.Y + 2, (int)((DrawRectangle.Width - 4) * Value), DrawRectangle.Height - 4), Color);

			//PUSH A NEW FRESH CURSOR FOR CONTENTS
			DrawRectangle.Y -= 1;
			UIManager.PushCursor(DrawRectangle);
			base.Render(sb);
			//POP THAT FRESH CURSOR
			_ = UIManager.Cursor.Pop();
		}
	}
}
