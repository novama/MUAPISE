using System;
using System.Diagnostics;
using System.Reflection;

namespace Muapise.Common.Config
{
    /// <summary>
    ///     Helper class used to get information from assemblies.
    /// </summary>
    public static class AssemblyInfoHelper
    {
        /// <summary>
        ///     Gets the product name for a given assembly
        /// </summary>
        /// <param name="assembly">The assembly to use.</param>
        /// <returns></returns>
        public static string GetProductName(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException();

            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersionInfo.ProductName;
        }

        /// <summary>
        ///     Gets the product name for the current executing assembly
        /// </summary>
        /// <returns></returns>
        public static string GetProductName()
        {
            var assembly = Assembly.GetExecutingAssembly();
            return GetProductName(assembly);
        }

        /// <summary>
        ///     Gets the Version object for a given assembly
        /// </summary>
        /// <param name="assembly">The assembly to use.</param>
        /// <returns></returns>
        public static Version GetVersion(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException();

            var version = assembly.GetName().Version;
            return version;
        }

        /// <summary>
        ///     Gets the version object for the current executing assembly
        /// </summary>
        /// <returns></returns>
        public static Version GetVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            return GetVersion(assembly);
        }

        /// <summary>
        ///     Gets the company name for a given assembly
        /// </summary>
        /// <param name="assembly">The assembly to use.</param>
        /// <returns></returns>
        public static string GetCompanyName(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException();
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersionInfo.CompanyName;
        }

        /// <summary>
        ///     Gets the company name for the current executing assembly
        /// </summary>
        /// <returns></returns>
        public static string GetCompanyName()
        {
            var assembly = Assembly.GetExecutingAssembly();
            return GetCompanyName(assembly);
        }
    }
}