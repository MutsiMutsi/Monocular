using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monocular.Enum;

namespace Monocular.UIElements
{
	public class Button : UIElement
	{
		public Button(Rect rect, Action? onClick = null) : base(rect)
		{
			this.OnClick = onClick;
		}

		private readonly Color[] stateColours = [new Color(1f, 1f, 1f), new Color(.8f, .8f, .8f), new Color(.5f, .5f, .5f)];


		private Color StateBlendColour => Color.Lerp(Color, stateColours[(int)State], 0.8f);

		public override void Render(SpriteBatch sb)
		{
			UpdateDrawRect();

			int offsetX = 8 * 3;

			Color stateColour = StateBlendColour;

			//Draw background fill
			sb.Draw(UIManager.Texture, new Rectangle(DrawRectangle.X + 8, DrawRectangle.Y + 8, DrawRectangle.Width - 16, DrawRectangle.Height - 16), new Rectangle(8 + offsetX, 8, 8, 8), stateColour);

			//Draw corners
			sb.Draw(UIManager.Texture, DrawRectangle.Location.ToVector2(), new Rectangle(offsetX, 0, 8, 8), stateColour);
			sb.Draw(UIManager.Texture, DrawRectangle.Location.ToVector2() + new Vector2(DrawRectangle.Width - 8, 0), new Rectangle(offsetX + 16, 0, 8, 8), stateColour);
			sb.Draw(UIManager.Texture, DrawRectangle.Location.ToVector2() + new Vector2(0, DrawRectangle.Height - 8), new Rectangle(offsetX, 16, 8, 8), stateColour);
			sb.Draw(UIManager.Texture, DrawRectangle.Location.ToVector2() + new Vector2(DrawRectangle.Width - 8, DrawRectangle.Height - 8), new Rectangle(offsetX + 16, 16, 8, 8), stateColour);

			//Draw edges
			sb.Draw(UIManager.Texture, new Rectangle(DrawRectangle.X + 8, DrawRectangle.Y, DrawRectangle.Width - 16, 8), new Rectangle(offsetX + 8, 0, 8, 8), stateColour);
			sb.Draw(UIManager.Texture, new Rectangle(DrawRectangle.X, DrawRectangle.Y + 8, 8, DrawRectangle.Height - 16), new Rectangle(offsetX + 0, 8, 8, 8), stateColour);
			sb.Draw(UIManager.Texture, new Rectangle(DrawRectangle.X + DrawRectangle.Width - 8, DrawRectangle.Y + 8, 8, DrawRectangle.Height - 16), new Rectangle(offsetX + 16, 8, 8, 8), stateColour);
			sb.Draw(UIManager.Texture, new Rectangle(DrawRectangle.X + 8, DrawRectangle.Y + DrawRectangle.Height - 8, DrawRectangle.Width - 16, 8), new Rectangle(offsetX + 8, 16, 8, 8), stateColour);

			//PUSH A NEW FRESH CURSOR FOR CONTENTS
			UIManager.PushCursor(DrawRectangle);
			base.Render(sb);
			//POP THAT FRESH CURSOR
			_ = UIManager.Cursor.Pop();
		}
	}
}
