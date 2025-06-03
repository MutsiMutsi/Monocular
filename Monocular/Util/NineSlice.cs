using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocular.UIElements;

namespace Monocular.Util
{
	public struct NineSlice
	{
		public Vector2 AtlasPosition { get; set; }
		public int SliceSize { get; set; }
		public int TotalSize { get; set; }

		public NineSlice(Rectangle sliceRect)
		{
			AtlasPosition = sliceRect.Location.ToVector2();
			SliceSize = sliceRect.Width / 3;
			TotalSize = sliceRect.Width;
		}

		public void Draw(SpriteBatch spriteBatch, Texture2D texture, Rectangle drawRectangle, Color tintColor)
		{
			int atlasX = (int)AtlasPosition.X;
			int atlasY = (int)AtlasPosition.Y;

			// Clamp slice sizes when rectangle is too small
			int effectiveSliceWidth = Math.Min(SliceSize, drawRectangle.Width / 2);
			int effectiveSliceHeight = Math.Min(SliceSize, drawRectangle.Height / 2);

			int left = drawRectangle.X;
			int right = drawRectangle.X + drawRectangle.Width - effectiveSliceWidth;
			int top = drawRectangle.Y;
			int bottom = drawRectangle.Y + drawRectangle.Height - effectiveSliceHeight;

			// Fix edge case for very small rectangles
			right = Math.Max(left, right);
			bottom = Math.Max(top, bottom);

			int middleWidth = drawRectangle.Width - (effectiveSliceWidth * 2);
			int middleHeight = drawRectangle.Height - (effectiveSliceHeight * 2);

			// Corners
			// Top-left
			spriteBatch.Draw(texture,
				new Vector2(left, top),
				new Rectangle(atlasX, atlasY, effectiveSliceWidth, effectiveSliceHeight),
				tintColor);

			// Top-right
			spriteBatch.Draw(texture,
				new Vector2(right, top),
				new Rectangle(atlasX + TotalSize - SliceSize + (SliceSize - effectiveSliceWidth), atlasY, effectiveSliceWidth, effectiveSliceHeight),
				tintColor);

			// Bottom-left
			spriteBatch.Draw(texture,
				new Vector2(left, bottom),
				new Rectangle(atlasX, atlasY + TotalSize - SliceSize, effectiveSliceWidth, effectiveSliceHeight),
				tintColor);

			// Bottom-right
			spriteBatch.Draw(texture,
				new Vector2(right, bottom),
				new Rectangle(atlasX + TotalSize - SliceSize + (SliceSize - effectiveSliceWidth), atlasY + TotalSize - SliceSize, effectiveSliceWidth, effectiveSliceHeight),
				tintColor);

			// Edges
			// Top edge
			if (middleWidth > 0)
			{
				spriteBatch.Draw(texture,
					new Rectangle(left + effectiveSliceWidth, top, middleWidth, effectiveSliceHeight),
					new Rectangle(atlasX + SliceSize, atlasY, TotalSize - (SliceSize * 2), SliceSize),
					tintColor);
			}

			// Bottom edge
			if (middleWidth > 0)
			{
				spriteBatch.Draw(texture,
					new Rectangle(left + effectiveSliceWidth, bottom, middleWidth, effectiveSliceHeight),
					new Rectangle(atlasX + SliceSize, atlasY + TotalSize - SliceSize, TotalSize - (SliceSize * 2), SliceSize),
					tintColor);
			}

			// Left edge
			if (middleHeight > 0)
			{
				spriteBatch.Draw(texture,
					new Rectangle(left, top + effectiveSliceHeight, effectiveSliceWidth, middleHeight),
					new Rectangle(atlasX, atlasY + SliceSize, SliceSize, TotalSize - (SliceSize * 2)),
					tintColor);
			}

			// Right edge
			if (middleHeight > 0)
			{
				spriteBatch.Draw(texture,
					new Rectangle(right, top + effectiveSliceHeight, effectiveSliceWidth, middleHeight),
					new Rectangle(atlasX + TotalSize - SliceSize, atlasY + SliceSize, SliceSize, TotalSize - (SliceSize * 2)),
					tintColor);
			}

			// Center
			if (middleWidth > 0 && middleHeight > 0)
			{
				spriteBatch.Draw(texture,
					new Rectangle(left + effectiveSliceWidth, top + effectiveSliceHeight, middleWidth, middleHeight),
					new Rectangle(atlasX + SliceSize, atlasY + SliceSize, TotalSize - (SliceSize * 2), TotalSize - (SliceSize * 2)),
					tintColor);
			}
		}
	}
}
