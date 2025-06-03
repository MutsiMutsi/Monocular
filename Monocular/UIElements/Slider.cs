using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monocular.Enum;
using Monocular.Util;

namespace Monocular.UIElements
{
	public class Slider : UIElement
	{
		public float Value = 1.0f;
		public Action<float>? OnValueChanged = null;

		private Rectangle handleRect;

		private readonly Color[] stateColours = [Color.Gray, Color.DarkGray, Color.LightGray];
		private Color borderColour = new Color(1f, 1f, 1f);

		public Slider(Rect rect, Action<float>? onValueChanged = null) : base(rect)
		{
			this.OnValueChanged = onValueChanged;
		}

		public override bool Input(KeyboardState ks, MouseInfo ms)
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

			NineSlice sliderNS = new NineSlice(new(24, 0, 24, 24));
			var slideRect = DrawRectangle;
			slideRect.X += 5;
			slideRect.Y += 5;
			slideRect.Width -= 10;
			slideRect.Height -= 10;
			sliderNS.Draw(sb, UIManager.Texture, slideRect, Color.White);


			NineSlice handleNS = new NineSlice(new(0, 80, 24, 24));

			//Draw handle
			handleRect = new Rectangle(
				(int)((DrawRectangle.Width - 24) * Value) + DrawRectangle.X,
				DrawRectangle.Y + 2,
				24,
				DrawRectangle.Height - 4);
			handleNS.Draw(sb, UIManager.Texture, handleRect, stateColours[(int)State]);

			//PUSH A NEW FRESH CURSOR FOR CONTENTS
			DrawRectangle.Y += 3;
			UIManager.PushCursor(DrawRectangle);
			base.Render(sb);
			//POP THAT FRESH CURSOR
			_ = UIManager.Cursor.Pop();
		}
	}
}
