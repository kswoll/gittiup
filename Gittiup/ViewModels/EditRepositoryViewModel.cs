// <copyright file="EditRepositoryViewModel.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2018 PlanGrid, Inc. All rights reserved.
// </copyright>

using Gittiup.Models;

namespace Gittiup.ViewModels
{
    public class EditRepositoryViewModel
    {
        public RepositoryModel Repository { get; set; }

        public EditRepositoryViewModel(RepositoryModel repository)
        {
            Repository = repository;
        }
    }
}