using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monocular.Enum;

namespace Monocular.UIElements
{
	public class Label : UIElement
	{
		public string Text
		{
			get { return text; }
			set
			{
				text = value;
				stringSize = dynamicFont.MeasureString(text);
				Rect.Width = (int)stringSize.X + 1;
				Rect.Height = (int)stringSize.Y + 1;
			}
		}
		private string text;
		private Vector2 stringSize;
		private DynamicSpriteFont dynamicFont;

		public Label(string text, int size = 13, Align alignment = Align.TopLeft, LineBreak lineBreak = LineBreak.None) : base(new Rect(-1, -1, -1, -1), alignment)
		{
			this.text = text;
			dynamicFont = UIManager.FontSystem.GetFont(size);
			stringSize = dynamicFont.MeasureString(text);
			Rect.Width = (int)stringSize.X + 1;
			Rect.Height = (int)stringSize.Y + 1;
			Alignment = alignment;
			LineBreak = lineBreak;
		}

		public override void Render(SpriteBatch sb)
		{
			UpdateDrawRect();
			_ = sb.DrawString(dynamicFont, Text, DrawRectangle.Location.ToVector2(), Color, effect: FontSystemEffect.Stroked, textStyle: TextStyle.None, effectAmount: 1);
			base.Render(sb);
		}

		public override bool Input(KeyboardState ks, MouseState ms)
		{
			return false;
		}
	}
}
