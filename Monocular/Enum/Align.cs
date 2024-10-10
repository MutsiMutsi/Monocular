namespace Monocular.Enum
{
	[Flags]
	public enum Align
	{
		TopLeft = 0x001,
		TopCenter = 0x002,
		TopRight = 0x004,
		MiddleLeft = 0x010,
		MiddleCenter = 0x020,
		MiddleRight = 0x040,
		BottomLeft = 0x100,
		BottomCenter = 0x200,
		BottomRight = 0x400,
	}

	public static class AlignHelper
	{
		public static readonly Align TopMask = Align.TopLeft | Align.TopCenter | Align.TopRight;
		public static readonly Align MiddleMask = Align.MiddleLeft | Align.MiddleCenter | Align.MiddleRight;
		public static readonly Align BottomMask = Align.BottomLeft | Align.BottomCenter | Align.BottomRight;

		public static readonly Align LeftMask = Align.TopLeft | Align.MiddleLeft | Align.BottomLeft;
		public static readonly Align CenterMask = Align.TopCenter | Align.MiddleCenter | Align.BottomCenter;
		public static readonly Align RightMask = Align.TopRight | Align.MiddleRight | Align.BottomRight;
	}
}
