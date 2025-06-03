using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monocular.Enum;
using Monocular.Util;
using System.Text;

namespace Monocular.UIElements
{
	public class Text : UIElement
	{
		private StringBuilder value = new StringBuilder();
		private string displayValue;
		private int scroll;

		private Vector2 stringSize;
		private DynamicSpriteFont dynamicFont;

		private Rectangle scissorRect = new Rectangle();

		public Text(int size = 13, Align alignment = Align.TopLeft, LineBreak lineBreak = LineBreak.None) : base(new Rect(-1, -1, -1, -1), alignment)
		{
			dynamicFont = UIManager.FontSystem.GetFont(size);
			Rect.Width = (int)stringSize.X + 1;
			Rect.Height = (int)stringSize.Y + 1;
			Alignment = alignment;
			LineBreak = lineBreak;
		}

		public void Append(string val)
		{
			value.Append(val);
			onAppend();
		}

		public void Append(char ch)
		{
			value.Append(ch);
			onAppend();
		}

		private void onAppend()
		{
			StringBuilder displaysb = new StringBuilder();

			displayValue = Util.StringUtils.LineBreakText(value.ToString(), dynamicFont, scissorRect.Width - 8, true);
			Rect.Height = (int)dynamicFont.MeasureString(displayValue).Y;

			scroll = Rect.Height - scissorRect.Height + 32;
			if (scroll < 0)
			{
				scroll = 0;
			}
		}

		public override void Render(SpriteBatch sb)
		{
			//PUSH A NEW FRESH CURSOR FOR CONTENTS
			//var oldRect = sb.GraphicsDevice.ScissorRectangle;
			var cursor = UIManager.Cursor.Peek();
			//sb.GraphicsDevice.ScissorRectangle = new Rectangle(cursor.Rect.X, cursor.Rect.Y + 24, cursor.Rect.Width, cursor.Rect.Height - dynamicFont.LineHeight + 8);
			//scissorRect = sb.GraphicsDevice.ScissorRectangle;

			UpdateDrawRect();
			base.Render(sb);
		}

		public override void RenderText(SpriteBatch sb)
		{
			_ = sb.DrawString(dynamicFont, displayValue, DrawRectangle.Location.ToVector2() - new Vector2(0, scroll), Color, effect: FontSystemEffect.Stroked, textStyle: TextStyle.None, effectAmount: 1);
			//sb.GraphicsDevice.ScissorRectangle = oldRect;
			base.RenderText(sb);
		}

		public override bool Input(KeyboardState ks, MouseInfo ms)
		{
			return false;
		}
	}
}
