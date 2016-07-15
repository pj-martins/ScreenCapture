﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PaJaMa.Common
{
	public class GlobalVariables
	{

	}
	public class Folders
	{
		[DllImport("shell32.dll")]
		static extern bool SHGetSpecialFolderPath(IntPtr hwndOwner,
		   [Out] StringBuilder lpszPath, int nFolder, bool fCreate);


		const int CSIDL_DESKTOP = 0x0000;        // <desktop>
		const int CSIDL_INTERNET = 0x0001;        // Internet Explorer (icon on desktop)
		const int CSIDL_PROGRAMS = 0x0002;        // Start Menu\Programs
		const int CSIDL_CONTROLS = 0x0003;        // My Computer\Control Panel
		const int CSIDL_PRINTERS = 0x0004;        // My Computer\Printers
		const int CSIDL_PERSONAL = 0x0005;        // My Documents
		const int CSIDL_FAVORITES = 0x0006;        // <user name>\Favorites
		const int CSIDL_STARTUP = 0x0007;        // Start Menu\Programs\Startup
		const int CSIDL_RECENT = 0x0008;        // <user name>\Recent
		const int CSIDL_SENDTO = 0x0009;        // <user name>\SendTo
		const int CSIDL_BITBUCKET = 0x000a;        // <desktop>\Recycle Bin
		const int CSIDL_STARTMENU = 0x000b;        // <user name>\Start Menu
		const int CSIDL_MYDOCUMENTS = CSIDL_PERSONAL; //  Personal was just a silly name for My Documents
		const int CSIDL_MYMUSIC = 0x000d;        // "My Music" folder
		const int CSIDL_MYVIDEO = 0x000e;        // "My Videos" folder
		const int CSIDL_DESKTOPDIRECTORY = 0x0010;        // <user name>\Desktop
		const int CSIDL_DRIVES = 0x0011;        // My Computer
		const int CSIDL_NETWORK = 0x0012;        // Network Neighborhood (My Network Places)
		const int CSIDL_NETHOOD = 0x0013;        // <user name>\nethood
		const int CSIDL_FONTS = 0x0014;        // windows\fonts
		const int CSIDL_TEMPLATES = 0x0015;
		const int CSIDL_COMMON_STARTMENU = 0x0016;        // All Users\Start Menu
		const int CSIDL_COMMON_PROGRAMS = 0x0017;        // All Users\Start Menu\Programs
		const int CSIDL_COMMON_STARTUP = 0x0018;        // All Users\Startup
		const int CSIDL_COMMON_DESKTOPDIRECTORY = 0x0019;        // All Users\Desktop
		const int CSIDL_APPDATA = 0x001a;        // <user name>\Application Data
		const int CSIDL_PRINTHOOD = 0x001b;        // <user name>\PrintHood
		const int CSIDL_LOCAL_APPDATA = 0x001c;        // <user name>\Local Settings\Applicaiton Data (non roaming)
		const int CSIDL_ALTSTARTUP = 0x001d;        // non localized startup
		const int CSIDL_COMMON_ALTSTARTUP = 0x001e;        // non localized common startup
		const int CSIDL_COMMON_FAVORITES = 0x001f;
		const int CSIDL_INTERNET_CACHE = 0x0020;
		const int CSIDL_COOKIES = 0x0021;
		const int CSIDL_HISTORY = 0x0022;
		const int CSIDL_COMMON_APPDATA = 0x0023;        // All Users\Application Data
		const int CSIDL_WINDOWS = 0x0024;        // GetWindowsDirectory()
		const int CSIDL_SYSTEM = 0x0025;        // GetSystemDirectory()
		const int CSIDL_PROGRAM_FILES = 0x0026;        // C:\Program Files
		const int CSIDL_MYPICTURES = 0x0027;        // C:\Program Files\My Pictures
		const int CSIDL_PROFILE = 0x0028;        // USERPROFILE
		const int CSIDL_SYSTEMX86 = 0x0029;        // x86 system directory on RISC
		const int CSIDL_PROGRAM_FILESX86 = 0x002a;        // x86 C:\Program Files on RISC
		const int CSIDL_PROGRAM_FILES_COMMON = 0x002b;        // C:\Program Files\Common
		const int CSIDL_PROGRAM_FILES_COMMONX86 = 0x002c;        // x86 Program Files\Common on RISC
		const int CSIDL_COMMON_TEMPLATES = 0x002d;        // All Users\Templates
		const int CSIDL_COMMON_DOCUMENTS = 0x002e;        // All Users\Documents
		const int CSIDL_COMMON_ADMINTOOLS = 0x002f;        // All Users\Start Menu\Programs\Administrative Tools
		const int CSIDL_ADMINTOOLS = 0x0030;        // <user name>\Start Menu\Programs\Administrative Tools
		const int CSIDL_CONNECTIONS = 0x0031;        // Network and Dial-up Connections
		const int CSIDL_COMMON_MUSIC = 0x0035;        // All Users\My Music
		const int CSIDL_COMMON_PICTURES = 0x0036;        // All Users\My Pictures
		const int CSIDL_COMMON_VIDEO = 0x0037;        // All Users\My Video
		const int CSIDL_RESOURCES = 0x0038;        // Resource Direcotry
		const int CSIDL_RESOURCES_LOCALIZED = 0x0039;        // Localized Resource Direcotry
		const int CSIDL_COMMON_OEM_LINKS = 0x003a;        // Links to All Users OEM specific apps
		const int CSIDL_CDBURN_AREA = 0x003b;        // USERPROFILE\Local Settings\Application Data\Microsoft\CD Burning
		const int CSIDL_COMPUTERSNEARME = 0x003d;        // Computers Near Me (computered from Workgroup membership)
		const int CSIDL_FLAG_CREATE = 0x8000;        // combine with CSIDL_ value to force folder creation in SHGetFolderPath()
		const int CSIDL_FLAG_DONT_VERIFY = 0x4000;        // combine with CSIDL_ value to return an unverified folder path
		const int CSIDL_FLAG_DONT_UNEXPAND = 0x2000;        // combine with CSIDL_ value to avoid unexpanding environment variables
		const int CSIDL_FLAG_NO_ALIAS = 0x1000;        // combine with CSIDL_ value to insure non-alias versions of the pidl
		const int CSIDL_FLAG_PER_USER_INIT = 0x0800;        // combine with CSIDL_ value to indicate per-user init (eg. upgrade)

		public static string AppDataPath
		{
			get { return GetCommonPath(CSIDL_COMMON_APPDATA); }
		}

		public static string DesktopPath
		{
			get { return GetCommonPath(CSIDL_COMMON_DESKTOPDIRECTORY); }
		}

		public static string GetCommonPath(int specialFolder)
		{
			StringBuilder path = new StringBuilder(260);
			SHGetSpecialFolderPath(IntPtr.Zero, path, specialFolder, false);
			return path.ToString();
		}
	}
}
