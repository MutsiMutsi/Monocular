using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocular.Enum;

namespace Monocular.UIElements
{
	public class Image : UIElement
	{
		public Texture2D Texture { get; set; }
		public int Size;
		private Rectangle sourceRectangle;


		public Image(Texture2D texture, int size, Align alignment = Align.TopLeft) : base(new Rect(-1, -1, -1, -1))
		{
			Texture = texture;
			Size = size;

			if (size == 0)
			{
				Rect.Width = texture.Width;
				Rect.Height = texture.Height;
			}
			else if (Size > 0)
			{
				Rect.Width = (int)(Texture.Width * (Size / (float)Texture.Height));
				Rect.Height = Size;
			}


			Alignment = alignment;
		}

		public Image(Rectangle sourceRect, Align alignment = Align.TopLeft) : base(new Rect(-1, -1, -1, -1))
		{
			this.sourceRectangle = sourceRect;
			Rect.Width = sourceRect.Width;
			Rect.Height = sourceRect.Height;
			Texture = UIManager.Texture;
			Alignment = alignment;
		}

		public override void Render(SpriteBatch sb)
		{
			var c = UIManager.Cursor.Peek();
			if (Size == -1)
			{
				int heightAvailable = c.Rect.Height - UIManager.Padding;
				Rect.Width = (int)(Texture.Width * (heightAvailable / (float)Texture.Height));
				Rect.Height = heightAvailable;
			}

			if (Alignment == Align.TopCenter)
			{
				int px = (c.Rect.X + c.Rect.Width) / 2 - (Rect.Width - UIManager.Padding) / 2;
				Rect.X = px;
			}
			if (Alignment == Align.TopRight)
			{
				int px = c.Rect.X + c.Rect.Width - Rect.Width - UIManager.Padding;
				Rect.X = px;
			}

			UpdateDrawRect();
			sb.Draw(Texture, DrawRectangle, sourceRectangle, Color);
			base.Render(sb);
		}
	}
}
