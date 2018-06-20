// <copyright file="Settings.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2018 PlanGrid, Inc. All rights reserved.
// </copyright>

using System;
using System.IO;

namespace Gittiup
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