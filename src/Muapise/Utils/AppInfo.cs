using System;
using System.Diagnostics;
using System.Reflection;
using Muapise.Common.Config;

namespace Muapise.Utils
{
    /// <summary>
    /// Provides information about the application
    /// </summary>
    internal static class AppInfo
    {
        internal static Assembly AppAssembly => Assembly.GetExecutingAssembly();
        internal static string AppAssemblyName = AppAssembly.GetName().Name;
        internal static string AppBasePath = AppDomain.CurrentDomain.BaseDirectory;
        internal static string AppProcessName = Process.GetCurrentProcess().ProcessName;
        internal static string AppName = AssemblyInfoHelper.GetProductName();
        internal static Version AppVersion = AssemblyInfoHelper.GetVersion();
        internal static string AppVersionString = AppVersion.ToString();
        internal static string AppCompany = AssemblyInfoHelper.GetCompanyName();
    }
}
