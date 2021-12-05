using System;
using System.Runtime.InteropServices;
using System.Threading;
using Winapi.Flags;
using Winapi.Structures;

namespace Winapi
{
	public static unsafe class Functions
	{
		[DllImport("kernel32.dll")]
		public static extern uint GetLastError();

		public static void PrintErrorIfExists()
		{
			uint err = GetLastError();
			if(err!=0)
				Console.WriteLine($"Some error occured. Error code {err}");
		}

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr CreateSemaphore(IntPtr lpSemaphoreAttributes, int lInitialCount, int lMaximumCount, string lpName);
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr OpenSemaphore(uint dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, string lpName);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr CreateMutex(IntPtr lpMutexAttributes, bool bInitialOwner, string lpName);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr OpenMutex(uint dwDesiredAccess, bool bInheritHandle, string lpName);

		[DllImport("kernel32.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		public static extern IntPtr CreateFile([MarshalAs(UnmanagedType.LPUTF8Str)] string fileName,
									  uint desiredAccess, uint shareMode, SECURITY_ATTRIBUTES securityAttributes,
									  uint creationDisposition, uint flagsAndAttributes, IntPtr templateFile);
		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern IntPtr CreateFileMapping(
		   IntPtr hFile,
		   SECURITY_ATTRIBUTES lpFileMappingAttributes,
		   FileMapProtection flProtect,
		   uint dwMaximumSizeHigh,
		   uint dwMaximumSizeLow,
		   [MarshalAs(UnmanagedType.LPStr)] string lpName);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr MapViewOfFile(
			IntPtr hFileMappingObject,
			uint dwDesiredAccess,
			uint dwFileOffsetHigh,
			uint dwFileOffsetLow,
			UIntPtr dwNumberOfBytesToMap);

		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern IntPtr OpenFileMapping(uint dwDesiredAccess, bool bInheritHandle, [MarshalAs(UnmanagedType.LPStr)] string lpName);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

		[DllImport("kernel32.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		public static extern bool CloseHandle(IntPtr hObject);
		
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool VirtualLock(IntPtr hObject, UIntPtr dwSize);

		[DllImport("kernel32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool CreateProcess(
			string lpApplicationName,
			string lpCommandLine,
			IntPtr lpProcessAttributes,
			IntPtr lpThreadAttributes,
			bool bInheritHandles,
			uint dwCreationFlags,
			IntPtr lpEnvironment,
			string lpCurrentDirectory,
			[In] ref StartupInfo lpStartupInfo,
			out ProcessInfo lpProcessInformation
		);
		[DllImport("Kernel32.dll", EntryPoint = "RtlZeroMemory", SetLastError = true)]
		static extern void ZeroMemory(IntPtr dest, IntPtr size);

		[DllImport("kernel32.dll", EntryPoint = "WaitForMultipleObjects", SetLastError = true)]
		public static extern int WaitForMultipleObjects(uint nCount, IntPtr[] lpHandles, bool fWaitAll, uint dwMilliseconds);
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr GetStdHandle(uint nStdHandle);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WriteFile(IntPtr hFile, byte[] lpBuffer,
		   uint nNumberOfBytesToWrite, out uint lpNumberOfBytesWritten,
		   [In] NativeOverlapped* lpOverlapped);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		private static extern bool ReadFileEx(
			IntPtr hFile,
			[Out] byte[] buffer,
			[In] uint numberOfBytesToRead,
			[In, Out] ref NativeOverlapped overlapped,
			[MarshalAs(UnmanagedType.FunctionPtr)] IOCompletionCallback completionRoutine);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool WriteFileEx(IntPtr hFile,
			[MarshalAs(UnmanagedType.LPArray)] byte[] buffer,
			uint numberOfBytesToWrite,
			[In] ref NativeOverlapped overlapped,
			[MarshalAs(UnmanagedType.FunctionPtr)] IOCompletionCallback completionRoutine);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern uint timeGetTime();

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern int SleepEx(
			uint dwMilliseconds,
			bool bAlertable);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool ReleaseMutex(IntPtr hMutex);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool ReleaseSemaphore(IntPtr hSemaphore, int lReleaseCount, out int lpPreviousCount);
	}
}
