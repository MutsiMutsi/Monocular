using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monocular.Enum;
using Monocular.UIElements;

namespace Monocular
{
	public class UIManager
	{
		public UIElement Stage;
		public static Texture2D Texture;
		public static FontSystem FontSystem;

		public static Stack<Cursor> Cursor = [];
		public static int Padding = 8;

		public static bool LeftMouseButtonCaptured = false;

		public static Action<InteractionState, InteractionState>? GlobalUIInteraction = null;

		public void LoadContent(GraphicsDevice gd, ContentManager cm)
		{
			Stage = new UIElement(new(0, 0, gd.PresentationParameters.BackBufferWidth, gd.PresentationParameters.BackBufferHeight));
			Texture = cm.Load<Texture2D>("ui");

			FontSystem = new FontSystem();
			FontSystemDefaults.FontResolutionFactor = 1f;
			FontSystemDefaults.KernelWidth = 0;
			FontSystemDefaults.KernelHeight = 0;

			FontSystem.AddFont(File.ReadAllBytes(@"Content\fonts\ProggyCleanSZ.ttf"));
		}

		public bool Input(KeyboardState ks, MouseState ms)
		{
			if (LeftMouseButtonCaptured && ms.LeftButton == ButtonState.Released)
			{
				LeftMouseButtonCaptured = false;
			}
			return Stage.Input(ks, ms);
		}

		public void Update(float dt)
		{
			Stage.Update(dt);
		}

		public void Render(SpriteBatch sb)
		{
			Cursor.Clear();

			Cursor c = new Cursor(new(Stage.Rect.X, Stage.Rect.Y, Stage.Rect.Width, Stage.Rect.Height));
			c.PosX = Padding;
			c.PosY = Padding;
			Cursor.Push(c);

			var rasterizer = new RasterizerState();
			rasterizer.ScissorTestEnable = true;
			sb.Begin(SpriteSortMode.Immediate, blendState: BlendState.NonPremultiplied, samplerState: SamplerState.PointClamp, rasterizerState: rasterizer);
			Stage.Render(sb);
			sb.End();
		}

		public static void IncrementCursor(int width)
		{
			var current = Cursor.Pop();
			current.PosX += width + Padding;
			Cursor.Push(current);
		}

		public static void NewLineCursor()
		{
			var current = Cursor.Pop();
			current.PosX = Padding;
			current.PosY += current.LineMax + Padding;
			current.LineMax = 0;
			Cursor.Push(current);
		}

		public static void PushCursor(Rectangle rect)
		{
			Cursor cursor = new Cursor(new Rect(rect.X, rect.Y, rect.Width, rect.Height));
			cursor.PosX = Padding;
			cursor.PosY = Padding;
			Cursor.Push(cursor);
		}

		public static void PopCursor()
		{
			Cursor.Pop();
		}

		public static Cursor PeekCursor()
		{
			return Cursor.Peek();
		}

		public static void UpdateLineMax(int height)
		{
			if (Cursor.Count > 0)
			{
				var current = Cursor.Pop();
				current.LineMax = Math.Max(current.LineMax, height);
				Cursor.Push(current);
			}
		}
		public static Cursor GetCursor(int width)
		{
			width = Math.Max(Padding * 2, width);

			var c = Cursor.Peek();
			if (c.PosX + width + Padding > c.Rect.Width)
			{
				NewLineCursor();
				c = Cursor.Peek();
			}
			return c;
		}
	}
}
