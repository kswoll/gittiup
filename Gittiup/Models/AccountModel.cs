﻿// <copyright file="AccountModel.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2018 PlanGrid, Inc. All rights reserved.
// </copyright>

using Gittiup.Utils;

namespace Gittiup.Models
{
    public class AccountModel : BaseObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}