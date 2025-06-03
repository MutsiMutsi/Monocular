using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monocular.Enum;

namespace Monocular.UIElements
{
	public class ProgressCircle : UIElement
	{
		public float Value;
		public float RotationOffset = 0f;
		private Label label;

		public ProgressCircle(Rect rect) : base(rect)
		{
			label = new("")
			{
				Alignment = Align.MiddleCenter
			};
			Children.Add(label);
		}

		public override void Update(float dt)
		{
			label.Text.Clear();
			label.Text.Append($"{(int)(Value * 100)}");
			label.Flush();

			base.Update(dt);
		}

		public override void Render(SpriteBatch sb)
		{
			UpdateDrawRect();

			//Draw complete backdrop
			sb.Draw(UIManager.Texture, new Rectangle(DrawRectangle.X + DrawRectangle.Width / 2, DrawRectangle.Y + DrawRectangle.Height / 2, DrawRectangle.Width, DrawRectangle.Height), new Rectangle(48, 0, 48, 48), Color, 0f, new Vector2(48 / 2, 48 / 2), SpriteEffects.None, 0f);

			//Draw upper semi
			sb.Draw(UIManager.Texture, new Rectangle(DrawRectangle.X + DrawRectangle.Width / 2, DrawRectangle.Y + DrawRectangle.Height / 2, DrawRectangle.Width, DrawRectangle.Height / 2), new Rectangle(48 + 48, 0, 48, 24), Color, RotationOffset + MathF.PI + MathHelper.Clamp(Value, 0f, .5f) * MathF.PI * 2.0f, new Vector2(48 / 2, 48 / 2), SpriteEffects.None, 0f);
			
			//Draw obscurer
			sb.Draw(UIManager.Texture, new Rectangle(DrawRectangle.X + DrawRectangle.Width / 2, DrawRectangle.Y + DrawRectangle.Height / 2, DrawRectangle.Width - 5, DrawRectangle.Height / 2 - 5), new Rectangle(48, 0, 48, 24), Color, RotationOffset + MathF.PI, new Vector2(48 / 2, 48 / 2), SpriteEffects.None, 0f);

			//Draw lower semi
			if (Value > 0.5f)
			{
				sb.Draw(UIManager.Texture, new Rectangle(DrawRectangle.X + DrawRectangle.Width / 2, DrawRectangle.Y + DrawRectangle.Height / 2, DrawRectangle.Width, DrawRectangle.Height / 2), new Rectangle(48 + 48, 0, 48, 24), Color, RotationOffset + MathF.PI + Value * MathF.PI * 2.0f, new Vector2(48 / 2, 48 / 2), SpriteEffects.None, 0f);
			}

			//Draw overtop
			sb.Draw(UIManager.Texture, new Rectangle(DrawRectangle.X + DrawRectangle.Width / 2, DrawRectangle.Y + DrawRectangle.Height / 2, DrawRectangle.Width, DrawRectangle.Height), new Rectangle(48, 48, 48, 48), Color.White, 0f, new Vector2(48 / 2, 48 / 2), SpriteEffects.None, 0f);

			DrawRectangle.Y -= 1;
			UIManager.PushCursor(DrawRectangle);
			base.Render(sb);
			//POP THAT FRESH CURSOR
			_ = UIManager.Cursor.Pop();
		}
	}
}
