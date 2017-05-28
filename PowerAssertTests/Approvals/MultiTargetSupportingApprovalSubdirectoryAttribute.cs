using System;
using System.IO;
using System.Reflection;

namespace PowerAssertTests.Approvals
{
	[AttributeUsage(AttributeTargets.Class)]
	public class MultiTargetSupportingApprovalSubdirectoryAttribute : ApprovalTests.Namers.UseApprovalSubdirectoryAttribute
	{
		public MultiTargetSupportingApprovalSubdirectoryAttribute(string subdirectory) : base((string) ConvertToCorrectPath(subdirectory))
		{
		}

		static string ConvertToCorrectPath(string subdirectory)
		{
			string codeBase = Assembly.GetExecutingAssembly().CodeBase;
			UriBuilder uri = new UriBuilder(codeBase);
			string path = Uri.UnescapeDataString(uri.Path);
			var directory = new DirectoryInfo(path).Parent;
			var rootProjectFolder = directory.Parent.Parent.Parent.FullName.Replace(directory.Root.FullName, "");
			var targetPath = Path.Combine(rootProjectFolder, subdirectory);
			return targetPath;
		}
	}
}