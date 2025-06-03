using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monocular.Enum;
using Monocular.Util;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Monocular.UIElements
{
	public class Label : UIElement
	{
		public StringBuilder Text;
		private Vector2 stringSize;
		private DynamicSpriteFont dynamicFont;

		public Label(string text, int size = 13, Align alignment = Align.TopLeft, LineBreak lineBreak = LineBreak.None) : base(new Rect(-1, -1, -1, -1), alignment)
		{
			Text = new StringBuilder();
			Text.Append(text);
			dynamicFont = UIManager.FontSystem.GetFont(size);

			Flush();
			Alignment = alignment;
			LineBreak = lineBreak;
		}

		public void Set(StringBuilder sb)
		{
			Text.Clear();
			Text.Append(sb);
			Flush();
		}

		public void Set(string val)
		{
			Text.Clear();
			Text.Append(val);
			Flush();
		}

		public void Flush()
		{
			stringSize = dynamicFont.MeasureString(Text);
			Rect.Width = (int)stringSize.X + 1;
			Rect.Height = (int)stringSize.Y + 1;
		}

		public override void Render(SpriteBatch sb)
		{
			UpdateDrawRect();
			base.Render(sb);
		}

		public override void RenderText(SpriteBatch sb)
		{
			_ = sb.DrawString(dynamicFont, Text, DrawRectangle.Location.ToVector2(), Color, effect: FontSystemEffect.Stroked, textStyle: TextStyle.None, effectAmount: 1);
			base.RenderText(sb);
		}

		public override bool Input(KeyboardState ks, MouseInfo ms)
		{
			return false;
		}
	}
}
