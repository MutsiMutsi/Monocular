using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monocular.Enum;

namespace Monocular.UIElements
{
	public class UIElement
	{
		public Rect Rect;
		public LineBreak LineBreak = LineBreak.None;
		public Align Alignment { get; set; }
		public Color Color { get; set; } = Color.White;
		public InteractionState State { get; set; }
		public List<UIElement> Children { get; set; } = [];

		protected Rectangle DrawRectangle;

		public Rectangle GetDrawRect => DrawRectangle;

		public Action<InteractionState>? OnStateChanged = null;
		public Action? OnClick;

		public UIElement(Rect rect, Align alignment = Align.TopLeft)
		{
			Rect = rect;
			Alignment = alignment;
		}

		public void UpdateDrawRect()
		{
			if (LineBreak is LineBreak.Before or LineBreak.Both)
			{
				UIManager.NewLineCursor();
			}

			Cursor cursor = UIManager.GetCursor(Rect.Width);

			// Horizontal alignment
			if ((Alignment & AlignHelper.LeftMask) != 0)
			{
				// It's Left aligned
				// No offset needed
			}
			else if ((Alignment & AlignHelper.CenterMask) != 0)
			{
				// It's Center aligned
				int px = cursor.Rect.X + ((cursor.Rect.Width - Rect.Width) / 2);
				Rect.X = px;
			}
			else
			{
				// It's Right aligned
				int px = cursor.Rect.X + cursor.Rect.Width - Rect.Width - UIManager.Padding;
				Rect.X = px;
			}

			// Vertical alignment
			if ((Alignment & AlignHelper.TopMask) != 0)
			{
				// It's Top aligned
				// No offset needed
			}
			else if ((Alignment & AlignHelper.MiddleMask) != 0)
			{
				// It's Middle aligned
				int py = cursor.Rect.Y + ((cursor.Rect.Height - Rect.Height) / 2);
				Rect.Y = py;
			}
			else
			{
				// It's Bottom aligned
				int py = cursor.Rect.Y + cursor.Rect.Height - Rect.Height - UIManager.Padding;
				Rect.Y = py;
			}

			DrawRectangle = new Rectangle(Rect.X, Rect.Y, Rect.Width, Rect.Height);

			if (DrawRectangle.X == -1)
			{
				DrawRectangle.X = cursor.Rect.X + cursor.PosX;
			}
			if (DrawRectangle.Y == -1)
			{
				DrawRectangle.Y = cursor.Rect.Y + cursor.PosY;
			}
			if (DrawRectangle.Width == -1)
			{
				DrawRectangle.Width = cursor.Rect.Width - cursor.PosX - UIManager.Padding;
			}
			if (DrawRectangle.Height == -1)
			{
				DrawRectangle.Height = cursor.Rect.Height - cursor.PosY - UIManager.Padding;
			}

			UIManager.UpdateLineMax(DrawRectangle.Height);
			UIManager.IncrementCursor(DrawRectangle.Width);

			if (LineBreak is LineBreak.After or LineBreak.Both)
			{
				UIManager.NewLineCursor();
			}
		}

		public virtual bool Input(KeyboardState ks, MouseState ms)
		{
			InteractionState currentState = State;

			if (State == InteractionState.Active)
			{
				if (ms.LeftButton != ButtonState.Pressed)
				{
					State = InteractionState.None;
					if (DrawRectangle.Contains(ms.Position))
					{
						OnClick?.Invoke();
					}
				}
			}

			if (!UIManager.LeftMouseButtonCaptured && DrawRectangle.Contains(ms.Position))
			{

				State = InteractionState.Hover;
				if (ms.LeftButton == ButtonState.Pressed)
				{
					if (OnClick != null)
					{
						UIManager.LeftMouseButtonCaptured = true;
					}
					State = InteractionState.Active;
				}

			}
			else if (State == InteractionState.Hover)
			{
				State = InteractionState.None;
			}

			foreach (UIElement child in Children)
			{
				_ = child.Input(ks, ms);
			}

			if (State != currentState)
			{
				OnStateChanged?.Invoke(State);
				UIManager.GlobalUIInteraction?.Invoke(State, currentState);
			}

			return UIManager.LeftMouseButtonCaptured;
		}
		public virtual void Update(float dt)
		{
			foreach (UIElement child in Children)
			{
				child.Update(dt);
			}
		}
		public virtual void Render(SpriteBatch sb)
		{
			foreach (UIElement child in Children)
			{
				child.Render(sb);
			}
		}
	}
}
