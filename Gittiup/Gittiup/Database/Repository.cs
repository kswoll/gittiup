// <copyright file="Repository.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2018 PlanGrid, Inc. All rights reserved.
// </copyright>

using Gittiup.Utils;

namespace Gittiup.Database
{
    public class Repository : BaseObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Path { get; set; }
    }
}