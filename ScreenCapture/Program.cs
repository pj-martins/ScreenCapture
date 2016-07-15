﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaJaMa.Common;

namespace PaJaMa.ScreenCapture
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.ThreadException += Application_ThreadException;
			Application.Run(new frmMain());
		}

		static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			System.Windows.Forms.MessageBox.Show(e.Exception.GetFullExceptionTextWithStackTrace());
		}
	}
}
