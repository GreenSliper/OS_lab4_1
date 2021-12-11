using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winapi.Flags
{
	public enum DesiredAccess : uint
	{
		GENERIC_READ = 0x80000000,
		GENERIC_WRITE = 0x40000000,
		GENERIC_EXECUTE = 0x20000000,
		GENERIC_ALL = 0x10000000
	}
	
	[Flags]
	public enum ShareMode : uint
	{
		None = 0,
		FILE_SHARE_READ = 1,
		FILE_SHARE_WRITE = 2,
		FILE_SHARE_DELETE = 4
	}
	public enum CreationDisposition : uint
	{
		CREATE_NEW = 1,
		CREATE_ALWAYS = 2,
		OPEN_EXISTING = 3,
		OPEN_ALWAYS = 4,
		TRUNCATE_EXISTING = 5
	}

	public enum FileAttributes : uint
	{
		FILE_ATTRIBUTE_READONLY = 0x1,
		FILE_ATTRIBUTE_HIDDEN = 0x2,
		FILE_ATTRIBUTE_SYSTEM = 0x4,
		FILE_ATTRIBUTE_ARCHIVE = 0x20,
		FILE_ATTRIBUTE_NORMAL = 0x80,
		FILE_ATTRIBUTE_TEMPORARY = 0x100,
		FILE_ATTRIBUTE_OFFLINE = 0x1000,
		FILE_ATTRIBUTE_ENCRYPTED = 0x4000
	}

	[Flags]
	public enum FileMapProtection : uint
	{
		PageReadonly = 0x02,
		PageReadWrite = 0x04,
		PageWriteCopy = 0x08,
		PageExecuteRead = 0x20,
		PageExecuteReadWrite = 0x40,
		SectionCommit = 0x8000000,
		SectionImage = 0x1000000,
		SectionNoCache = 0x10000000,
		SectionReserve = 0x4000000,
	}

	public enum FileMapAccess : uint
	{
		Copy = 1,
		Write = 2,
		Read = 4,
		AllAccess = 983071,
		Execute = 32,
	}

	[Flags]
	public enum CreateProcessFlags : uint
	{
		DEBUG_PROCESS = 0x00000001,
		DEBUG_ONLY_THIS_PROCESS = 0x00000002,
		CREATE_SUSPENDED = 0x00000004,
		DETACHED_PROCESS = 0x00000008,
		CREATE_NEW_CONSOLE = 0x00000010,
		NORMAL_PRIORITY_CLASS = 0x00000020,
		IDLE_PRIORITY_CLASS = 0x00000040,
		HIGH_PRIORITY_CLASS = 0x00000080,
		REALTIME_PRIORITY_CLASS = 0x00000100,
		CREATE_NEW_PROCESS_GROUP = 0x00000200,
		CREATE_UNICODE_ENVIRONMENT = 0x00000400,
		CREATE_SEPARATE_WOW_VDM = 0x00000800,
		CREATE_SHARED_WOW_VDM = 0x00001000,
		CREATE_FORCEDOS = 0x00002000,
		BELOW_NORMAL_PRIORITY_CLASS = 0x00004000,
		ABOVE_NORMAL_PRIORITY_CLASS = 0x00008000,
		INHERIT_PARENT_AFFINITY = 0x00010000,
		INHERIT_CALLER_PRIORITY = 0x00020000,
		CREATE_PROTECTED_PROCESS = 0x00040000,
		EXTENDED_STARTUPINFO_PRESENT = 0x00080000,
		PROCESS_MODE_BACKGROUND_BEGIN = 0x00100000,
		PROCESS_MODE_BACKGROUND_END = 0x00200000,
		CREATE_BREAKAWAY_FROM_JOB = 0x01000000,
		CREATE_PRESERVE_CODE_AUTHZ_LEVEL = 0x02000000,
		CREATE_DEFAULT_ERROR_MODE = 0x04000000,
		CREATE_NO_WINDOW = 0x08000000,
		PROFILE_USER = 0x10000000,
		PROFILE_KERNEL = 0x20000000,
		PROFILE_SERVER = 0x40000000,
		CREATE_IGNORE_SYSTEM_DEFAULT = 0x80000000,
	}

	public enum STARTF : uint
	{
		USESHOWWINDOW = 0x00000001,
		USESIZE = 0x00000002,
		USEPOSITION = 0x00000004,
		USECOUNTCHARS = 0x00000008,
		USEFILLATTRIBUTE = 0x00000010,
		RUNFULLSCREEN = 0x00000020, // ignored for non-x86 platforms
		FORCEONFEEDBACK = 0x00000040,
		FORCEOFFFEEDBACK = 0x00000080,
		USESTDHANDLES = 0x00000100
	}

	public enum SemaphoreAccess : uint
	{
		SEMAPHORE_ALL_ACCESS = 2031619,
		SEMAPHORE_MODIFY_STATE = 2,
		SYNCHRONIZE = 1048576
	}
	public enum MutexAccess : uint
	{
		DELETE = 0x0001000,
		READ_CONTROL = 0x00020000,
		SYNCHRONIZE = 0x00100000,
		WRITE_DAC = 0x00040000,
		WRITE_OWNER = 0x00080000,
		MUTEX_ALL_ACCESS = 0x1F0001,
		MUTEX_MODIFY_STATE = 0x0001
	}

	public enum MappingAccess : uint
	{
		STANDARD_RIGHTS_REQUIRED = 0x000F0000,
        SECTION_QUERY = 0x0001,
        SECTION_MAP_WRITE = 0x0002,
        SECTION_MAP_READ = 0x0004,
        SECTION_MAP_EXECUTE = 0x0008,
        SECTION_EXTEND_SIZE = 0x0010,
        SECTION_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | SECTION_QUERY |
			SECTION_MAP_WRITE |
			SECTION_MAP_READ |
			SECTION_MAP_EXECUTE |
			SECTION_EXTEND_SIZE),
        FILE_MAP_ALL_ACCESS = SECTION_ALL_ACCESS
	}

	public enum PipeOpenModeFlags : uint
	{
		PIPE_ACCESS_DUPLEX = 0x00000003,
		PIPE_ACCESS_INBOUND = 0x00000001,
		PIPE_ACCESS_OUTBOUND = 0x00000002,
		FILE_FLAG_FIRST_PIPE_INSTANCE = 0x00080000,
		FILE_FLAG_WRITE_THROUGH = 0x80000000,
		FILE_FLAG_OVERLAPPED = 0x40000000,
	}

	[Flags]
	public enum PipeModeFlags : uint
	{
		//One of the following type modes can be specified. The same type mode must be specified for each instance of the pipe.
		PIPE_TYPE_BYTE = 0x00000000,
		PIPE_TYPE_MESSAGE = 0x00000004,
		//One of the following read modes can be specified. Different instances of the same pipe can specify different read modes
		PIPE_READMODE_MESSAGE = 0x00000002,
		//One of the following wait modes can be specified. Different instances of the same pipe can specify different wait modes.
		PIPE_WAIT = 0x00000000,
		PIPE_NOWAIT = 0x00000001,
		//One of the following remote-client modes can be specified. Different instances of the same pipe can specify different 
		//remote-client modes.
		PIPE_ACCEPT_REMOTE_CLIENTS = 0x00000000,
		PIPE_REJECT_REMOTE_CLIENTS = 0x00000008
	}

	public static class Constants
	{
		public const uint STD_OUTPUT_HANDLE = 4294967285;
		public const uint INFINITE = 4294967295;
		public const uint PIPE_UNLIMITED_INSTANCES = 255;
		public const int N = 13;
		public static readonly string mappingName = "mappinpls";
	}
}