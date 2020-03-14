using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaJaMa.ScreenCapture
{
	public partial class frmCaptureVideo : Form
	{
		public event EventHandler VideoCaptured;
		private int _captureLeft { get; set; }
		private int _captureTop { get; set; }
		private int _captureWidth { get; set; }
		private int _captureHeight { get; set; }
		public List<Image> CapturedImages { get; set; }

		[StructLayout(LayoutKind.Sequential)]
		struct CURSORINFO
		{
			public Int32 cbSize;
			public Int32 flags;
			public IntPtr hCursor;
			public POINTAPI ptScreenPos;
		}

		[StructLayout(LayoutKind.Sequential)]
		struct POINTAPI
		{
			public int x;
			public int y;
		}

		[DllImport("user32.dll")]
		static extern bool GetCursorInfo(out CURSORINFO pci);

		[DllImport("user32.dll")]
		static extern bool DrawIcon(IntPtr hDC, int X, int Y, IntPtr hIcon);

		const Int32 CURSOR_SHOWING = 0x00000001;

		public frmCaptureVideo()
		{
			CapturedImages = new List<Image>();
			InitializeComponent();
		}

		private void BtnRecord_Click(object sender, EventArgs e)
		{
			timRecord.Enabled = true;
		}

		private void BtnPause_Click(object sender, EventArgs e)
		{
			timRecord.Enabled = false;
		}

		private void BtnStop_Click(object sender, EventArgs e)
		{
			timRecord.Enabled = false;
			VideoCaptured?.Invoke(this, new EventArgs());
		}

		public void SetCaptureBounds(Rectangle rect)
		{
			_captureLeft = rect.Left;
			_captureTop = rect.Top;
			_captureWidth = rect.Width;
			_captureHeight = rect.Height;
		}

		private void TimRecord_Tick(object sender, EventArgs e)
		{
			var bmp = new Bitmap(_captureWidth, _captureHeight);
			using (var graphics = Graphics.FromImage(bmp))
			{
				graphics.CopyFromScreen(new Point(_captureLeft, _captureTop), new Point(0, 0), new Size(_captureWidth, _captureHeight));
				CURSORINFO pci;
				pci.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(CURSORINFO));

				if (GetCursorInfo(out pci))
				{
					if (pci.flags == CURSOR_SHOWING)
					{
						DrawIcon(graphics.GetHdc(), pci.ptScreenPos.x, pci.ptScreenPos.y, pci.hCursor);
						graphics.ReleaseHdc();
					}
				}
			}
			CapturedImages.Add(bmp);
		}
	}
}
