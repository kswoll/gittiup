// <copyright file="AddAccountViewModel.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2018 PlanGrid, Inc. All rights reserved.
// </copyright>

namespace Gittiup.ViewModels
{
    public class AddAccountViewModel
    {
        private string name;
        public AccountsViewModel AccountsViewModel { get; set; }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}