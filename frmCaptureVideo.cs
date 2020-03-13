using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
			}
			CapturedImages.Add(bmp);
		}
	}
}
