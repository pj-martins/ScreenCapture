namespace PaJaMa.ScreenCapture
{
	partial class frmCaptureVideo
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.btnRecord = new System.Windows.Forms.Button();
			this.btnPause = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.timRecord = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// btnRecord
			// 
			this.btnRecord.Enabled = false;
			this.btnRecord.Location = new System.Drawing.Point(12, 12);
			this.btnRecord.Name = "btnRecord";
			this.btnRecord.Size = new System.Drawing.Size(75, 23);
			this.btnRecord.TabIndex = 0;
			this.btnRecord.Text = "Record";
			this.btnRecord.UseVisualStyleBackColor = true;
			this.btnRecord.Click += new System.EventHandler(this.BtnRecord_Click);
			// 
			// btnPause
			// 
			this.btnPause.Location = new System.Drawing.Point(93, 12);
			this.btnPause.Name = "btnPause";
			this.btnPause.Size = new System.Drawing.Size(75, 23);
			this.btnPause.TabIndex = 1;
			this.btnPause.Text = "Pause";
			this.btnPause.UseVisualStyleBackColor = true;
			this.btnPause.Click += new System.EventHandler(this.BtnPause_Click);
			// 
			// btnStop
			// 
			this.btnStop.Location = new System.Drawing.Point(174, 12);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(75, 23);
			this.btnStop.TabIndex = 2;
			this.btnStop.Text = "Stop";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
			// 
			// timRecord
			// 
			this.timRecord.Enabled = true;
			this.timRecord.Interval = 40;
			this.timRecord.Tick += new System.EventHandler(this.TimRecord_Tick);
			// 
			// frmCaptureVideo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(256, 49);
			this.ControlBox = false;
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.btnPause);
			this.Controls.Add(this.btnRecord);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "frmCaptureVideo";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Capture";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnRecord;
		private System.Windows.Forms.Button btnPause;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Timer timRecord;
	}
}