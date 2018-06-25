using System;
using System.IO;

namespace Gittiup.Library
{
    public class Settings
    {
        public static string DbFile => $@"{RootDataDirectory}\gittiup.db";
        public static string RootDataDirectory { get; } = Environment.ExpandEnvironmentVariables(@"%LOCALAPPDATA%\Gittiup");

        static Settings()
        {
            Directory.CreateDirectory(RootDataDirectory);
        }
    }
}