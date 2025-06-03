using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocular.Enum;

namespace Monocular.UIElements
{
	public class Select : UIElement
	{
		public string[] Values = [];
		public Action<string>? OnValueChanged = null;

		private readonly Label valueLabel;
		private readonly Image toggleLeft;
		private readonly Image toggleRight;

		private int index = 0;

		public Select(Rect rect, string[] values, int defaultValue = 0, int fontSize = 13, Align alignment = Align.TopLeft) : base(rect, alignment)
		{
			index = defaultValue;
			Values = values;
			valueLabel = new(Values[index], fontSize){ Color = Color};

			toggleLeft = new(new Rectangle(0, 24, 8, fontSize));
			toggleLeft.OnStateChanged = (s) =>
			{
				switch (s)
				{
					case InteractionState.None:
						toggleLeft.Color = Color.White;
						break;
					case InteractionState.Hover:
						toggleLeft.Color = Color.DarkCyan;
						break;
					case InteractionState.Active:
						toggleLeft.Color = Color.Gold;
						break;
					default:
						break;
				}
			};

			toggleRight = new(new Rectangle(8, 24, 8, fontSize));
			toggleRight.OnStateChanged = (s) =>
			{
				switch (s)
				{
					case InteractionState.None:
						toggleRight.Color = Color.White;
						break;
					case InteractionState.Hover:
						toggleRight.Color = Color.DarkCyan;
						break;
					case InteractionState.Active:
						toggleRight.Color = Color.Gold;
						break;
					default:
						break;
				}
			};

			var updateValue = () =>
			{
				valueLabel.Text.Clear();
				valueLabel.Text.Append(Values[index]);
				valueLabel.Flush();
				OnValueChanged?.Invoke(Values[index]);
			};

			toggleLeft.OnClick = () =>
			{
				if (index > 0)
				{
					index--;
					updateValue();
				}
			};
			toggleRight.OnClick = () =>
			{
				if (index < Values.Length - 1)
				{
					index++;
					updateValue();
				}
			};

			toggleLeft.Alignment = Align.TopLeft;
			valueLabel.Alignment = Align.TopCenter;
			toggleRight.Alignment = Align.TopRight;

			Children.Add(toggleLeft);
			Children.Add(valueLabel);
			Children.Add(toggleRight);
		}

		public override void Render(SpriteBatch sb)
		{
			//UpdateDrawRect();
			Cursor c = UIManager.Cursor.Pop();
			c.Rect.Y += c.PosY;
			c.Rect.Height -= c.PosY;
			c.PosY = 0;
			UIManager.Cursor.Push(c);

			//sb.Draw(UIManager.Texture, new Rectangle(c.Rect.X, c.Rect.Y, c.Rect.Width, c.Rect.Height), new Rectangle(32, 8, 1, 1), Color.White);

			base.Render(sb);
			UIManager.NewLineCursor();
		}
	}
}
