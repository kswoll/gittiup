// <copyright file="GittiupDb.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2018 PlanGrid, Inc. All rights reserved.
// </copyright>

using System.IO;
using LiteDB;

namespace Gittiup.Models
{
    public class GittiupDb : LiteDatabase
    {
        public LiteCollection<AccountModel> Accounts => GetCollection<AccountModel>("accounts");
        public LiteCollection<RepositoryModel> Repositories => GetCollection<RepositoryModel>("repositories");

        static GittiupDb()
        {
            var mapper = BsonMapper.Global;
            mapper.Entity<RepositoryModel>().DbRef(x => x.Account);
        }

        public GittiupDb() : base(Settings.DbFile)
        {
        }
    }
}