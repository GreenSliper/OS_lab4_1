using System;
using System.Text;
using Winapi.Flags;
using static Winapi.Functions;

namespace Writer
{
	class Writer
	{
		static string GetTime() => DateTime.Now.ToString("MM.dd HH:mm:ss:fff");

		/// <summary>
		/// WriteFile wrapper
		/// </summary>
		/// <param name="str">target string</param>
		/// <returns>number of written bytes</returns>
		unsafe static uint WriteLog(string str, IntPtr outHandle)
		{
			var outputStr = Encoding.UTF8.GetBytes(str);
			WriteFile(outHandle, outputStr, (uint)outputStr.Length, out uint writtenBytes, null);
			return writtenBytes;
		}

		unsafe static void Main(string[] args)
		{
			var freeSem = OpenSemaphore((uint)SemaphoreAccess.SYNCHRONIZE | (uint)SemaphoreAccess.SEMAPHORE_MODIFY_STATE,
				false, "freeSem");
			var usedSem = OpenSemaphore((uint)SemaphoreAccess.SYNCHRONIZE | (uint)SemaphoreAccess.SEMAPHORE_MODIFY_STATE,
				false, "usedSem");
			var fileMutex = OpenMutex((uint)MutexAccess.MUTEX_ALL_ACCESS, false, "fileMutex");
			var stdOut = GetStdHandle(Constants.STD_OUTPUT_HANDLE);

			int page = -1;
			int written = 0;
			var mappingHandle = OpenFileMapping((uint)FileMapAccess.Write | (uint)FileMapAccess.Read,
				false, Constants.mappingName);


			if (mappingHandle != IntPtr.Zero)
			{
				for (int i = 0; i < 3; i++)
				{
					WaitForSingleObject(freeSem, Constants.INFINITE);
					WriteLog($"{GetTime()} | TAKE | FREE SEMAPHORE\n", stdOut);
					PrintErrorIfExists();


					WaitForSingleObject(fileMutex, Constants.INFINITE);
					WriteLog($"{GetTime()} | TAKE | MUTEX\n", stdOut);
					PrintErrorIfExists();


					SleepEx((uint)new Random().Next(1000) + 500, false);

					if (ReleaseMutex(fileMutex))
						WriteLog($"{GetTime()} | FREE | MUTEX\n", stdOut);
					else
						WriteLog($"MUTEX ERROR CODE: {GetLastError()}\n", stdOut);

					if (ReleaseSemaphore(usedSem, 1, out page))
					{
						WriteLog($"{GetTime()} | FREE | USED SEMAPHORE\n", stdOut);
						WriteLog($"{GetTime()} | PAGE | NUMBER = {page+1}\n\n", stdOut);
					}
					else
						WriteLog($"SEMAPHORE ERROR CODE: {GetLastError()}\n", stdOut);
				}
			}
			else
			{
				PrintErrorIfExists();
				WriteLog($"MAPPING CREATION ERROR CODE: {GetLastError()}\n", stdOut);
			}

			CloseHandle(stdOut);
		}
	}
}
