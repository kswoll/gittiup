// <copyright file="GittiupDb.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2018 PlanGrid, Inc. All rights reserved.
// </copyright>

using System.IO;
using Windows.Storage;
using LiteDB;

namespace Gittiup.Database
{
    public class GittiupDb : LiteDatabase
    {
        public LiteCollection<Account> Accounts => GetCollection<Account>("accounts");
        public LiteCollection<Repository> Repositories => GetCollection<Repository>("repositories");

        public GittiupDb() : base(Path.Combine(ApplicationData.Current.LocalFolder.Path, "gittiup.db"))
        {
        }
    }
}