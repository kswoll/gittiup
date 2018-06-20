// <copyright file="RepositoryViewModel.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2018 PlanGrid, Inc. All rights reserved.
// </copyright>

using Gittiup.Models;
using Gittiup.Utils;
using LibGit2Sharp;

namespace Gittiup.ViewModels
{
    public class RepositoryViewModel : BaseObject
    {
        public RepositoryModel Repository { get; }
        public Repository Repo { get; }

        public RepositoryViewModel(RepositoryModel repository)
        {
            Repository = repository;
            Repo = new Repository(repository.Path);
        }
    }
}