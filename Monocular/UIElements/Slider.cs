using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monocular.Enum;

namespace Monocular.UIElements
{
	public class Slider : UIElement
	{
		public float Value = 1.0f;
		public Action<float>? OnValueChanged = null;

		private Rectangle handleRect;

		private readonly Color[] stateColours = [Color.Gray, Color.DarkGray, Color.LightGray];
		private Color borderColour = new Color(.1f, .1f, .1f);

		public Slider(Rect rect, Action<float>? onValueChanged = null) : base(rect)
		{
			this.OnValueChanged = onValueChanged;
		}

		public override bool Input(KeyboardState ks, MouseState ms)
		{
			if (!UIManager.LeftMouseButtonCaptured && new Rectangle(DrawRectangle.X, DrawRectangle.Y, DrawRectangle.Width, DrawRectangle.Height).Contains(ms.Position))
			{
				State = InteractionState.Hover;
				if (ms.LeftButton == ButtonState.Pressed)
				{
					UIManager.LeftMouseButtonCaptured = true;
					State = InteractionState.Active;
				}
			}
			else if (State == InteractionState.Hover)
			{
				State = InteractionState.None;
			}

			if (State == InteractionState.Active)
			{
				if (ms.LeftButton != ButtonState.Pressed)
				{
					State = InteractionState.None;
				}

				float newValue = (ms.Position.X - DrawRectangle.X - 16) / ((float)DrawRectangle.Width - 32);
				newValue = MathHelper.Clamp(newValue, 0f, 1f);

				if (newValue != Value)
				{
					Value = newValue;
					OnValueChanged?.Invoke(newValue);
				}
			}

			return base.Input(ks, ms);
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

			//Draw handle
			handleRect = new Rectangle((int)((DrawRectangle.Width - 32) * Value) + DrawRectangle.X + 8, DrawRectangle.Y + 8, 16, DrawRectangle.Height - 16);
			sb.Draw(UIManager.Texture, handleRect, new Rectangle(offsetX + 8, 8, 8, 8), stateColours[(int)State]);

			//PUSH A NEW FRESH CURSOR FOR CONTENTS
			DrawRectangle.Y += 3;
			UIManager.PushCursor(DrawRectangle);
			base.Render(sb);
			//POP THAT FRESH CURSOR
			_ = UIManager.Cursor.Pop();
		}
	}
}
