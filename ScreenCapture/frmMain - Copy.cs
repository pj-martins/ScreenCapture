using PaJaMa.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaJaMa.ScreenCapture
{
	public partial class frmMain : Form
	{
		private List<frmOverlay> _overlays = new List<frmOverlay>();

		private DirectoryInfo _picturesDirectory;

		private int _downX = -1;
		private int _downY = -1;
		private int _imageSize = 128;

		private bool _lock = false;
		private GlobalHooks _hooks;
		private Process _paintProcess;

		public frmMain()
		{
			InitializeComponent();

			_lock = true;
			foreach (var val in Enum.GetValues(typeof(View)))
			{
				cboViewType.Items.Add(val);
			}


			cboViewType.SelectedItem = lstMain.View;

			lstMain.MouseWheel += lstMain_MouseWheel;

			_lock = false;

			refreshImages();
		}

		private void lstMain_MouseWheel(object sender, MouseEventArgs e)
		{
			if (ModifierKeys == Keys.Control)
			{
				int increase = (int)Math.Ceiling((double)e.Delta / 3.75);
				_imageSize += increase;
				if (_imageSize < 16)
					_imageSize = 16;

				if (_imageSize > 512)
					_imageSize = 512;

				imgList.ImageSize = new System.Drawing.Size(_imageSize, _imageSize);
				// refreshImages(true);
				((HandledMouseEventArgs)e).Handled = true;
			}
		}

		private void refreshImages(bool noResize = false)
		{
			lstMain.Items.Clear();
			imgList.Images.Clear();

			if (!noResize)
			{
				_imageSize = 128;

				switch (lstMain.View)
				{
					case View.Details:
					case View.List:
						_imageSize = 64;
						break;
					case View.Tile:
					case View.SmallIcon:
						_imageSize = 128;
						break;
					case View.LargeIcon:
						_imageSize = 256;
						break;
				}
			}

			imgList.ImageSize = new System.Drawing.Size(_imageSize, _imageSize);

			_picturesDirectory = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PJCapture", "Images"));
			if (!_picturesDirectory.Exists)
				_picturesDirectory.Create();

			int i = 0;
			foreach (var file in _picturesDirectory.GetFiles())
			{
				using (var original = Image.FromFile(file.FullName))
					imgList.Images.Add(Imaging.ResizeImage(original, imgList.ImageSize));
				lstMain.Items.Add(new ListViewItem(file.Name, i) { Tag = file.FullName });
				i++;
			}
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			_hooks = new GlobalHooks();
			_hooks.KeyDown += _hooks_KeyDown;
		}

		private void _hooks_MouseMove(object sender, MouseEventArgs e)
		{
			if (_overlays.Any())
			{
				bool mouseDown = _downX >= 0 && _downY >= 0;
				foreach (var overlay in _overlays)
				{
					if (overlay.IsInControl(e.X, e.Y) || mouseDown)
					{
						overlay.Draw(e.X, e.Y, _downX, _downY, mouseDown);
					}
					else
						overlay.ClearGraphics();
				}
			}
		}

		private void _hooks_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.PrintScreen)
			{
				Cursor.Hide();
				_hooks.MouseMove += _hooks_MouseMove;
				_hooks.LeftMouseDown += _hooks_LeftMouseDown;
				_hooks.LeftMouseUp += _hooks_LeftMouseUp;
				foreach (var screen in Screen.AllScreens)
				{
					var overlay = new frmOverlay();
					overlay.SetBounds(screen.Bounds.X, screen.Bounds.Y, screen.Bounds.Width, screen.Bounds.Height);
					overlay.ScreenX = screen.Bounds.X;
					overlay.ScreenY = screen.Bounds.Y;
					overlay.StartPosition = FormStartPosition.Manual;
					overlay.Show();
					_overlays.Add(overlay);
				}
			}
			else if (e.KeyCode == Keys.Escape && _overlays.Any())
			{
				closeAll();
			}
		}

		private void _hooks_LeftMouseUp(object sender, MouseEventArgs e)
		{
			if (_overlays.Any())
			{
				string imagePath = string.Empty;

				foreach (var overlay in _overlays)
				{
					if (overlay.IsInControl(e.X, e.Y))
					{
						var image = overlay.GetImage();
						imagePath = Path.Combine(_picturesDirectory.FullName, DateTime.Now.ToString("yyyyMMddHHmmss") + ".png");
						image.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
						Clipboard.SetImage(image);
						imgList.Images.Add(Imaging.ResizeImage(image, imgList.ImageSize));
						lstMain.Items.Add(new ListViewItem(Path.GetFileName(imagePath), imgList.Images.Count - 1) { Tag = imagePath });
						this.WindowState = FormWindowState.Normal;
						break;
					}
				}

				closeAll();

				if (!string.IsNullOrEmpty(imagePath))
				{
					if (_paintProcess != null)
					{
						//_paintProcess.CloseMainWindow();
						_paintProcess.Dispose();
						_paintProcess = null;
					}
					_paintProcess = Process.Start("mspaint.exe", imagePath);
				}
			}

			_downX = -1;
			_downY = -1;
		}

		private void _hooks_LeftMouseDown(object sender, MouseEventArgs e)
		{
			_downX = e.X;
			_downY = e.Y;
		}

		private void closeAll()
		{
			foreach (var overlay in _overlays)
			{
				overlay.Close();
				overlay.Dispose();
			}

			_overlays.Clear();
			_hooks.MouseMove -= _hooks_MouseMove;
			_hooks.LeftMouseDown -= _hooks_LeftMouseDown;
			_hooks.LeftMouseUp -= _hooks_LeftMouseUp;


			Cursor.Show();
		}

		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_lock) return;

			this.WindowState = FormWindowState.Minimized;
			e.Cancel = true;
		}

		private void frmMain_Resize(object sender, EventArgs e)
		{
			ShowInTaskbar = WindowState != FormWindowState.Minimized;
		}

		private void lstMain_DoubleClick(object sender, EventArgs e)
		{
			if (lstMain.SelectedItems.Count < 1)
				return;

			foreach (ListViewItem img in lstMain.SelectedItems)
			{
				Process.Start("mspaint.exe", img.Tag.ToString());
			}
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var img = lstMain.SelectedItems[0];
			Clipboard.SetImage(Image.FromFile(img.Tag.ToString()));
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to delete sected images?", "Delete", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
				return;

			var faileds = new List<string>();
			var items = lstMain.SelectedItems.OfType<ListViewItem>().OrderByDescending(lvi => lvi.ImageIndex).ToList();
			foreach (var item in items)
			{
				string file = item.Tag.ToString();
				try
				{
					File.Delete(file);
				}
				catch (Exception ex)
				{
					faileds.Add("Failed to delete " + file + ": " + ex.Message);
					continue;
				}

				var img = imgList.Images[item.ImageIndex];
				lstMain.Items.Remove(item);
				imgList.Images.RemoveAt(item.ImageIndex);
				img.Dispose();
			}

			if (faileds.Any())
			{
				MessageBox.Show(string.Join("\r\n", faileds.ToArray()));
			}

			int i = 0;
			foreach (ListViewItem img in lstMain.Items)
			{
				img.ImageIndex = i++;
			}
		}

		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{
			if (lstMain.SelectedItems.Count < 1) e.Cancel = true;
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			lstMain_DoubleClick(sender, e);
		}

		private void lstMain_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
				deleteToolStripMenuItem_Click(sender, e);
			else if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
			{
				foreach (ListViewItem item in lstMain.Items)
				{
					item.Selected = true;
				}
			}
			else if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
			{
				copyToolStripMenuItem_Click(sender, e);
			}
		}

		private void notifyIcon1_DoubleClick(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Normal;
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			exit();
		}

		private void exit()
		{
			_hooks.Dispose();
			_lock = true;
			this.Close();
		}

		private void cboViewType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_lock) return;

			lstMain.View = (View)cboViewType.SelectedItem;
			refreshImages();
		}
	}
}
