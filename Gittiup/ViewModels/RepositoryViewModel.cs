// <copyright file="RepositoryViewModel.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2018 PlanGrid, Inc. All rights reserved.
// </copyright>

using Gittiup.Models;
using Gittiup.Utils;

namespace Gittiup.ViewModels
{
    public class RepositoryViewModel : BaseObject
    {
        public RepositoryModel Repository { get; }

        public RepositoryViewModel(RepositoryModel repository)
        {
            Repository = repository;
        }
    }
}