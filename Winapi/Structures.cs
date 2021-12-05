using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Winapi.Structures
{
	[StructLayout(LayoutKind.Sequential)]
	public class SECURITY_ATTRIBUTES
	{
		public uint length;
		public IntPtr securityDescriptor;
		public bool inheritHandle;
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public struct ProcessInfo
	{
		public IntPtr hProcess;
		public IntPtr hThread;
		public int ProcessId;
		public int ThreadId;
	}

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct StartupInfo
    {
        public uint cb;
        public string lpReserved;
        public string lpDesktop;
        public string lpTitle;
        public uint dwX;
        public uint dwY;
        public uint dwXSize;
        public uint dwYSize;
        public uint dwXCountChars;
        public uint dwYCountChars;
        public uint dwFillAttribute;
        public uint dwFlags;
        public short wShowWindow;
        public short cbReserved2;
        public IntPtr lpReserved2;
        public IntPtr hStdInput;
        public IntPtr hStdOutput;
        public IntPtr hStdError;
    }

}
