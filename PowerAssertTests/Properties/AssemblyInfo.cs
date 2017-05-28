using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using ApprovalTests.Reporters;
using PowerAssertTests.Approvals.ApprovalTestExtensions;

[assembly: FrontLoadedReporter(typeof (CiReporter))]
[assembly: UseReporter(typeof (HappyDiffReporter))]