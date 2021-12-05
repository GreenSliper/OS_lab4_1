using System;
using System.Collections.Generic;
using Winapi.Flags;
using Winapi.Structures;
using static Winapi.Functions;

namespace OS_lab4_1
{
	class Starter
	{

		static IntPtr StartRWProcess(string executableFile, string targetFileName)
		{
			StartupInfo startupInfo = new StartupInfo();
			SECURITY_ATTRIBUTES securAttr = new SECURITY_ATTRIBUTES()
			{
				length = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(SECURITY_ATTRIBUTES)),
				inheritHandle = true,
				securityDescriptor = IntPtr.Zero
			};
			var logFileHandle = CreateFile(targetFileName,
				(uint)DesiredAccess.GENERIC_WRITE,
				(uint)ShareMode.FILE_SHARE_WRITE,
				securAttr,
				(uint)CreationDisposition.CREATE_ALWAYS,
				(uint)FileAttributes.FILE_ATTRIBUTE_NORMAL,
				IntPtr.Zero
			);

			startupInfo.cb = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(StartupInfo));
			startupInfo.hStdOutput = logFileHandle;
			startupInfo.hStdError = IntPtr.Zero;
			startupInfo.hStdInput = IntPtr.Zero;
			startupInfo.dwFlags |= (uint)STARTF.USESTDHANDLES;

			ProcessInfo procInfo = new ProcessInfo();
			bool mainProcess = CreateProcess(executableFile,
				null,
				IntPtr.Zero,
				IntPtr.Zero,
				true,
				0,
				IntPtr.Zero,
				null,
				ref startupInfo,
				out procInfo);
			if (mainProcess)
				return procInfo.hProcess;
			else
				PrintErrorIfExists();
			return IntPtr.Zero;
		}

		unsafe static void Main(string[] args)
		{
			int pageSize = 4096;
			int pageCnt = Constants.N + 1;
			int semaphoreCount = pageCnt - 1;
			int writerCount = pageCnt / 2;
			int readerCount = pageCnt / 2;
			int fileSize = semaphoreCount * pageSize;

			Console.WriteLine("Initializing child applications...");

			var freeSem = CreateSemaphore(IntPtr.Zero, semaphoreCount, semaphoreCount, "freeSem");
			var usedSem = CreateSemaphore(IntPtr.Zero, 0, semaphoreCount, "usedSem");
			var fileMutex = CreateMutex(IntPtr.Zero, false, "fileMutex");
			
			var fileHandle = CreateFile(@"D:\1\testfile.txt",
				(uint)DesiredAccess.GENERIC_READ | (uint)DesiredAccess.GENERIC_WRITE,
				(uint)ShareMode.FILE_SHARE_READ | (uint)ShareMode.FILE_SHARE_WRITE,
				null,
				(uint)CreationDisposition.CREATE_ALWAYS,
				0,
				IntPtr.Zero);
			WriteFile(fileHandle, new byte[] { (byte)'\0' }, 1, out uint foo, null);
			var fileMapping = CreateFileMapping(fileHandle, null, FileMapProtection.PageReadWrite, 0, (uint)fileSize, Constants.mappingName);
			var mapView = MapViewOfFile(fileMapping, (uint)FileMapAccess.AllAccess, 0, 0, new UIntPtr((uint)fileSize));
			VirtualLock(mapView, new UIntPtr((uint)fileSize));
			PrintErrorIfExists();
			List<IntPtr> procHandles = new List<IntPtr>();

			for (int i = 0; i < writerCount; i++)
			{
				string logFileName = $@"C:\1\WriteLogs\log{i}.txt";
				procHandles.Add(StartRWProcess(@"C:\Users\Igor\source\repos\OS_lab4_1\Writer\bin\Debug\net5.0\Writer.exe", logFileName));
			}
			for (int i = 0; i < writerCount; i++)
			{
				string logFileName = $@"C:\1\ReadLogs\log{i}.txt";
				procHandles.Add(StartRWProcess(@"C:\Users\Igor\source\repos\OS_lab4_1\Reader\bin\Debug\net5.0\Reader.exe", logFileName));
			}
			Console.WriteLine("Waiting for child applications to finish...");
			WaitForMultipleObjects((uint)procHandles.Count, procHandles.ToArray(), true, uint.MaxValue);

			foreach (var h in procHandles)
				CloseHandle(h);
			UnmapViewOfFile(mapView);
			CloseHandle(fileMapping); //check or move 1 line up
			CloseHandle(fileHandle);
			CloseHandle(fileMutex);
			CloseHandle(freeSem);
			CloseHandle(usedSem);
			Console.WriteLine("All processes finished.");
			Console.ReadKey();
		}
	}
}